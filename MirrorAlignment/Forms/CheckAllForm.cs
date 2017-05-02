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
using Emgu.CV;
using Emgu.CV.Structure;

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
		/// Sets the image in imagebox1.
		/// </summary>
		/// <param name="img">The image.</param>
        public void SetImage1(Image<Bgr, byte> img)
		{
			progressBar1.Visible = false;
			imageBox1.Image = img.Clone();
			btnClose.Visible = true;
			Update();
		}

        /// <summary>
        /// Sets the image in imagebox1.
        /// </summary>
        /// <param name="img">The image.</param>
        public void SetImage2(Image<Bgr, byte> img)
        {
            progressBar1.Visible = false;
            imageBox2.Image = img.Clone();
            Update();
        }
        /// <summary>
        /// Sets the image in imagebox1.
        /// </summary>
        /// <param name="img">The image.</param>
        public void SetImage3(Image<Bgr,byte> img)
        {
            progressBar1.Visible = false;
            imageBox3.Image = img.Clone();
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
        /// sets Title to pictureboxes.
        /// </summary>
        /// <param name="t">The title</param>
        public void PbTitles(string [] t)
        {
            label1.Text = t[0];
            label2.Text = t[1];
            label3.Text = t[2];
        }

		/// <summary>
		/// Closes down.
		/// </summary>
		public void CloseDown()
		{
			DialogResult = System.Windows.Forms.DialogResult.OK;
			Visible = false;
			//Close();
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			CloseDown();
		}
	}
}


