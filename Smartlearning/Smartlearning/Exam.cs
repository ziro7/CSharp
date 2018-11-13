using System;
using System.Collections.Generic;

namespace Smartlearning
{
	class Exam
	{
		public Boolean IsOnline { get; set; }
		public DateTime Date { get; set; }
		public Teacher Examiner { get; set; }
		public List<Teacher> ExtraExaminer { get; set; }
		public List<Student> StudentsEligableForExam { get;  }
		public Dictionary<Student,Int32> StudentGrade { get;  }


		public Exam(Boolean isOnline, DateTime date)
		{
			IsOnline = isOnline;
			Date = date;
			Examiner = null;
			ExtraExaminer = new List<Teacher>();
			StudentsEligableForExam = new List<Student>();
			StudentGrade = new Dictionary<Student, int>();
		}

		public void AddStudentToExam(Student student)
		{
			StudentsEligableForExam.Add(student);
			Console.WriteLine("Added: " + student.Name + " to the exam");
		}

		public void RemoveStudentFromExam(String studentName)
		{
			var studentToRemove = StudentsEligableForExam.Find(x => x.Name == studentName);
			StudentsEligableForExam.Remove(studentToRemove);
			Console.WriteLine("removed: " + studentName + " from the exam");
		}

		public void AddExaminer(Teacher teacher)
		{
			if (Examiner == null)
			{
				Examiner = teacher;
				Console.WriteLine("Added: " + teacher.Name + " to the exam as main examiner");
			}
			else
			{
				ExtraExaminer.Add(teacher);
				Console.WriteLine("Added: " + teacher.Name + " to the exam as assistent examiner");
			}
		}

		public void AddResult(Student student, Int32 grade)
		{
			StudentGrade.Add(student, grade);
			Console.WriteLine("Added result for: " + student.Name + " with a grade of: " + grade);
		}
	}
}