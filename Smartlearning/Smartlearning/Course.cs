using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Smartlearning
{
	class Course
	{
		public string Name { get; set; }
		public List<Student> Students { get; set; }
		public Teacher MainTeacher { get; set; }
		public List<Teacher> ExtraTeacher { get; }
		public List<Assignement> RequiredAssignements { get; }
		public List<Exam> Exams { get;  }

		public Course(string name)
		{
			Name = name;
			MainTeacher = null;
			Students = new List<Student>();
			ExtraTeacher = new List<Teacher>();
			RequiredAssignements = new List<Assignement>();
			Exams = new List<Exam>();
		}

		public Course(string name, Teacher mainTeacher)
		{
			Name = name;
			Students = new List<Student>();
			MainTeacher = mainTeacher;
			ExtraTeacher = new List<Teacher>();
			RequiredAssignements = new List<Assignement>();
			Exams = new List<Exam>();
		}

		public void AddStudentToCourse(string newStudent)
		{
			Students.Add(new Student(newStudent));
			Console.WriteLine("Student: " + newStudent + " was added to the course");
		}

		public void RemoveStudentFromCourse(string studentName)
		{
			var studentToRemove = Students.Find(x => x.Name == studentName);
			Students.Remove(studentToRemove);
		}

		public void AddTeacherToCourse(Teacher teacherName)
		{
			if (MainTeacher == null)
			{
				MainTeacher = teacherName;
				Console.WriteLine(teacherName.Name + ": Added to course as teacher");
			}
			else
			{
				ExtraTeacher.Add(teacherName);
				Console.WriteLine(teacherName.Name + ": Added to course as assistent teacher");
			}
		}

		public void RemoveTeacherFromCourse(string teacherName)
		{
			if (MainTeacher.Name == teacherName)
			{
				MainTeacher = null;

				if (ExtraTeacher.Count <= 0) return;
				Teacher newMainTeacher = ExtraTeacher[0];
				ExtraTeacher.Remove(ExtraTeacher[0]);
				MainTeacher = newMainTeacher;
				Console.WriteLine(teacherName + ": removed from course as main teacher");
			}
			else
			{
				var teacherToRemove = ExtraTeacher.Find(x => x.Name == teacherName);
				ExtraTeacher.Remove(teacherToRemove);
				Console.WriteLine(teacherName + ": removed from course as assitent teacher");
			}


		}

		public void AddAssignment(string assignementName)
		{
			RequiredAssignements.Add(new Assignement(assignementName));
		}

		public void AddExam(bool isOnline, DateTime date)
		{
			Exams.Add(new Exam(isOnline, date));
			Console.WriteLine("Exam created: " + "isOnline: " + isOnline + " at: " + date );
		}

		public Boolean IsStudentEligableForExam(Student student)
		{
			bool studentIsEligable = true;

				foreach (Assignement assignement in RequiredAssignements)
				{
					if (assignement.IsAssignmentPassed(student) == false)
					{
						studentIsEligable = false;
						Console.WriteLine("Student: " + student.Name + " is not eligable for exam");
						break;
					};
				}
			
			return studentIsEligable;
		}

		public void AddStudentToExam(Student student, bool isOnline, DateTime date)
		{
			if (IsStudentEligableForExam(student))
			{
				bool studentAdded = false;

				foreach (Exam exam in Exams)
				{
					if (exam.Date == date && exam.IsOnline == isOnline)
					{
						exam.AddStudentToExam(student);
						studentAdded = true;
					}
				}

				if (!studentAdded)
				{
					Console.WriteLine("No matching exam found");
				}


				

			}
		}
	}
}