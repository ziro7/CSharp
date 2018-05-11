using System;

namespace Archive
{
	public interface ILogger
	{
		void Error(string message, Exception exception);
		void LogInfo(string message);
	}
}