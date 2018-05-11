using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archive
{
	public class Archive
	{
		public ILogger Logger { get; }
		
		public List<Student> Students { get; } = new List<Student>();
		public List<Employee> Employees { get; } = new List<Employee>();

		//Konstruktor
		public Archive(ILogger logger)
		{
			this.Logger = logger;
		}

		//I nedenstående 2 metoder undersøges det først, om der allerede finde en registrering med samme telefon nummer. 
		//Hvis der findes en opdateres personen, med de data der er blevet indsat i formularen og breaker ud af løkken,
		//så løkken ikke bruger tid på at køre listen færdig (der burde ikke kunne forekomme to forekomster med samme telefonnr.)
		public void CreateOrAlterStudent(string name, int age, string adresse, int postNumber, int phoneNumber, School school)
		{
			bool personFound = false;

			foreach (var student in Students)
			{
				if (student.PhoneNumber == phoneNumber)
				{
					student.Name = name;
					student.Age = age;
					student.Adresse = adresse;
					student.PostNumber = postNumber;
					student.PhoneNumber = phoneNumber;
					student.School = school;
					Logger.LogInfo("Studerende ændret kl. " + DateTime.Now);
					personFound = true;
				}
			}

			if (!personFound)
			{
				Students.Add(new Student(name, age, adresse, postNumber, phoneNumber, school));
				Logger.LogInfo("Studerende oprettet kl. " + DateTime.Now);
			}
		}
	
		public void CreateOrAlterEmployee(string name, int age, string adresse, int postNumber, int phoneNumber, string job,
			double? salary)
		{
			bool personFound = false;

			foreach (var employee in Employees)
			{
				if (employee.PhoneNumber == phoneNumber)
				{
					employee.Name = name;
					employee.Age = age;
					employee.Adresse = adresse;
					employee.PostNumber = postNumber;
					employee.PhoneNumber = phoneNumber;
					employee.Job = job;
					employee.Salary = salary;
					Logger.LogInfo("Medarbejder ændret kl. " + DateTime.Now);
					personFound = true;
				}
			}
			if (!personFound)
			{
				Employees.Add(new Employee(name, age, adresse, postNumber, phoneNumber, job, salary));
				Logger.LogInfo("Medarbejder oprettet kl. " + DateTime.Now);
			}
		}

		//I nedenstående metode undersøges det først om der finde en person med et matchende telefonnummer. 
		//Hvis der findes et match slettes denne registrering. Hvis der ikke findes et match bliver smidt 
		//en exception med info som bliver sendt tilbage til kalderen.
		public void DeletePerson(int phoneNumber)
		{
			bool personFound = false;

			for (int i = 0; i < Students.Count; i++)
			{
				if (Students[i].PhoneNumber == phoneNumber)
				{
					Logger.LogInfo("Person med telefonnummer " + Students[i].PhoneNumber + " blev slettet kl. " + DateTime.Now);
					Students.Remove(Students[i]);
					MessageBox.Show("Sletning succesfuld", "Sletning succesfuld", MessageBoxButtons.OK);
					personFound = true;
					break;
				}
			}

			if (personFound == false)
			{
				for (int i = 0; i < Employees.Count; i++)
				{
					if (Employees[i].PhoneNumber == phoneNumber)
					{
						Logger.LogInfo("Person med telefonnummer " + Employees[i].PhoneNumber + " blev slettet kl. " + DateTime.Now);
						Employees.Remove(Employees[i]);
						MessageBox.Show("Sletning succesfuld", "Sletning succesfuld", MessageBoxButtons.OK);
						personFound = true;
						break;
					}
				}
			}

			if (personFound == false)
			{
				MessageBox.Show("Der findes ikke en person med det telefon nummer i listen", "Ingen person matcher", MessageBoxButtons.OK);
				Logger.LogInfo("Person ikke fundet og kan ikk slettes");
			}
		}

		//Viser antalet af mennesker
		public int ShowNumberOfPersons()
		{
			Logger.LogInfo("Viser antallet af personer kl. " + DateTime.Now);
			return Employees.Count + Students.Count;
		}

		//Overload af ShowNumberOfPersons metode 
		//Viser antale af studerende eller employees
		public int ShowNumberOfPersons(Boolean showStudents)
		{
			Logger.LogInfo("Viser antallet af specifikke personer kl. " + DateTime.Now);
			return (showStudents) ? Students.Count : Employees.Count;
		}

		//Da der er flere der kan have samme alder eller løn, sker der her flere steps. 
		//1. Sorter listen efter Age/løn for at sikre at første registrering er den mindste og sidste registrering er den højeste. 
		//2. tildel min og max værdier 
		//3. iterere hen over de to løkker (ved age og en ved salary) og finder de personer der matcher min/max løn og skriv dem til en resultat liste. 
		//4. returner den relevante liste.
		public List<Person> ShowPersonsWithAge(Boolean lowestAge = true)
		{
			List<Person> minAgeList = new List<Person>();
			List<Person> maxAgeList = new List<Person>();
			int minAge;
			int maxAge;

			//1. sorterer listen efter CompareTo i persons hvilket er age i descending
			Students.Sort();
			Employees.Sort();

			//2. tildel min og max værdier 
			//Da listen er sorteret efter alder, må min og max være de to ydre værdier - men det valideres lige inden det assignes.
			minAge = (Students[0].Age <= Employees[0].Age) ? Students[0].Age : Employees[0].Age;
			maxAge = (Students[Students.Count - 1].Age <= Employees[Employees.Count - 1].Age) ? Students[Students.Count - 1].Age : Employees[Employees.Count - 1].Age;

			//3. iterere hen over de to løkker (ved age og en ved salary) og finder de personer der matcher min/max løn og skriv dem til en resultat liste. 
			//hvis en person på listen har samme alder som min eller max alder bliver de tilføjet den respective liste .
			for (int i = 0; i < Students.Count; i++)
			{
				if (Students[i].Age == minAge)
				{
					minAgeList.Add(Students[i]);
				}

				if (Students[i].Age == maxAge)
				{
					maxAgeList.Add(Students[i]);
				}
			}

			for (int i = 0; i < Employees.Count; i++)
			{
				if (Employees[i].Age == minAge)
				{
					minAgeList.Add(Employees[i]);
				}

				if (Employees[i].Age == maxAge)
				{
					maxAgeList.Add(Employees[i]);
				}
			}

			//4. returner den relevante liste.
			Logger.LogInfo("Retuner person(er) med højeste eller laveste aldre kl. " + DateTime.Now);
			if (lowestAge)
			{
				return minAgeList;
			}
			else
			{
				return maxAgeList;
			}
		}

		//Se kommentar fra ShowPersonsWithAge
		public List<Employee> ShowEmployeesWithSalary(Boolean lowestSalary)
		{
			List<Employee> minSalaryList = new List<Employee>();
			List<Employee> maxSalaryList = new List<Employee>();

			Employees.Sort(new SortSalaryAscendingHelper()); //efter salary stigende

			//da listen er sorteret efter løn må min og max være de to yderse værdier.
			//hvis listen er sorteret stigende kunne man bare min = [0], max = count -1
			Double? minSalary = Employees[0].Salary;
			Double? maxSalary = Employees[Employees.Count - 1].Salary;

			//hvis en person på listen har samme løn som min eller max alder bliver de tilføjet den respective liste .
			//hvis jeg ved at listen er sorteret stigende kunne jeg lave while løn = minLøn og spare tid.

			foreach (var employee in Employees)
			{
				if (employee.Salary.Equals(minSalary))
				{
					minSalaryList.Add(employee);
				}

				if (employee.Salary.Equals(maxSalary))
				{
					maxSalaryList.Add(employee);
				}
			}

			//returnere den liste der bliver bedt om.
			Logger.LogInfo("Retuner person(er) med højeste eller laveste løn kl. " + DateTime.Now);
			return (lowestSalary) ? minSalaryList : maxSalaryList;

		}

		//I nedenstående metode undersøges det først om der finde en person med et matchende telefonnummer. 
		//Hvis der findes et match sendes denne person retur - det sker i en liste da det skal bruges til grid. 
		//Hvis der ikke findes et match bliver der smidt en exception med info som bliver sendt tilbage til kalderen.
		public List<Person> FindPersonWithPhoneNumber(int phoneNumber)
		{
			List<Person> allPersons = ShowAllPersons();
			List<Person> resultList = new List<Person>();

			for (int i = 0; i < allPersons.Count; i++)
			{
				if(allPersons[i].PhoneNumber == phoneNumber)
				{
					resultList.Add(allPersons[i]);
					return resultList;
				} 
			}
			throw new ArchiveException("Der findes ikke en person med det nummer");
		}

		public List<Person> ShowAllPersons()
		{
			List<Person> allPersons = new List<Person>();

			for (int i = 0; i < Students.Count; i++)
			{
				allPersons.Add(Students[i]);
			}
			
			for (int i = 0; i < Employees.Count; i++)
			{
				allPersons.Add(Employees[i]);
			}
		
			return allPersons;
		}
	}
}
