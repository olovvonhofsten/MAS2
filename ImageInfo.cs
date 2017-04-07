using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MirrorAlignmentSystem
{
    /// <summary>
    ///  This class handles the Image the user choose from the image history form
    /// </summary>
    public partial class ImageInfo : Form
    {
        /// <summary>
        /// The class constructor
        /// </summary>
        public ImageInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event method for when the form is loaded
        /// </summary>
        /// <param name="sender">The control, in this method the form</param> 
        /// <param name="e">The event information</param> 
        private void ImageInfo_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Displays the image information and the image to the user inside the form
        /// </summary>
        /// <param name="img">The image</param> 
        /// <param name="timestamp">The timestamp of the when the image was inserted into the database</param> 
        /// <param name="expTime">Exposure time when the image was captured</param> 
        /// <param name="threshold">Threshold when the image was captured</param> 
        /// <param name="cameraID">The camera ID of the camera that took the image</param> 
        public void LoadNewImage(Bitmap img, string timestamp, string expTime, string threshold, string cameraID) 
        {
        }
    }
}
