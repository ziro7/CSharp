﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Archive
{
	public abstract class Person : IComparable
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public string Adresse { get; set; }
		public int PostNumber { get; set; }
		public int PhoneNumber { get; set; }

		protected Person(string name, int age, string adresse, int postNumber, int phoneNumber)
		{
			Name = name;
			Age = age;
			Adresse = adresse;
			PostNumber = postNumber;
			PhoneNumber = phoneNumber;
		}

		//Implementere IComparable ved at have en default sortering efter alder 
		public int CompareTo(object obj)
		{
			var a = (Person)obj;
			return this.Age.CompareTo(a.Age);
		}
	}
}
