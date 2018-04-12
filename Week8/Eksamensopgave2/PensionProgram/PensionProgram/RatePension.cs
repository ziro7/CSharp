using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionProgram
{
	class RatePension : SavingsProduct, ISavingProduct
	{
		public int MaxTax2ByLaw { get; set; }
		public int DefaultPayoutPeriode { get; set; }
		
		public void EnEllerAndenSlagsMetode()
		{
			throw new NotImplementedException();
		}

		public override void HvadSkalAlleOpSparingerKunne()
		{
			throw new NotImplementedException();
		}
	}
}
