

using System;

namespace Archive
{
	public class Employee : Person, IComparable
	{
		public string Job { get; set; }
		public double Salary { get; set; }	

		public Employee(string name, int age, string adresse, int postNumber, int phoneNumber, string job, double salary) 
			: base(name, age, adresse, postNumber, phoneNumber)
		{
			Job = job;
			Salary = salary;
		}

		public int CompareTo(object obj)
		{
			throw new NotImplementedException();
		}
	}
}