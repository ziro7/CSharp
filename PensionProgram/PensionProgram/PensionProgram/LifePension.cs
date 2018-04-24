using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PensionProgram
{
	class LifePension : SavingProduct
	{
		private double _deposit;
		public const double Omregningsfaktor = 15.554449;

		public override int MaxOnRate()
		{
			return 0;
		}

		public override bool HaveRate()
		{
			return false;
		}
	}
}
