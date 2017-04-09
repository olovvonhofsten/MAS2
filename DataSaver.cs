
using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace MirrorAlignmentSystem
{

	class DataBlock
	{
		public string name;
		public int[] vals;
	}

	/// <summary>
	/// holding class for data
	/// </summary>
	public class DataSaver
	{
		/// <summary>
		/// Add a data point
		/// </summary>
		/// <param name="name"></param>
		/// <param name="vals"></param>
		public void AddDataPoint(string name, int[] vals)
		{
			DataBlock db = new DataBlock();
			db.name = (string)name.Clone();
			db.vals = new int[vals.Length];
			vals.CopyTo(db.vals,0);
			blocks.Add(db);
		}

		/// <summary>
		/// clears data
		/// </summary>
		public void ClearAllData()
		{
			blocks.Clear();
		}

		/// <summary>
		/// save data to file
		/// </summary>
		/// <param name="fn"></param>
		public void SaveData(string fn)
		{
			StreamWriter sw = new StreamWriter(fn);
			foreach (var x in blocks)
			{
				DataBlock b = x;
				sw.Write('"');
				sw.Write(b.name);
				sw.Write('"');
				foreach (int i in b.vals)
				{
					sw.Write(',');
					sw.Write(i);
				}
				sw.WriteLine();
			}
		}

		private List<DataBlock> blocks = new List<DataBlock>();
	}




}
