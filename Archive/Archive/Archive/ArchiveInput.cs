using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archive
{
	public partial class ArchiveInput : Form
	{
		private bool isStudent;
		private bool _asc = true;
		private bool _ascAge = true;
		private string name;
		private int age;
		private string adress;
		private int postNumber;
		private int phoneNumber;
		private School school;
		private string job;
		private double? salary;
		private int phoneNumberToDelete;
		private int findPersonWithPhoneNumber;
		public Archive archive;

		public ArchiveInput()
		{
			InitializeComponent();
			SchoolComboBox.DataSource = Enum.GetValues(typeof(School));
			archive = new Archive(new GuiLogger(), new FilePrint("C:\\log.txt"));
			archive.CreateOrAlterStudent("Jacob", 23, "Hyttekrogen 9", 2665, 24862386, School.SmartLearning);
			archive.CreateOrAlterStudent("Jacob2", 23, "Hyttekrogen 9", 2665, 24862387, School.SmartLearning);
			archive.CreateOrAlterStudent("Jacob3", 35, "Hyttekrogen 9", 2665, 24862388, School.SmartLearning);
			archive.CreateOrAlterStudent("Jacob4", 40, "Hyttekrogen 9", 2665, 24862389, School.SmartLearning);
			archive.CreateOrAlterStudent("Jacob5", 41, "Hyttekrogen 9", 2665, 24862390, School.SmartLearning);
			archive.CreateOrAlterEmployee("Jacob6", 23, "Hyttekrogen 12", 2665, 86377900, "Tester", null);
			archive.CreateOrAlterEmployee("Jacob7", 26, "Hyttekrogen 12", 2665, 86377901, "Tester", 400000);
			archive.CreateOrAlterEmployee("Jacob8", 40, "Hyttekrogen 12", 2665, 86377902, "Tester", 500000);
			archive.CreateOrAlterEmployee("Jacob9", 41, "Hyttekrogen 12", 2665, 86377903, "Tester", 350000);
			dataGridView.DataSource = archive.ShowAllPersons();
		}

		private void CreateOrAlterButton_Click(object sender, EventArgs e)
		{
			isStudent = StudentsRadioButton.Checked;
			name = Convert.ToString(NameTextBox.Text);
			age = Convert.ToInt32(AgeTextBox.Text);
			adress = Convert.ToString(AdressTextBox.Text);
			postNumber = Convert.ToInt32(PostNumberTextBox.Text);
			phoneNumber = Convert.ToInt32(PhoneNumberTextBox.Text);

			if (isStudent)
			{
				school = (School) SchoolComboBox.SelectedItem;
				archive.CreateOrAlterStudent(name, age, adress,postNumber,phoneNumber,school);
			}
			else
			{
				job = Convert.ToString(JobTextBox.Text);

				if (SalaryTextBox.Text == "")
				{
					salary = null;
				}
				else
				{
					try
					{
						salary = Convert.ToDouble(SalaryTextBox.Text);
					}
					catch (Exception exception)
					{
						MessageBox.Show(exception.StackTrace, "Fejl i input vedr. sletningsfelt", MessageBoxButtons.OK);
					}
				}
				archive.CreateOrAlterEmployee(name, age, adress,postNumber,phoneNumber,job, salary);
			}

		}

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			try
			{
				phoneNumberToDelete = Convert.ToInt32(DeletePersonTextBoks.Text);
				archive.DeletePerson(phoneNumberToDelete);
			}
			catch (Exception exception)
			{
				MessageBox.Show("Fejl i input vedr. sletningsfelt", "Fejl i input vedr. sletningsfelt", MessageBoxButtons.OK); 
				throw;
			}
		}

		private void ShowNumberOfPersonButton_Click(object sender, EventArgs e)
		{
			ShowNumbersOfLabel.Text = "Antal af personer er: " + archive.ShowNumberOfPersons();
		}

		private void ShowNumberOfStudentsButton_Click(object sender, EventArgs e)
		{
			ShowNumbersOfLabel.Text = "Antal af studerende er: " + archive.ShowNumberOfPersons(true);
		}

		private void ShowNumberOfEmployeesButton_Click(object sender, EventArgs e)
		{
			ShowNumbersOfLabel.Text = "Antal af medarbejdere er: " + archive.ShowNumberOfPersons(false);
		}

		private void PersonWithPhoneNumberButton_Click(object sender, EventArgs e)
		{
			try
			{
				findPersonWithPhoneNumber = Convert.ToInt32(PersonWithPhoneNumberTextBoks.Text);
				dataGridView.DataSource = archive.FindPersonWithPhoneNumber(findPersonWithPhoneNumber);
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.StackTrace, "Fejl", MessageBoxButtons.OK);
			}
		}

		private void MinAgeButton_Click(object sender, EventArgs e)
		{
			dataGridView.DataSource = archive.ShowPersonsWithAge();
		}

		private void MaxAgeButton_Click(object sender, EventArgs e)
		{
			dataGridView.DataSource = archive.ShowPersonsWithAge(false);
		}

		private void MinSalaryButton_Click(object sender, EventArgs e)
		{
			dataGridView.DataSource = archive.ShowEmployeesWithSalary(true);
		}

		private void MaxSalaryButton_Click(object sender, EventArgs e)
		{
			dataGridView.DataSource = archive.ShowEmployeesWithSalary(false);
		}

		private void EmployeeRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (EmployeeRadioButton.Checked)
			{
				JobTextBox.Enabled = true;
				SalaryTextBox.Enabled = true;
			}
			else
			{
				JobTextBox.Enabled = false;
				SalaryTextBox.Enabled = false;
				JobTextBox.Text = "";
				SalaryTextBox.Text = "";
				}
			}

		private void StudentsRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			SchoolComboBox.Enabled = StudentsRadioButton.Checked;
		}

		private void ShowAllPersonsButton_Click(object sender, EventArgs e)
		{
			dataGridView.DataSource = archive.ShowAllPersons();
		}

		private void SortSalary_Click(object sender, EventArgs e)
		{
			var list = archive.Employees;
			_asc = !_asc;
			if (_asc)
			{
				list.Sort(new SortSalaryAscendingHelper());
			}
			else
			{
				list.Sort(new SortSalaryDescendingHelper());
			}
			dataGridView.DataSource = list;
		}

		private void SortAge_Click(object sender, EventArgs e)
		{
			var list = archive.ShowAllPersons();
			_ascAge = !_ascAge;
			if (_ascAge)
			{
				list.Sort(new SortAgeAscendingHelper());
			}
			else
			{
				list.Sort();
			}
			dataGridView.DataSource = list;
		}
	}
}
