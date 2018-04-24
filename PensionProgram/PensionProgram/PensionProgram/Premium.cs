using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionProgram
{
	public class Premium	
	{
		private Maturity _maturity;
		private double _premium;
		private readonly List<Payment> _payments = new List<Payment>();

		public Premium(double premiumPrMaturity, Maturity maturity)
		{
			this._premium = premiumPrMaturity;
			this.Maturity = maturity;
		}

		public List<Payment> Payments
		{
			get { return _payments; }
		}

		public Maturity Maturity { get => _maturity; set => _maturity = value; }

		public void AddPayment(Payment payment) => _payments.Add(payment);

		public double ShowYearlyPremium()
		{
			if (Maturity == Maturity.Yearly)
			{
				return _premium;
			}
			else
			{
				return _premium * 12;
			}
		}

		public double PaymentsOnTaxCode(Taxcode taxcode)
		{
			double sum = 0;
			for (var i = 0; i < _payments.Count; i++)
			{
				if (_payments[i].Taxcode == taxcode)
				{
					sum += _payments[i].Amount;
				}
			}
			return sum;
		}

	}
}
