using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace Archive
{
	public class Student : Person
	{
		public School School { get; set; }

		public Student(string name, int age, string adress, int postNumber, int phoneNumber, School school)
			: base(name, age, adress, postNumber, phoneNumber)
		{
			School = school;
		}
	}
}

