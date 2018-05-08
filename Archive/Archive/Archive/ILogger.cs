namespace Archive
{
	public interface ILogger
	{
		void Error(string message);
		void LogInfo(string message);
	}
}