
using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace MirrorAlignmentSystem
{

	class DataBlockInt
	{
		public string name;
		public int[] vals;
	}

	class DataBlockDbl
	{
		public string name;
		public double[] vals;
	}


	/// <summary>
	/// holding class for data
	/// </summary>
	public class DataSaver
	{
		/// <summary>
		/// Add an int data point
		/// </summary>
		/// <param name="name"></param>
		/// <param name="vals"></param>
		public void AddDataPoint(string name, int[] vals)
		{
			DataBlockInt db = new DataBlockInt();
			db.name = (string)name.Clone();
			db.vals = new int[vals.Length];
			vals.CopyTo(db.vals,0);
			iblocks.Add(db);
		}

		/// <summary>
		/// Add a data point
		/// </summary>
		/// <param name="name"></param>
		/// <param name="vals"></param>
		public void AddDataPoint(string name, double[] vals)
		{
			DataBlockDbl db = new DataBlockDbl();
			db.name = (string)name.Clone();
			db.vals = new double[vals.Length];
			vals.CopyTo(db.vals, 0);
			dblocks.Add(db);
		}


		/// <summary>
		/// clears data
		/// </summary>
		public void ClearAllData()
		{
			iblocks.Clear();
			dblocks.Clear();
		}

		/// <summary>
		/// save data to file
		/// </summary>
		/// <param name="fn"></param>
		public void SaveData(string fn)
		{
			StreamWriter sw = new StreamWriter(fn);
			foreach (var x in iblocks)
			{
				DataBlockInt b = x;
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

			foreach (var x in dblocks)
			{
				DataBlockDbl b = x;
				sw.Write('"');
				sw.Write(b.name);
				sw.Write('"');
				foreach (double d in b.vals)
				{
					sw.Write(',');
					sw.Write(d);
				}
				sw.WriteLine();
			}
		}

		private List<DataBlockInt> iblocks = new List<DataBlockInt>();
		private List<DataBlockDbl> dblocks = new List<DataBlockDbl>();

		/// <summary>
		/// singleton
		/// </summary>
		public static DataSaver instance = new DataSaver();
	}




}
