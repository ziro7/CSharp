using System;
using System.IO;
using System.Threading;

namespace Archive
{
	public class FilePrint : ILogger
	{
		private readonly string _path;

		public FilePrint(string path)
		{
			_path = path;
		}

		public void Error(string message, Exception exception)
		{
			Print(message, MessageType.Error);

			using (var streamWriter = new StreamWriter(_path, true))
			{
				streamWriter.WriteLine("stacktrace: " + exception.StackTrace);
			}
		}

		public void LogInfo(string message)
		{
				Print(message, MessageType.Info);
		}

		public void Print(string message, MessageType messageType)
		{
			using (var streamWriter = new StreamWriter(_path, true))
			{
				streamWriter.WriteLine(messageType + ": " + message);
			}
		}
	}
}