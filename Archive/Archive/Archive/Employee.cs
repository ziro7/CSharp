using System;
using System.Collections;

namespace Archive
{
	public class Employee : Person
	{
		public string Job { get; set; }
		public double? Salary { get; set; }

		public Employee(string name, int age, string adresse, int postNumber, int phoneNumber, string job, double salary)
			: base(name, age, adresse, postNumber, phoneNumber)
		{
			Job = job;
			Salary = salary;
		}

		// Nested class to do ascending sort on year property.
		private class SortSalaryDescendingHelper : IComparer
		{
			int IComparer.Compare(object a, object b)
			{
				Employee c1 = (Employee)a;
				Employee c2 = (Employee)b;

				if (c1.Salary < c2.Salary)
					return 1;

				if (c1.Salary > c2.Salary)
					return -1;

				else
					return 0;
			}

		}

		// Nested class to do ascending sort on year property.
		private class SortSalaryAscendingHelper : IComparer
		{
			int IComparer.Compare(object a, object b)
			{
				Employee c1 = (Employee)a;
				Employee c2 = (Employee)b;

				if (c1.Salary > c2.Salary)
					return 1;

				if (c1.Salary < c2.Salary)
					return -1;

				else
					return 0;
			}
		}

		// Method to return IComparer object for sort helper.
		public static IComparer SortSalaryDescending()
		{
			return (IComparer)new SortSalaryDescendingHelper();
		}

		// Method to return IComparer object for sort helper.
		public static IComparer SortSalaryAscending()
		{
			return (IComparer)new SortSalaryAscendingHelper();
		}

	}
}