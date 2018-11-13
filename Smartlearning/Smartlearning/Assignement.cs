using System;
using System.Collections.Generic;

namespace Smartlearning
{
	class Assignement
	{
		public string Name { get; set; }
		public Dictionary<Student,Boolean> IsPassed { get; }

		public Assignement(string assignmentName)
		{
			Name = assignmentName;
			IsPassed = new Dictionary<Student, bool>();
			Console.WriteLine("Assignement created: " + assignmentName);
		}

		public void AddResult(Student student, Boolean isPassed)
		{
			IsPassed.Add(student,isPassed);
			Console.WriteLine("Result added: " + student.Name + ": " + isPassed );
		}

		public bool IsAssignmentPassed(Student student)
		{
			bool isPassed = false;

			if(IsPassed.ContainsKey(student))
			{
				isPassed = IsPassed[student];
				return isPassed;
			}
			else
			{
				Console.WriteLine("Student not found");
				return false;
			}
		}
	}
}