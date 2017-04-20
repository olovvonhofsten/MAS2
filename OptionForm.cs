using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MirrorAlignmentSystem
{
    /// <summary>
    ///  This class handles the OptionForm 
    /// </summary>
    public partial class OptionForm : Form
    {
        CameraController cameraController;

        double framerateMax, framerateMin, framerateInc, expMin, expMax, expInc;

        /// <summary>
        /// The class constructor
        /// </summary>
        public OptionForm(CameraController inputCamera)
        {
            InitializeComponent();

            cameraController = inputCamera;
        }

        /// <summary>
        /// Get or set the parent of the window
        /// </summary>
        public MainWindow ParentWindow { get; set; }

        /// <summary>
        /// Event method for when the form is loaded
        /// </summary>
        /// <param name="sender">The control, in this method the form</param> 
        /// <param name="e">The event information</param> 
        private void OptionForm_Load(object sender, EventArgs e)
        {
            string[] DALSettings = DAL.GetSettings("User");

            exposureTimeTB.Text = "" + Math.Round(cameraController.GetExposureTime(), 2);//DALSettings[0];
            thresholdTB.Text = "" + DALSettings[2];
            waitTimeMonitorTB.Text = "" + DALSettings[3];
            waitTimeCycleTB.Text = "" + DALSettings[4];
            xOffsetTB.Text = "" + DALSettings[6];
            yOffsetTB.Text = "" + DALSettings[7];
            framerateTB.Text = "" + Math.Round(cameraController.GetFramerate(), 2);//DALSettings[5];

            cameraController.GetFrameRateRange(out framerateMin, out framerateMax, out framerateInc);
            cameraController.GetExposureTimeRange(out expMin, out expMax, out expInc);

            //trackBar1.Value = (int)(framerateInc * cameraController.GetFramerate());
            //trackBar2.Value = (int)(expInc * cameraController.GetExposureTime());

            trackBar1.Maximum = 100;
            trackBar1.Minimum = 1;
            trackBar1.TickFrequency = 1;

            trackBar1.Value = (int)((99 / (framerateMax - framerateMin)) * Math.Round(cameraController.GetFramerate(), 2));
            //MessageBox.Show("Value: " + (int)((99 / (framerateMax - framerateMin)) * Math.Round(cameraController.GetFramerate(), 2)));

            trackBar2.Maximum = 100;
            trackBar2.Minimum = 1;
            trackBar2.TickFrequency = 1;

            trackBar2.Value = (int)((99 / (expMax - expMin)) * Math.Round(cameraController.GetExposureTime(), 2));
        }

        /// <summary>
        /// Event method for when the user clicks the update button. Calls the DAL class which then updates the settings in the database
        /// </summary>
        /// <param name="sender">The control, in this method the button</param> 
        /// <param name="e">The event information</param> 
        private void button1_Click(object sender, EventArgs e)
        {
			ParentWindow.SettingsUpdated(true);
			if (DAL.UpdateSettings(thresholdTB.Text, waitTimeMonitorTB.Text, waitTimeCycleTB.Text, xOffsetTB.Text, yOffsetTB.Text)) 
            {
               // MessageBox.Show("The settings have been updated");
            }
		}

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            double newFramerate = ((framerateMax - framerateMin) / 99) * trackBar1.Value;

            //MessageBox.Show("newFramerate: " + newFramerate);
            //MessageBox.Show("framerateMin: " + framerateMin + " framerateMax: " + framerateMax + " framerateInc: " + framerateInc);
            cameraController.SetFramerate(newFramerate);
            framerateTB.Text = "" + Math.Round(cameraController.GetFramerate(), 2);
            exposureTimeTB.Text = "" + Math.Round(cameraController.GetExposureTime(), 2);

            cameraController.GetFrameRateRange(out framerateMin, out framerateMax, out framerateInc);
            cameraController.GetExposureTimeRange(out expMin, out expMax, out expInc);

            label12.Text = "" + Math.Round(framerateMin, 2);
            label13.Text = "" + Math.Round(framerateMax, 2);
            label15.Text = "" + Math.Round(expMin, 2);
            label14.Text = "" + Math.Round(expMax, 2);

            //MessageBox.Show("ExpTime: " + cameraController.GetExposureTime());
            if ((int)((99 / (expMax - expMin)) * Math.Round(cameraController.GetExposureTime(), 2)) < trackBar2.Minimum || (int)((99 / (expMax - expMin)) * Math.Round(cameraController.GetExposureTime(), 2)) > trackBar2.Maximum)
            {
            }
            else 
            {
                trackBar2.Value = (int)((99 / (expMax - expMin)) * Math.Round(cameraController.GetExposureTime(), 2));
            }

            //double newExposureTime = ((expMax - expMin) / 99) * trackBar2.Value;

            //cameraController.SetExposureTime(newExposureTime);
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            double newExposureTime = ((expMax - expMin) / 99) * trackBar2.Value;

            cameraController.SetExposureTime(newExposureTime);
            framerateTB.Text = "" + Math.Round(cameraController.GetFramerate(), 2);
            exposureTimeTB.Text = "" + Math.Round(cameraController.GetExposureTime(), 2);

            cameraController.GetFrameRateRange(out framerateMin, out framerateMax, out framerateInc);
            cameraController.GetExposureTimeRange(out expMin, out expMax, out expInc);

            //MessageBox.Show("ExpTime: " + cameraController.GetExposureTime());
        }

        private void framerateTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cameraController.SetFramerate(double.Parse(framerateTB.Text));

                trackBar1.Value = (int)((99 / (framerateMax - framerateMin)) * Math.Round(cameraController.GetFramerate(), 2));

                cameraController.GetFrameRateRange(out framerateMin, out framerateMax, out framerateInc);
                cameraController.GetExposureTimeRange(out expMin, out expMax, out expInc);
            }
        }

        private void exposureTimeTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cameraController.SetFramerate(double.Parse(framerateTB.Text));

                trackBar2.Value = (int)((99 / (framerateMax - framerateMin)) * Math.Round(cameraController.GetExposureTime(), 2));

                cameraController.GetFrameRateRange(out framerateMin, out framerateMax, out framerateInc);
                cameraController.GetExposureTimeRange(out expMin, out expMax, out expInc);
            }
        }
    }
}
