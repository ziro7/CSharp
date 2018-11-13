using System;
using System.Linq;

namespace Smartlearning
{
	class Program
    {
        static void Main(string[] args)
        {
            SmartLearning smartLearning = new SmartLearning();

	        Teacher faisal = new Teacher("Faisal Jarkass");
			smartLearning.AddCourse("Avanceret Programmering",faisal);

	        var avanceretProgramering = smartLearning.Courses[0];
			avanceretProgramering.AddAssignment("Obligatorisk opgave 1");
			avanceretProgramering.AddAssignment("Obligatorisk opgave 2");

			DateTime examInDecember = new DateTime(2018,12,10);
			avanceretProgramering.AddExam(true,examInDecember);


	        Teacher kristian = new Teacher("Kristian");
			avanceretProgramering.AddTeacherToCourse(kristian);

			avanceretProgramering.AddStudentToCourse("Jacob Kjærgaard");
	        avanceretProgramering.AddStudentToCourse("Jens Hansen");
	        avanceretProgramering.AddStudentToCourse("Jens HansenTest");
	        Student jacob = avanceretProgramering.Students[0];
	        Student jens = avanceretProgramering.Students.Find(x => x.Name == "Jens Hansen");
			

			avanceretProgramering.RemoveStudentFromCourse("Jens HansenTest");

			avanceretProgramering.RequiredAssignements[0].AddResult(jacob, true);
	        avanceretProgramering.RequiredAssignements[0].AddResult(jens, false);
	        avanceretProgramering.RequiredAssignements[1].AddResult(jacob, true);
	        avanceretProgramering.RequiredAssignements[1].AddResult(jens, false);

			avanceretProgramering.IsStudentEligableForExam(jacob);
	        avanceretProgramering.IsStudentEligableForExam(jens);

	        avanceretProgramering.AddStudentToExam(jacob,true, examInDecember);

			avanceretProgramering.Exams[0].AddExaminer(kristian);
	        avanceretProgramering.Exams[0].AddExaminer(faisal);

			avanceretProgramering.Exams[0].AddResult(jacob,10);

			avanceretProgramering.Exams[0].RemoveStudentFromExam("Jacob Kjærgaard");

	        avanceretProgramering.Exams[0] = null;

			avanceretProgramering.RemoveTeacherFromCourse("Faisal");

			smartLearning.RemoveCourse("Avanceret Programmering");
			
	        Console.ReadLine();


        }
	}
}
