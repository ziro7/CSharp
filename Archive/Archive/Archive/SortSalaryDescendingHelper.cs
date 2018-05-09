using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Forms.VisualStyles;

namespace Archive
{
	public class SortSalaryDescendingHelper : IComparer<Employee>
	{
		public int Compare(Employee left, Employee right)
		{
			if (left.Salary == null) return 1;
			if (right.Salary == null) return -1;

			if (left.Salary > right.Salary)
				return -1;
			if (left.Salary < right.Salary)
				return 1;
			else
				return 0;
		}
	}

	public class SortSalaryAscendingHelper : IComparer<Employee>
	{
		public int Compare(Employee left, Employee right)
		{
			if (left.Salary == null) return -1;
			if (right.Salary == null) return 1;

			if (left.Salary < right.Salary)
				return -1;
			if (left.Salary > right.Salary)
				return 1;
			else
				return 0;
		}
	}
}
