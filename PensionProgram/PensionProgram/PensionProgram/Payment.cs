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
		private Taxcode _taxcode;

		public double Amount
		{
			get { return _amount; }
			set { _amount = value; }
		}

		public Taxcode Taxcode
		{
			get { return _taxcode; }
			set { _taxcode = value; }
		}

		public Payment(DateTime date, double amount, Taxcode taxcode)
		{
			this._date = date;
			this._amount = amount;
			this._taxcode = taxcode;
		}
	}
}
