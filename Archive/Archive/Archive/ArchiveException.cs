using System;

namespace Archive
{
	public class ArchiveException : Exception
	{

		public ArchiveException()
		{
		}

		public ArchiveException(string message)
			: base(message)
		{
		}

		public ArchiveException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}