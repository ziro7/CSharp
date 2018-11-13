using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Smartlearning
{
	class SmartLearning
	{
		public List<Course> Courses { get;}

		public SmartLearning()
		{
			Courses = new List<Course>();
		}

		public void RemoveCourse(string courseName)
		{
			Courses.Find(x => x.Name == courseName);
			Console.WriteLine("Smartlearning course was removed: " + courseName);
		}

		public void AddCourse(string courseName, Teacher teacherName)
		{
			var newCourse = new Course(courseName, teacherName);
			Courses.Add(newCourse);
			Console.WriteLine("Smartlearning course created: " + courseName);

		}
	}

}