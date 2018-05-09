using System.IO;

namespace Archive
{
	public class FilePrint : IPrint
	{
		private readonly string _path;

		public FilePrint(string path)
		{
			_path = path;
		}


		public void Print(string message)
		{
			//using (var streamWriter = new StreamWriter(_path, true))
			//{
			//	streamWriter.WriteLine(message);
			//}
		}
	}
}