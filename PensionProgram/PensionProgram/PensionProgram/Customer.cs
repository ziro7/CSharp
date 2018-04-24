using System;

namespace PensionProgram
{
	public class Customer
	{
		public string Name { get; }
		public string Fødselsdato { get; }

		public Customer(string name, string fødselsdato)
		{
			Name = name;
			Fødselsdato = fødselsdato;
		}

		public double Age()
		{
			int year = Convert.ToInt32(Fødselsdato.Substring(4));
			int month = Convert.ToInt32(Fødselsdato.Substring(2, 2));
			
			int currentYear = DateTime.Now.Year;
			int currentMonth = DateTime.Now.Month;

			string now = "" + currentYear + "," + currentMonth;
			string born = "" + year + "," + month;

			double age = Convert.ToDouble(now) - Convert.ToDouble(born);

			return age;
		}
	}
}

