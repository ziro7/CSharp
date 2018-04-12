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
		private DepositDistribution _depositDistribution;
		public double PremiumPrYear { get; set; }

		public Premium(double premiumPrYear, double maxOnRate, Maturity maturity)
		{
			this._depositDistribution = new DepositDistribution(maxOnRate);
			this.PremiumPrYear = premiumPrYear;
			this._maturity = maturity;
		}

		public Premium(double premiumPrYear, Maturity maturity)
		{
			this._depositDistribution = new DepositDistribution(false);
			this.PremiumPrYear = premiumPrYear;
			this._maturity = maturity;
		}

		//public double PremiumOnRatePrYear()
		//{
		//	double premiumOnRatePrYear;

		//	if (PremiumPrYear > _depositDistribution.ValueLeftOnRate)
		//	{
		//		return _depositDistribution.maxOnRate;
		//	}

		//	premiumOnRatePrYear = 0; //lav beregning her.
		//	return premiumOnRatePrYear;
		//}

		/*
		 * 	Samlet præmie
			metode der kalder indbetalingsfordeling med præmie. Præmie på rate pr. år 
			metode der kalder indbetalingsfordeling med præmie. Præmie på livrente pr. år
			Metode til at nulstille årlig indbetaling
		 */
	}
}
