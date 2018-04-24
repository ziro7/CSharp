using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionProgram
{
	class PensionScheme
	{
		public int PensionsSchemeNumber { get; }
		public Customer Customer { get; }
		public DepositAccount DepositAccount { get; }
		public Premium Premium { get; }
		public int ExpirationYear { get; set; }
		public DateTime StartDate { get; }

		public PensionScheme(Customer customer, DepositAccount depositAccount, Premium premium, int expirationYear, DateTime StartDate, PensionsSchemeNumber pensionsSchemeNumber)
		{
			this.PensionsSchemeNumber = pensionsSchemeNumber.GetNextNumber();
			this.Customer = customer;
			this.DepositAccount = depositAccount;
			this.Premium = premium;
			this.ExpirationYear = expirationYear;
			this.StartDate = StartDate;
		}

		public double ShowYearlyPremium()
		{
			return Premium.ShowYearlyPremium();
		}

		public DateTime ShowExpirationDate()
		{
			return StartDate.AddYears((int)ExpirationYear-(int)Customer.Age());
		}

		public double ShowDepositAccount()
		{
			return DepositAccount.GetCurrentValue();
		}
	}
}
