using System.Net.Mime;
using System.Net.WebSockets;


namespace Archive
{
	public class GuiLogger : ILogger
	{
		public string LogMessage { get; set; } = "";

		public void Error(string message)
		{
			LogMessage = message;
		}

		public void LogInfo(string message)
		{
			LogMessage = message;
		}

	}
}

