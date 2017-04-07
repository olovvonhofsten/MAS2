using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MirrorAlignmentSystem
{
	/// <summary> CheckAllForm </summary>
	public partial class CheckAllForm : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CheckAllForm"/> class.
		/// </summary>
		public CheckAllForm()
		{
			InitializeComponent();
		}

		private void CheckAllForm_Load(object sender, EventArgs e)
		{
			Start();
		}

		private int progress;

		/// <summary>
		/// Starts this instance.
		/// </summary>
		public void Start()
		{
			//this.
			WindowState = FormWindowState.Maximized;
			progress = 0;
			progressBar1.Value = 0;
			progressBar1.Visible = true;
			btnClose.Visible = false;
			CheckForIllegalCrossThreadCalls = false;
		}

		private void CheckAllForm_Shown(object sender, EventArgs e)
		{
			Start();
		}

		/// <summary>
		/// Waits the close.
		/// </summary>
		public void WaitClose()
		{
			while(true)
			{
				Thread.Sleep(150);
				Update();
				Application.DoEvents();
				if (!Visible)
					break;
			}
		}

		/// <summary>
		/// Sets the progress.
		/// </summary>
		/// <param name="p">The progress</param>
		public void SetProgress(int p)
		{
			progress = p;
			progressBar1.Value = p;
		}

		/// <summary>
		/// Sets the image.
		/// </summary>
		/// <param name="bmp">The BMP.</param>
		public void SetImage(Bitmap bmp)
		{
			progressBar1.Visible = false;
			pictureBox1.Image =(Bitmap) bmp.Clone();
			btnClose.Visible = true;
			Update();
		}

		/// <summary>
		/// sets Title to t.
		/// </summary>
		/// <param name="t">The title</param>
		public void Title(string t)
		{
			lblTitle.Text = t;
		}

		/// <summary>
		/// Closes down.
		/// </summary>
		public void CloseDown()
		{
			DialogResult = System.Windows.Forms.DialogResult.OK;
			Visible = false;
			Close();
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			button1.Top += 1;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			CloseDown();
		}
	}
}


