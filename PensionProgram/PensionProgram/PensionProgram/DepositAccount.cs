using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PensionProgram
{
	class DepositAccount
	{
		private readonly List<SavingProduct> _savingProducts;
		private bool _thereIsRatePension = false;

		public DepositAccount()
		{
			_savingProducts = new List<SavingProduct>();
			_savingProducts.Add(new LifePension());
		}

		public void AddRatePension(int maxTax2, int defaultPayoutPeriode = 10)
		{
			_savingProducts.Add(new RatePension(maxTax2,defaultPayoutPeriode));
			_thereIsRatePension = true;
		}

		public bool ThereIsRatePension
		{
			get { return _thereIsRatePension; }
		}

		public int Max2Amount()
		{
			int amount = 0;
			for (var i = 0; i < _savingProducts.Count; i++)
			{
				amount += _savingProducts[i].MaxOnRate();
			}
			return amount;
		}

		public double GetCurrentValue()
		{
			double life = _savingProducts[0].CurrentDeposit();
			if (_savingProducts.Count<2)
			{
				return life;
			}

			else
			{
				return life + _savingProducts[1].CurrentDeposit();
			}
		}

		public void AddPaymentToDepot(double payment, Taxcode taxcode)
		{
			switch (taxcode)
			{
				case Taxcode.Ratepension:
					for (var i = 0; i < _savingProducts.Count; i++)
					{
						if (_savingProducts[i].HaveRate()==true)
							_savingProducts[i].AddDeposit(payment);
					}

					break;
				case Taxcode.Livrente:
					for (var i = 0; i < _savingProducts.Count; i++)
					{
						if (_savingProducts[i].HaveRate()==false)
							_savingProducts[i].AddDeposit(payment);
					}

					break;
			}
			
		}

		public double GetCurrentLife()
		{
			return _savingProducts[0].CurrentDeposit();
		}

		public double GetCurrentRate()
		{
			double depositRate = 0;
			for (var i = 0; i < _savingProducts.Count; i++)
			{
				if (_savingProducts[i].HaveRate() == true)
					depositRate += _savingProducts[i].CurrentDeposit();
			}
			return depositRate;
		}
	}
}
