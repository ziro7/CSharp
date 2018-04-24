using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionProgram
{
	public sealed class PensionsSchemeNumber
	{
		private int _nextNumber = 800001;
		private static readonly PensionsSchemeNumber instance = new PensionsSchemeNumber();

		private PensionsSchemeNumber()
		{
		}

		public static PensionsSchemeNumber Instance
		{
			get
			{
				return instance;
			}
		}

		public int GetNextNumber()
		{
			int OccupiedNumber = _nextNumber;
			_nextNumber++;
			return OccupiedNumber;
		}


	}
}
