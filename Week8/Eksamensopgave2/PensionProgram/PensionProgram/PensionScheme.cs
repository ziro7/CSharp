using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionProgram
{
	class PensionScheme
	{
		//ordningsnummer
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


		/*
		 * Der laves et PensionScheme klasse
			Construktor laver:	
			-ordningsnummer via ordningnummerering singelton klass
			-kunde objekt. 
			-depot objekt (som igen består opsparingsstack), 
			-præmie objekt.(indbetaling og termin/indbetaingstidspunkt))
			-Indbetalingsfordelings objekt (ren liv? - max på rate?)
			-Udløb på ordning
			-startdato på ordning
				 */

	}
}
