
using System.IO;
using System.Windows.Forms;


namespace MirrorAlignmentSystem
{

	/// <summary>
	/// 
	/// </summary>
	public static class LocalSettings
	{

		private static string GetVal(StreamReader sr, string name)
		{
			if (sr==null) return null;
			string val = null;

			while (true)
			{
				string s = sr.ReadLine();
				if (s==null) break;
				string[] arr = s.Split('#');
				if (arr.Length != 2) continue;
				if (arr[0].Trim() == name)
					val = arr[1].Trim();
			}

			return val;
		}

		private static string GetVal(StreamReader sett, StreamReader loc, string name, string def="")
		{
			string s, val = def;
			s = GetVal(sett, name);
			if (s!=null)
				val = s;
			s = GetVal(loc, name);
			if (s!=null)
				val = s;
			return val;
		}

		/// <summary>
		/// SQLs the string.
		/// </summary>
		/// <returns></returns>
		public static string SQLString()
		{
			StreamReader sett = null, loc = null;

			//string lwd = Directory.GetCurrentDirectory();
			//MessageBox.Show(lwd);

			try
			{
				sett = new StreamReader("settings.txt");
			} catch {}


			try { loc = new StreamReader("local.txt"); }
			catch { }

			return GetVal(sett, loc, "SQLString");
		}

	}


}

