using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Archive
{
	class Archive
	{
		private readonly ILogger _logger;
		private readonly IPrint _print;
		
		public List<Student> Students { get; } = new List<Student>();
		public List<Employee> Employees { get; } = new List<Employee>();

		public Archive(ILogger logger, IPrint print)
		{
			this._logger = logger;
			this._print = print;
		}

		public void CreateOrAlterStudent(string name, int age, string adresse, int postNumber, int phoneNumber, School school)
		{
			foreach (var student in Students)
			{
				if (phoneNumber == student.PhoneNumber)
				{
					student.Name = name;
					student.Age = age;
					student.Adresse = adresse;
					student.PostNumber = postNumber;
					student.PhoneNumber = phoneNumber;
					student.School = school;
					_logger.LogInfo("Studerende ændret kl. " + DateTime.Now);
				}
				else
				{
					Students.Add(new Student(name, age, adresse, postNumber, phoneNumber, school));
					_logger.LogInfo("Studerende oprettet kl. " + DateTime.Now);
				}
			}
		}

		public void CreateOrAlterEmployee(string name, int age, string adresse, int postNumber, int phoneNumber, string job,
			double salary)
		{
			foreach (var employee in Employees)
			{
				if (phoneNumber == employee.PhoneNumber)
				{
					employee.Name = name;
					employee.Age = age;
					employee.Adresse = adresse;
					employee.PostNumber = postNumber;
					employee.PhoneNumber = phoneNumber;
					employee.Job = job;
					employee.Salary = salary;
					_logger.LogInfo("Medarbejder ændret kl. " + DateTime.Now);

				}
				else
				{
					Employees.Add(new Employee(name, age, adresse, postNumber, phoneNumber, job, salary));
					_logger.LogInfo("Medarbejder oprettet kl. " + DateTime.Now);
				}
			}
		}

		//Delete a person based on phoneNumber
		public void DeletePerson(int phoneNumber)
		{
			for (int i = 0; i < Students.Count; i++)
			{
				if (Students[i].PhoneNumber == phoneNumber)
				{
					_logger.LogInfo("Person med telefonnummer " + Students[i].PhoneNumber + " blev slettet kl. " + DateTime.Now);
					Students.Remove(Students[i]);
					break;
				}

				if (Employees[i].PhoneNumber == phoneNumber)
				{
					_logger.LogInfo("Person med telefonnummer " + Employees[i].PhoneNumber + " blev slettet kl. " + DateTime.Now);
					Employees.Remove(Employees[i]);
					break;
				}

			}
		}

		//Viser antalet af mennesker
		public int ShowNumberOfPersons()
		{
			_logger.LogInfo("Viser antallet af personer kl. " + DateTime.Now);
			return Employees.Count + Students.Count;
		}

		//Overload af ShowNumberOfPersons metode 
		//Viser antale af studerende eller employees
		public int ShowNumberOfPersons(Boolean showStudents)
		{
			_logger.LogInfo("Viser antallet af specifikke personer kl. " + DateTime.Now);
			return (showStudents) ? Students.Count : Employees.Count;
		}

		public List<Person> ShowPersonsWithAge(Boolean lowestAge = true)
		{
			List<Person> minAgeList = new List<Person>();
			List<Person> maxAgeList = new List<Person>();
			int minAge;
			int maxAge;

			//sorterer listen efter CompareTo i persons hvilket er age i descending
			Students.Sort();
			Employees.Sort();

			//Da listen er sorteret efter alder, må min og max være de to ydre værdier
			//minAge = 
			minAge = (Students[0].Age <= Employees[0].Age) ? Students[0].Age : Employees[0].Age;
			maxAge = (Students[Students.Count - 1].Age <= Employees[Employees.Count - 1].Age) ? Students[Students.Count - 1].Age : Employees[Employees.Count - 1].Age;
			
			//hvis en person på listen har samme alder som min eller max alder bliver de tilføjet den respective liste .
			for (int i = 1; i < Students.Count; i++)
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

			for (int i = 1; i < Employees.Count; i++)
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

			//returnere den liste der bliver bedt om.
			_logger.LogInfo("Retuner person(er) med højeste eller laveste aldre kl. " + DateTime.Now);
			_print.Print(minAgeList.ToString());
			if (lowestAge)
			{
				_print.Print(minAgeList.ToString());
				return minAgeList;
			}
			else
			{
				_print.Print(maxAgeList.ToString());
				return maxAgeList;
			}
		}

		public List<Employee> ShowEmployeesWithSalary(Boolean lowestSalary)
		{
			List<Employee> minSalaryList = new List<Employee>();
			List<Employee> maxSalaryList = new List<Employee>();


			Employees.Sort(); //efter salary

			//da listen er sorteret efter løn må min og max være de to yderse værdier.
			//hvis listen er sorteret stigende/falden kunne man bare min = [0], max = count -1
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
			_logger.LogInfo("Retuner person(er) med højeste eller laveste løn kl. " + DateTime.Now);
			return (lowestSalary) ? minSalaryList : maxSalaryList;

		}

		//Find en person
		public Person FindPersonWithPhoneNumber(int phoneNumber)
		{

			for (int i = 1; i < Students.Count; i++)
			{
				if(Students[i].PhoneNumber == phoneNumber)
				{
					return Students[i];
				} 
			}

			for (int i = 1; i < Employees.Count; i++)
			{
				if (Employees[i].PhoneNumber == phoneNumber)
				{
					return Employees[i];
				}
			}

			throw new ArchiveException("Der findes ikke en person med det nummer");
		}

	}
}
