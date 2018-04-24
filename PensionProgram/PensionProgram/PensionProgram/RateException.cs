using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionProgram
{
	public class RateException : Exception
	{

		public RateException()
		{
		}
		
		public RateException(string message)
			: base(message)
		{
		}

		public RateException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
