using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionProgram
{
	public struct Payment
	{
		private DateTime _date;
		private double _amount;

		public Payment(DateTime date, double amount)
		{
			this._date = date;
			this._amount = amount;
		}
	}
}
