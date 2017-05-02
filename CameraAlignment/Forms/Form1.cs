using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

namespace CameraAlignment
{
	public partial class Form1 : Form
	{
		private Capture _capture;
		public Form1()
		{
			InitializeComponent();

			//try to create the capture

			if (_capture == null)
			{

				try
				{

					_capture = new Capture();

				}

				catch (NullReferenceException excpt)
				{   //show errors if there is any

					MessageBox.Show(excpt.Message);

				}

			}



			if (_capture != null) //if camera capture has been successfully created
			{
				_capture.ImageGrabbed += ProcessFrame;
				_capture.Start();

			}
		}
		private void ProcessFrame(object sender, EventArgs e)
		{
			Image<Gray, Byte> image = new Image<Gray,byte>(1936,1216);
			_capture.Retrieve(image, 0);
			{
				imageBox1.Image = image;
			}
		}

	}
}
