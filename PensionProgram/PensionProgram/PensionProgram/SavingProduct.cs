using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionProgram
{
	public abstract class SavingProduct
	{
		private double _deposit;

		public virtual double CurrentDeposit()
		{
			return _deposit;
		}

		public virtual void AddDeposit(double payment)
		{
			_deposit += payment;
		}

		public abstract int MaxOnRate();

		public abstract bool HaveRate();

	}
}

