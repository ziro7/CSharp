using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Archive
{
	class Archive
	{

		public List<Person> Persons { get; } = new List<Person>();

		public void CreateOrDeletePerson(Boolean isStudent, string name, int age, string adresse, int postNumber, int phoneNumber,
			string job = "Unknown", double salary = 0, School school = School.CbsBussiness)
		{
			for (int i = 0; i < Persons.Count; i++)
			{
				if (postNumber == Persons[i].PhoneNumber)
				{
					Persons.Remove(Persons[i]);
				}
			}

			if (isStudent)
			{
				Persons.Add(new Student(name, age, adresse, postNumber, phoneNumber, school));
			}
			else
			{
				Persons.Add(new Employee(name, age, adresse, postNumber, phoneNumber, job, salary));
			}

		}

		public void DeletePerson()
		{

		}

		public int ShowNumberOf()
		{
			return 0;
		}

		public List<Person> ShowPersonsWith()
		{
			return Persons;
		}

		public List<Person> FindPersonsWith()
		{
			return Persons;
		}



	}
}
