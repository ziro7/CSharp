using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PensionProgram.UnitTests
{
	[TestClass]
	public class UnitTest
	{
		[TestMethod]
		public void ShowYearlyPremium_MonthlyPayment_PremiumTimes12()
		{
			//arrange
			var premiumInstance = new Premium(1000, Maturity.Monthly);

			//act
			var result = premiumInstance.ShowYearlyPremium();

			//assert
			Assert.AreEqual(result, 12000);

		}

		[TestMethod]
		public void ShowYearlyPremium_YearlyPayment_PremiumTimes1()
		{
			//arrange
			var premiumInstance = new Premium(1000, Maturity.Yearly);

			//act
			var result = premiumInstance.ShowYearlyPremium();

			//assert
			Assert.AreEqual(result, 1000);

		}
	}
}
