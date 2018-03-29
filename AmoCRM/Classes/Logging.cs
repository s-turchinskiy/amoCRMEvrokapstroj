using System;

namespace AmoCRM.Classes
{
	static class Log
	{
		public static void WriteInfo(string info)
		{
			Console.WriteLine(DateTime.Now.ToString("dd MMMM yyyy | HH:mm:ss") + " " + info);
		}

		public static void WriteError(string info)
		{
			Console.WriteLine(info);
		}
	}

}
