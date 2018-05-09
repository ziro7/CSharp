using System.Collections;
using System.Collections.Generic;

namespace Archive
{
	public class SortAgeAscendingHelper : IComparer<Person>
	{
		public int Compare(Person left, Person right)
		{
			return right.Age - left.Age;
		}
	}
}