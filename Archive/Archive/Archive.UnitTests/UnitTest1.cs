using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Archive.UnitTests
{
	[TestClass]
	public class UnitTest1
	{
		Archive archive = new Archive(new FilePrint(@"C:\\Zirodevelopment\Csharp\logTest.txt"));

		[TestMethod]
		public void CreateOrAlterStuden_StudenExsist_StudentChanged()
		{
			//arrange
			archive.CreateOrAlterStudent("Jens", 20, "abs 12", 2500, 9999999, School.AalborgUniversity);

			//act
			archive.CreateOrAlterStudent("Jens", 20, "abs 12", 2500, 9999999, School.AarhusUniversity);
			var result = archive.Students[0].School;

			//assert
			Assert.AreEqual(result, School.AarhusUniversity); 
		}

		[TestMethod]
		public void CreateOrAlterStuden_StudenIsNew_StudentCreated()
		{
			//arrange
			var startUp = archive.Students.Count;

			//act
			archive.CreateOrAlterStudent("Kurt", 20, "abs 12", 2500, 9999999, School.AarhusUniversity);
			var result = archive.Students.Count;

			//assert
			Assert.AreEqual(result, startUp+1);
		}

		[TestMethod]
		public void CreateOrAlterEmployee_EmployeeExsist_EmployeeChanged()
		{
			//arrange
			archive.CreateOrAlterEmployee("Jensen", 20, "abs 12", 2500, 9999999, "tester",null);

			//act
			archive.CreateOrAlterEmployee("Jensen", 20, "abs 12", 2500, 9999999, "tester", 300000);
			var result = archive.Employees[0].Salary;

			//assert
			Assert.AreEqual(result, 300000);
		}

		[TestMethod]
		public void CreateOrAlterEmployee_EmployeeIsNew_EmployeeCreated()
		{
			//arrange
			var startUp = archive.Employees.Count;

			//act
			archive.CreateOrAlterEmployee("Jensen", 20, "abs 12", 2500, 9999999, "tester", 300000);
			var result = archive.Employees.Count;

			//assert
			Assert.AreEqual(result, startUp + 1);
		}

		[TestMethod]
		public void DeletePerson_DeleteStudent_StudentIsDeleted()
		{
			//arrange
			archive.CreateOrAlterStudent("Kurt", 20, "abs 12", 2500, 99999999, School.AarhusUniversity);
			var startUp = archive.Students.Count;

			//act
			archive.DeletePerson(99999999);
			var result = archive.Students.Count;

			//assert
			Assert.AreEqual(result, startUp - 1);
		}

		[TestMethod]
		public void DeletePerson_DeleteEmploee_EmployeeIsDeleted()
		{
			//arrange
			archive.CreateOrAlterEmployee("Jensen", 20, "abs 12", 2500, 99999999, "tester", 300000);
			var startUp = archive.Employees.Count;

			//act
			archive.DeletePerson(99999999);
			var result = archive.Employees.Count;

			//assert
			Assert.AreEqual(result, startUp - 1);
		}

		[TestMethod]
		public void DeletePerson_NotFound_NothingIsDeleted()
		{
			//arrange
			archive.CreateOrAlterStudent("Kurt", 20, "abs 12", 2500, 99999999, School.AarhusUniversity);
			var startUp = archive.ShowNumberOfPersons();

			//act
			archive.DeletePerson(99991239);
			var result = archive.ShowNumberOfPersons();

			//assert
			Assert.AreEqual(result, startUp);
		}

		[TestMethod]
		public void ShowNumberOfPersons_NoInput_2persons()
		{
			//arrange
			archive.CreateOrAlterStudent("Kurt", 20, "abs 12", 2500, 99999999, School.AarhusUniversity);
			archive.CreateOrAlterEmployee("Jensen", 20, "abs 12", 2500, 99999999, "tester", 300000);

			//act
			var result = archive.ShowNumberOfPersons();

			//assert
			Assert.AreEqual(result, 2);
		}

		[TestMethod]
		public void ShowNumberOfPersons_True_person()
		{
			//arrange
			archive.CreateOrAlterStudent("Kurt", 20, "abs 12", 2500, 99999999, School.AarhusUniversity);
			archive.CreateOrAlterEmployee("Jensen", 20, "abs 12", 2500, 99999999, "tester", 300000);

			//act
			var result = archive.ShowNumberOfPersons(true);

			//assert
			Assert.AreEqual(result, 1);
		}

		[TestMethod]
		public void ShowNumberOfPersons_False_person()
		{
			//arrange
			archive.CreateOrAlterStudent("Kurt", 20, "abs 12", 2500, 99999999, School.AarhusUniversity);
			archive.CreateOrAlterEmployee("Jensen", 20, "abs 12", 2500, 99999999, "tester", 300000);

			//act
			var result = archive.ShowNumberOfPersons(false);

			//assert
			Assert.AreEqual(result, 1);
		}

	}


}
