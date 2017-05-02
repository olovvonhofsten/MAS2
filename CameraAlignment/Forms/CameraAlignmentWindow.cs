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
using MirrorAlignmentSystem;

namespace CameraAlignment
{
	public partial class CameraAlignmentWindow : Form
	{
		CameraController _cameraController;

		delegate void SetOverviewImageCallback(Bitmap image);


		/// <summary>
		/// The class constructor
		/// </summary>
		public CameraAlignmentWindow(CameraController inputCamera)
		{
			InitializeComponent();

			_cameraController = inputCamera;

			CurrentUser.SetCurrentUser("Admin");
			CurrentUser.SetCurrentUserType("Admin");

			//Create path
			//string pathToday = "C:/MASDATA/" + valueDiscIDTextBox + "/" + System.DateTime.Now.ToString("yyyy_MM_dd");
			//System.IO.Directory.CreateDirectory(pathToday);

			//DiscIDTextBox.Text = "NOT SET";
			//valueDiscIDTextBox = DiscIDTextBox.Text;
			//SegmentNumberTextbox.Text = "0911";
			//valueSegmentNumberTextbox = SegmentNumberTextbox.Text;
			//finePBshow = "sgbg";
			//exposureSlider.Value = 100;

			CheckForIllegalCrossThreadCalls = false;

			//SetSegmentControllerArrows();
			//exposureSlider.SetThumbRect();
			//ShowSegmentId(valueSegmentNumberTextbox);
		}

		/// <summary>
		/// Updates the picturebox which contains the overview image for the segment
		/// </summary>
		/// <param name="image">The image to be displayed in the picturebox</param>    
		public void ShowOverviewBitmap(Bitmap image)
		{
			if (this.CameraAlignment_pictureBox.InvokeRequired)
			{
				SetOverviewImageCallback d = new SetOverviewImageCallback(ShowOverviewBitmap);
				this.Invoke(d, new object[] { image.Clone() });
			}
			else
			{
				CameraAlignment_pictureBox.Image = null;
				CameraAlignment_pictureBox.Image = (Bitmap)image.Clone();
			}
		}

	}
}
