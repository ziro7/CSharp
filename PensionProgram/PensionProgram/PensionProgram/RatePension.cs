using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionProgram
{
	class RatePension : SavingProduct
	{
		private int _maxTax2ByLaw = 50000;
		private int _maxTax2;
		private int _defaultPayoutPeriode = 10;
		private double _deposit;

		public int MaxTax2ByLaw { get; private set; }

		public RatePension(int maxTax2, int defaultPayoutPeriode = 10)
		{
			MaxTax2 = maxTax2;
			this._defaultPayoutPeriode = defaultPayoutPeriode;
			this._deposit = 0;
		}

		public int DefaultPayoutPeriode
		{
			get { return _defaultPayoutPeriode; }
			set { _defaultPayoutPeriode = value; }
		}

		public int MaxTax2
		{
			get{return _maxTax2;}
			set
			{
				if (value < _maxTax2ByLaw)
				{
					_maxTax2 = value;
				} else
				{
					throw new RateException("Du kan ikke sætte en grænse på Ratepension der overstiger: " + _maxTax2ByLaw);
				}
			}
		}

		public override int MaxOnRate()
		{
			return _maxTax2;
		}

		public override bool HaveRate()
		{
			return true;
		}
	}
}
