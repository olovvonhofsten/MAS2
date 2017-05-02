using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows;

namespace MirrorAlignmentSystem
{
	/// <summary>
	///  This class Handles the Graphic User Interface and everything associated with it
	/// </summary>
	public partial class MainWindow : Form
	{
		string Alignment = "over";
		bool manualAlignment = true;
		delegate void SetTextCallback(int i);
		delegate void SetEnableCallback(int i);
		delegate void ShowFPSCallback(float FPS);
		delegate void SetDirLeftRightLabelCallback(string dir);
		delegate void SetDirUpDownLabelCallback(string dir);
		delegate void SetImagesCallback();
		//delegate void SetBlackBackgroundImageCallback(Bitmap image);
		//delegate void SetBitmapBackgroundImageCallback(Bitmap image);
        delegate void SetCalibrationImageCallback(Bitmap image);
		delegate void SetCombinedImageCallback(Bitmap image);
		delegate void SetLeftImageCallback(Bitmap image);
		delegate void SetRightImageCallback(Bitmap image);
		delegate void SetUpImageCallback(Bitmap image);
		delegate void SetDownImageCallback(Bitmap image);
		delegate void SetMotor1Callback(string motor1Pos);
		delegate void SetOnOffButtonCallback(string onOff);
		delegate void SetMotor1AlarmCallback(string motor1Alarm);
		delegate void SetMotor1SPReachedCallback(string motor1SPReached);
		delegate void SetMotor2Callback(string motor2Pos);
		delegate void SetOnOff2ButtonCallback(string onOff);
		delegate void SetMotor2AlarmCallback(string motor2Alarm);
		delegate void SetMotor2SPReachedCallback(string motor2SPReached);
		delegate void SetMotor1MDIActiveCallback(string motor1MDIActive);
		delegate void SetMotor2MDIActiveCallback(string motor2MDIActive);
		delegate void SetCoMLabelActiveCallback(string CoM, Point realCoMInput);
		delegate void SetvisibilityCallback(bool mode);
           
		string valueBlackBGNumberTextBox;
		string valueSegmentNumberTextbox;
        string valueLiveCheckState;

		Point realCoM;

		string currentUser = "Admin";
		string currentUserType = "Admin";

		bool settingsUpdated = false;
		bool manualUpdateOverview = false;

		CameraController cameraController;

		/// <summary>
		/// The class constructor
		/// </summary>
		public MainWindow(CameraController inputCamera)
		{
			InitializeComponent();

			cameraController = inputCamera;

			CurrentUser.SetCurrentUser("Admin");
			CurrentUser.SetCurrentUserType("Admin");

			BlackBGNumberTextBox.Text = "1";
			valueBlackBGNumberTextBox = BlackBGNumberTextBox.Text;
			SegmentNumberTextbox.Text = "0911";
			valueSegmentNumberTextbox = SegmentNumberTextbox.Text;

			//this.ActiveControl = BlackBGNumberLabel;
		}

		/// <summary>
		/// Lets the GUI know that the settings have been updated or not
		/// </summary>
		/// <param name="value">Sets the settingsUpdated variable depeding on what the application are requesting</param>    
		public void SettingsUpdated(bool value)
		{
			settingsUpdated = value;
		}

		/// <summary>
		/// Disposes the images on display
		/// </summary>
		public void DisposeImages()
		{
            if (this.CalibrateImgPB.InvokeRequired)
			{
				SetImagesCallback d = new SetImagesCallback(DisposeImages);
				this.Invoke(d, new object[] { });
			}
			else
			{
                CalibrateImgPB.Image.Dispose();
			}
		}

		/// <summary>
		/// Returns the current value of the settingsUpdated variable, if this one is true the settings needs to be updated
		/// </summary>
		/// <returns>
		/// Returns a boolean indicating if the settings have been updated
		/// </returns>
		public bool IsSettingsUpdated() 
		{
			return settingsUpdated;
		}

		/// <summary>
		/// This method updates all the GUI elements that needs to be updated every cycle of the worker class
		/// </summary>
		/// <param name="i">A counter that indicates to the user that the worker and GUI classes are working with eachother.
		/// The worker count and the GUI displays the new value to the user every cycle.</param>    
		public void UpdateGUI(int i) 
		{
			currentUser = CurrentUser.GetCurrentUser();
			currentUserType = CurrentUser.GetCurrentUserType();

			if (currentUser.Trim() != "")
			{
				if (String.Compare(currentUserType.Trim(), "Admin") == 0)
				{
					if (this.BlackBGNumberTextBox.InvokeRequired || this.SegmentNumberTextbox.InvokeRequired)
					{
						SetEnableCallback e = new SetEnableCallback(UpdateGUI);
						this.Invoke(e, new object[] { i });
					}
					else
					{
						this.BlackBGNumberTextBox.Enabled = true;
						this.SegmentNumberTextbox.Enabled = true;
					}
				}
				else 
				{
					if (this.BlackBGNumberTextBox.InvokeRequired || this.SegmentNumberTextbox.InvokeRequired)
					{
						SetEnableCallback e = new SetEnableCallback(UpdateGUI);
						this.Invoke(e, new object[] { i });
					}
					else
					{
						this.BlackBGNumberTextBox.Enabled = false;
						this.SegmentNumberTextbox.Enabled = false;
					}
				}
			}
			else 
			{
				if (this.BlackBGNumberTextBox.InvokeRequired || this.SegmentNumberTextbox.InvokeRequired)
				{
					SetEnableCallback e = new SetEnableCallback(UpdateGUI);
					this.Invoke(e, new object[] { i });
				}
				else
				{

				}
			}
            
		}

		/// <summary>
		/// Updates the picturebox which contains the overview image for the segment
		/// </summary>
		/// <param name="image">The image to be displayed in the picturebox</param>    
		public void ShowOverviewBitmap(Bitmap image)
		{
			if (overviewImagePB.Image != null)
			{
				overviewImagePB.Image.Dispose();
			}

			overviewImagePB.Image = image;
		}

        /// <summary>
        /// Updates the picturebox which contains the calibration image
        /// </summary>
        /// <param name="image">The image to be displayed in the picturebox</param>    
        public void ShowCalibrateBitmap(Bitmap image)
        {
            if (this.CalibrateImgPB.InvokeRequired)
            {
                SetCalibrationImageCallback d = new SetCalibrationImageCallback(ShowCalibrateBitmap);
                this.Invoke(d, new object[] { image });
            }

            CalibrateImgPB.Image = null;
            CalibrateImgPB.Image = (Bitmap)image.Clone();
        }

		/// <summary>
		/// Updates the picturebox which contains the image of the combined image calculated by the algorithm during fine alignment
		/// </summary>
		/// <param name="image">The image to be displayed in the picturebox</param>    
		public void ShowCombinedBitmap(Bitmap image)
		{
			if (this.combinedImagePB.InvokeRequired)
			{
				SetCombinedImageCallback d = new SetCombinedImageCallback(ShowCombinedBitmap);
				this.Invoke(d, new object[] { image });
			}
			else
			{
				combinedImagePB.Image = null;

				combinedImagePB.Image = (Bitmap)image.Clone(); //image;
			}
		}

		/// <summary>
		/// This method returns the value currently in the textbox for interval between black background and bitmap on the monitor
		/// </summary>
		/// <returns>
		/// Returns the number(as a string) of intervall between the monitor showing a bitmap and a black background, only works in Fine Alignment phase
		/// </returns>
		public string GetBlackBGNumber() 
		{
			return valueBlackBGNumberTextBox;
		}

        public string GetLiveMode()
        {
            return valueLiveCheckState;
        }

		/// <summary>
		/// This method returns the value of the textbox containing the segment the user want to run at the moment
		/// </summary>
		/// <returns>
		/// Returns the number(as a string) of the segment that the user wants to run the algorithm on at the moment
		/// </returns>
		public string GetSegmentNumber()
		{
			return valueSegmentNumberTextbox;
		}

		/// <summary>
		/// This method returns the current online/offline mode
		/// </summary>
		/// <returns>
		/// The online/offline mode
		/// </returns>
		public bool GetOfflineOnlineMode() 
		{
            return false;
		}

		/// <summary>
		/// This method returns the current fine/coarse alignment mode
		/// </summary>
		/// <returns>
		/// The fine/coarse alignment mode
		/// </returns>
		public string GetAlignmentMode() 
		{
			return Alignment;
		}


		/// <summary>
		/// This method returns true if the application is in manual alignment mode
		/// </summary>
		/// <returns>
		/// The alignment mode
		/// </returns>
		public bool GetManualAlignment()
		{
			return manualAlignment;
		}

		/// <summary>
		/// Event method for when the user presses the enter button inside the textbox displaying the number of cycles between each black image on the monitor
		/// </summary>
		/// <param name="sender">The control, in this method the textbox</param> 
		/// <param name="e">The event information</param>  
		private void BlackBGNumberTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter && (valueBlackBGNumberTextBox != BlackBGNumberTextBox.Text))
			{
				//this.ActiveControl = BlackBGNumberLabel;

				DAL.InsertEvent(BlackBGNumberTextBox.Text, valueBlackBGNumberTextBox, "No user", "Intervall black background change", "BlackBGNumberTextBox");

				valueBlackBGNumberTextBox = BlackBGNumberTextBox.Text;
			}
		}

		/// <summary>
		/// Event method for when the user presses the enter button inside the textbox displaying the current segment number being worked on
		/// </summary>
		/// <param name="sender">The control, in this method the textbox</param> 
		/// <param name="e">The event information</param>  
		private void SegmentNumberTextbox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter && (valueSegmentNumberTextbox != SegmentNumberTextbox.Text))
			{
				//this.ActiveControl = SegmentNumberLabel;

				DAL.InsertEvent(SegmentNumberTextbox.Text, valueSegmentNumberTextbox, CurrentUser.GetCurrentUser(), "Segment changed", "SegmentNumberTextbox");

				valueSegmentNumberTextbox = SegmentNumberTextbox.Text;
			}
		}

		/// <summary>
		/// Event method for when the user clicks the eventlog button in the menu. Displays the eventlog to the user
		/// </summary>
		/// <param name="sender">The control, in this method the menuitem</param> 
		/// <param name="e">The event information</param>  
		private void eventLogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Eventlog eventForm = new Eventlog();

			eventForm.TopMost = true;
			eventForm.Show();
		}

		private void ShutDownProper()
		{
			//cameraController.StopChecking();

			Visible = false;

			System.Threading.Thread.Sleep(1);

			DAL.UpdateFPSExp(cameraController.GetExposureTime(), cameraController.GetFramerate());

			Application.Exit();
			System.Environment.Exit(0);

		}

		/// <summary>
		/// Event method for when the user clicks the exit button in the menu. Terminates the program
		/// </summary>
		/// <param name="sender">The control, in this method the menuitem</param> 
		/// <param name="e">The event information</param> 
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShutDownProper();
		}

		/// <summary>
		/// Event method for when the user clicks the login button. Creates a new LoginForm
		/// </summary>
		/// <param name="sender">The control, in this method the button</param> 
		/// <param name="e">The event information</param> 
		private void button1_Click(object sender, EventArgs e)
		{
			Login loginForm = new Login();

			loginForm.TopMost = true;

			loginForm.Show();
		}

		/// <summary>
		/// Event method for when the user clicks the logout button. Calls the CurrentUser class and logouts the user
		/// </summary>
		/// <param name="sender">The control, in this method the button</param> 
		/// <param name="e">The event information</param> 
		private void logoutButton_Click(object sender, EventArgs e)
		{
			CurrentUser.Logout();
		}

		/// <summary>
		/// Event method for when the user clicks the administrate button in the menu. creates a new AdministrateUserForm if the current user is the a admin
		/// </summary>
		/// <param name="sender">The control, in this method the menuitem</param> 
		/// <param name="e">The event information</param> 
		private void administrateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if(String.Compare(currentUserType.Trim(), "Admin") == 0)
			{
				AdministrateUsersForm adminForm = new AdministrateUsersForm();

				adminForm.Show();
				adminForm.TopMost = true;
			}
			else
			{
				MessageBox.Show("You are not the administrator");
			}
		}

		private void SetToFine()
		{
			Alignment = "fine";
			BIA_timer.Enabled = false;

			DAL.InsertEvent("1", "0", CurrentUser.GetCurrentUser(), "Fine alignment activated", "fineRadioButton");
		}

		private void SetToCoarse()
		{
			Alignment = "coarse";
			BIA_timer.Enabled = true;
			DAL.InsertEvent("0", "1", CurrentUser.GetCurrentUser(), "Coarse alginment deactivated", "coarceRadioButton");
		}


		/// <summary>
		/// Event method for when the user clicks the option button in the menu. creates a new OptionForm if the current user is the a admin
		/// </summary>
		/// <param name="sender">The control, in this method the menuitem</param> 
		/// <param name="e">The event information</param> 
		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (String.Compare(currentUserType.Trim(), "Admin") == 0)
			{
				OptionForm optionForm = new OptionForm(cameraController);

				optionForm.ParentWindow = this;

				optionForm.Show();
				optionForm.TopMost = true;
			}
			else
			{
				MessageBox.Show("You are not the administrator");
			}
		}

		/// <summary>
		/// Event method for when the user clicks the image history button in the menu. creates a new ImageHistory form if the current user is the a admin
		/// </summary>
		/// <param name="sender">The control, in this method the menuitem</param> 
		/// <param name="e">The event information</param> 
		private void imagesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (String.Compare(currentUserType.Trim(), "Admin") == 0)
			{
				ImageHistory imgHistory = new ImageHistory();

				imgHistory.Show();
				imgHistory.TopMost = true;
			}
			else
			{
				MessageBox.Show("You are not the administrator");
			}
		}

		float analyze_image(Bitmap img)
		{
			return 0.5f;
			/*
			int w = img.Width;
			int h = img.Height;

			float rs = 0.0f, ls = 0.0f;

			for (int y = 0; y < w; ++y )
			{
				for(int x=0; x<h; ++x)
				{
					var pix = img.GetPixel(x, y);
					float b = pix.GetBrightness();
					if (x > (w / 2))
						rs += b;
					else
						ls += b;
				}
			}
			return (rs - ls) / (w * h);
			*/
		}

		BIA bia_r1 = new BIA();
		MailBox mb_r1 = new MailBox();
		BIA bia_r2 = new BIA();
		MailBox mb_r2 = new MailBox();

		private void timer1_Tick(object sender, EventArgs e)
		{
			string s;
			s = mb_r1.ReadMessage();
			if (s != null)
			{
				lbl_lr_1.Text = s;
			}
			s = mb_r2.ReadMessage();
			if (s != null)
			{
				lbl_lr_2.Text = s;
			}
		}

		/// <summary>
		/// Updates the picturebox which contains the image of the segment when the left bitmap is shown on the monitor, only during coarse alignment
		/// </summary>
		/// <param name="image">The image to be displayed in the picturebox</param>   
		public void ShowLeftRightOneBackgroundBitmap(Bitmap image)
		{
			if (this.leftRightPBOne.InvokeRequired)
			{
				SetLeftImageCallback d = new SetLeftImageCallback(ShowLeftRightOneBackgroundBitmap);
				this.Invoke(d, new object[] { image });
			}
			else
			{
				leftRightPBOne.Image = null;

				leftRightPBOne.Image = new Bitmap(image); //image;

				bia_r1.StartAnalyze(image, mb_r1);

				//float f = analyze_image(new Bitmap(image));
				//lbl_lr_1.Text = f.ToString("0.0");
			}
		}

		/// <summary>
		/// Updates the picturebox which contains the image of the segment when the right bitmap is shown on the monitor, only during coarse alignment
		/// </summary>
		/// <param name="image">The image to be displayed in the picturebox</param>  
		public void ShowLeftRightTwoBackgroundBitmap(Bitmap image)
		{
			if (this.leftRightPBTwo.InvokeRequired)
			{
				SetRightImageCallback d = new SetRightImageCallback(ShowLeftRightTwoBackgroundBitmap);
				this.Invoke(d, new object[] { image });
			}
			else
			{
				leftRightPBTwo.Image = null;

				leftRightPBTwo.Image = new Bitmap(image); //image;

				bia_r2.StartAnalyze(image, mb_r2);

				//float f = analyze_image(new Bitmap(image));
				//lbl_lr_2.Text = f.ToString("0.0");
			}
		}

		/// <summary>
		/// Updates the picturebox which contains the image of the segment when the up bitmap is shown on the monitor, only during coarse alignment
		/// </summary>
		/// <param name="image">The image to be displayed in the picturebox</param>  
		public void ShowUpDownOneBackgroundBitmap(Bitmap image)
		{
			if (this.upDownPBOne.InvokeRequired)
			{
				SetUpImageCallback d = new SetUpImageCallback(ShowUpDownOneBackgroundBitmap);
				this.Invoke(d, new object[] { image });
			}
			else
			{
				upDownPBOne.Image = null;

				upDownPBOne.Image = new Bitmap(image); //image;
			}
		}

		/// <summary>
		/// Updates the picturebox which contains the image of the segment when the down bitmap is shown on the monitor, only during coarse alignment
		/// </summary>
		/// <param name="image">The image to be displayed in the picturebox</param>  
		public void ShowUpDownTwoBackgroundBitmap(Bitmap image)
		{
			if (this.upDownPBTwo.InvokeRequired)
			{
				SetDownImageCallback d = new SetDownImageCallback(ShowUpDownTwoBackgroundBitmap);
				this.Invoke(d, new object[] { image });
			}
			else
			{
				upDownPBTwo.Image = null;

				upDownPBTwo.Image = new Bitmap(image);//image;
			}
		}

		/// <summary>
		/// Updates the label which states in which direction the engines should run
		/// </summary>
		/// <param name="dir">The direction to be shown to the user</param>  
		public void ShowLeftRightDirection(string dir) 
		{
			if (this.dirLeftRightLabel.InvokeRequired)
			{
				SetDirLeftRightLabelCallback d = new SetDirLeftRightLabelCallback(ShowLeftRightDirection);
				this.Invoke(d, new object[] { dir });
			}
			else
			{
				this.dirLeftRightLabel.Text = dirLeftRightLabel.Text = dir;
			}
		}

		/// <summary>
		/// Updates the label which states in which direction the engines should run
		/// </summary>
		/// <param name="dir">The direction to be shown to the user</param>  
		public void ShowUpDownDirection(string dir)
		{
			if (this.dirUpDownLabel.InvokeRequired)
			{
				SetDirLeftRightLabelCallback d = new SetDirLeftRightLabelCallback(ShowUpDownDirection);
				this.Invoke(d, new object[] { dir });
			}
			else
			{
				this.dirUpDownLabel.Text = dirUpDownLabel.Text = dir;
			}
		}

		/// <summary>
		/// Manual button to update the overview image is updated
		/// </summary>
		private void button1_Click_1(object sender, EventArgs e)
		{
			manualUpdateOverview = true;
		}

		/// <summary>
		/// Setting the update telling the worker that the overview image should be updated
		/// </summary>
		/// <param name="value">Set or not set the value that the worker reads</param>  
		public void SetManualUpdateOverviewImg(bool value)
		{
			manualUpdateOverview = value;
		}

		/// <summary>
		/// Setting the update telling the worker that the overview image should be updated
		/// </summary>
		public bool GetUpdateManualOverview()
		{
			return manualUpdateOverview;
		}

		/// <summary>
		/// Saves the settings in the DALgetmotor1spreache
		/// </summary>
		private void button2_Click(object sender, EventArgs e)
		{
			string[] settings = DAL.GetSettings("User");

			//MessageBox.Show("massCenter x: " + realCoM.X + " y: " + realCoM.Y);

			if(Alignment == "fine")
			{
				Bitmap saveimage = new Bitmap(combinedImagePB.Image);
				DAL.AddImage(saveimage, cameraController.GetExposureTime(), cameraController.GetAOI().ToString(), 1, double.Parse(settings[2]), 1, realCoM);
				saveimage.Save("c:/imagedata/image"+ System.DateTime.Now.ToString("yyy_MM_dd.HH_mm_ss")+ ".bmp");

			}
			else
			{
				DAL.AddImage(new Bitmap(leftRightPBOne.Image), cameraController.GetExposureTime(), cameraController.GetAOI().ToString(), 2, double.Parse(settings[2]), 1, realCoM);
				DAL.AddImage(new Bitmap(leftRightPBTwo.Image), cameraController.GetExposureTime(), cameraController.GetAOI().ToString(), 3, double.Parse(settings[2]), 1, realCoM);
				DAL.AddImage(new Bitmap(upDownPBOne.Image), cameraController.GetExposureTime(), cameraController.GetAOI().ToString(), 4, double.Parse(settings[2]), 1, realCoM);
				DAL.AddImage(new Bitmap(upDownPBTwo.Image), cameraController.GetExposureTime(), cameraController.GetAOI().ToString(), 5, double.Parse(settings[2]), 1, realCoM);
			}

			//blackBackgroundImagePB.Image = null;
			//bitmapBackgroundImagePB.Image = null;
		}

		/// <summary>
		/// Not implemented
		/// </summary>
		public CreateNewUserForm CreateNewUserForm
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}

		/// <summary>
		/// Not implemented
		/// </summary>
		public AdministrateUsersForm AdministrateUsersForm
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}

		/// <summary>
		/// Not implemented
		/// </summary>
		public ChangePasswordForm ChangePasswordForm
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}

		/// <summary>
		/// Not implemented
		/// </summary>
		public Eventlog Eventlog
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}

		/// <summary>
		/// Not implemented
		/// </summary>
		public OptionForm OptionForm
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}

		/// <summary>
		/// Not implemented
		/// </summary>
		public ImageHistory ImageHistory
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}

		/// <summary>
		/// Not implemented
		/// </summary>
		public ImageInfo ImageInfo
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}

		/// <summary>
		/// Not implemented
		/// </summary>
		public Login Login
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}

		/// <summary>
		/// Updates the CoM value in the GUI
		/// </summary>
		/// <param name="CoM">The CoM string</param>
		/// <param name="realCoMInput">beats me</param>
		public void UpdateCoMLabel(string CoM, Point realCoMInput)
		{
			if (this.CoMLabel.InvokeRequired)
			{
				SetCoMLabelActiveCallback d = new SetCoMLabelActiveCallback(UpdateCoMLabel);
				this.Invoke(d, new object[] { CoM, realCoMInput });
			}
			else
			{
				CoMLabel.Text = "CoM offset: " + CoM;
				realCoM = realCoMInput;
			}
		}

		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			ShutDownProper();
		}

		private void groupBox2_Enter(object sender, EventArgs e)
		{
			//SetToFine();
		}

		private void MainWindow_Load(object sender, EventArgs e)
		{
			//SetToFine();
		}

		private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}

		private void MainWindow_KeyDown(object sender, KeyEventArgs e)
		{
			//menuStrip1.OnKeyDown(sender, e);
		}

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void gbCoarse_Enter(object sender, EventArgs e)
        {

        }

        private void coarseRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabOverview"])
            {
                Alignment = "over";
            }

            if (tabControl1.SelectedTab == tabControl1.TabPages["tabCoarse"])
            {
                SetToCoarse();
            }

            if (tabControl1.SelectedTab == tabControl1.TabPages["tabFine"])
            {
                SetToFine();
            }

            if (tabControl1.SelectedTab == tabControl1.TabPages["tabCal"])
            {
                Alignment = "calibrate";
            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
           
        }

        // The LIVE button has changed state
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            valueLiveCheckState = checkBox1.CheckState.ToString();
            MessageBox.Show(valueLiveCheckState);
        }

        private void UpButton_Click(object sender, EventArgs e)
        {
            string segment = valueSegmentNumberTextbox;
            StringBuilder strB = new StringBuilder(segment);
            if (strB[2] == (char)1)
            {
                int pos = int.Parse(strB[1].ToString());
                if (pos != 0)
                {
                    pos = pos - 1;
                    strB[1] = (char)pos;
                    valueSegmentNumberTextbox = strB.ToString();
                }
                else
                {
                    pos = 9;
                    strB[0] = (char)0;
                    strB[1] = (char)pos;
                    valueSegmentNumberTextbox = strB.ToString();
                }
            }

            if (strB[2] == (char)2)
            {
                int pos = int.Parse(strB[3].ToString());
                if (pos != 2)
                {
                    pos = pos - 1;
                    strB[3] = (char)pos;
                    valueSegmentNumberTextbox = strB.ToString();
                }
                else
                {
                    pos = 1;
                    if (strB[1] == (char)0)
                    {
                        strB[1] = (char)9;
                        strB[0] = (char)0;
                        strB[3] = (char)pos;
                        valueSegmentNumberTextbox = strB.ToString();
                    }
                    else
                    {
                        strB[1] = (char)(int.Parse(strB[1].ToString()) - 1);
                        strB[3] = (char)pos;
                        valueSegmentNumberTextbox = strB.ToString();
                    }

                }
            }

            if (strB[2] == (char)3)
            {
                int pos = int.Parse(strB[3].ToString());
                if (pos != 3)
                {
                    pos = pos - 1;
                    strB[3] = (char)pos;
                    valueSegmentNumberTextbox = strB.ToString();
                }
                else
                {
                    pos = 1;
                    if (strB[1] == (char)0)
                    {
                        strB[1] = (char)9;
                        strB[0] = (char)0;
                        strB[3] = (char)pos;
                        valueSegmentNumberTextbox = strB.ToString();
                    }
                    else
                    {
                        strB[1] = (char)(int.Parse(strB[1].ToString()) - 1);
                        strB[3] = (char)pos;
                        valueSegmentNumberTextbox = strB.ToString();
                    }
                }
            }
            SegmentNumberTextbox.Text = valueSegmentNumberTextbox;
        }

        private void DWbutton_Click(object sender, EventArgs e)
        {
            string segment = valueSegmentNumberTextbox;
            StringBuilder strB = new StringBuilder(segment);
            MessageBox.Show(strB[0] + " " + strB[1] + " " + strB[2] + " " + strB[3]);
            int pos0 = int.Parse(strB[0].ToString());
            int pos1 = int.Parse(strB[1].ToString());
            int pos2 = int.Parse(strB[2].ToString());
            int pos3 = int.Parse(strB[3].ToString());
            if (pos1 == 1) // inner segment
            {
                if (pos1 != 9)
                {
                    pos1 = pos1 + 1;
                    strB[1] = (char)(80+pos1);
                }
                else
                {
                    pos0 = 0;
                    strB[0] = (char)pos0;
                    strB[1] = (char)pos1;
                    MessageBox.Show(strB.ToString());
                }
            }

            if (pos2 == 2) // middle segment
            {
                if (pos2 != 2)
                {
                    pos3 = pos3 + 1;
                    strB[3] = (char)pos3;
                }
                else
                {
                    pos3 = 1;
                    if (strB[1] == (char)9)
                    {
                        strB[1] = (char)0;
                        strB[0] = (char)1;
                        strB[3] = (char)pos3;
                    }
                    else
                    {
                        strB[1] = (char)(pos1 + 1);
                        strB[3] = (char)pos3;
                    }
                    
                }
            }

            if (strB[2] == (char)3) // outer segment
            {
                int pos = int.Parse(strB[3].ToString());
                if (pos != 3)
                {
                    pos = pos + 1;
                    strB[3] = (char)pos;
                    //valueSegmentNumberTextbox = strB.ToString();
                }
                else
                {
                    pos = 1;
                    if (strB[1] == (char)9)
                    {
                        strB[1] = (char)0;
                        strB[0] = (char)1;
                        strB[3] = (char)pos;
                        //valueSegmentNumberTextbox = strB.ToString();
                    }
                    else
                    {
                        strB[1] = (char)(int.Parse(strB[1].ToString()) + 1);
                        strB[3] = (char)pos;
                        //valueSegmentNumberTextbox = strB.ToString();
                    }
                }
            }
            //SegmentNumberTextbox.Text = valueSegmentNumberTextbox;
            //MessageBox.Show(valueSegmentNumberTextbox);
        }

        private void RTbutton_Click(object sender, EventArgs e)
        {
            string segment = valueSegmentNumberTextbox;
            StringBuilder strB = new StringBuilder(segment);
            int pos = int.Parse(strB[2].ToString());
            if (pos != 3)
            {
                pos = pos + 1;
                strB[2] = (char)pos;
                valueSegmentNumberTextbox = strB.ToString();
            }
            else
            {
                pos = 1;
                strB[2] = (char)pos;
                valueSegmentNumberTextbox = strB.ToString();
            }
            SegmentNumberTextbox.Text = valueSegmentNumberTextbox;
        }

        private void LTbutton_Click(object sender, EventArgs e)
        {
            string segment = valueSegmentNumberTextbox;
            StringBuilder strB = new StringBuilder(segment);
            int pos = int.Parse(strB[2].ToString());
            if (pos != 1)
            {
                pos = pos - 1;
                strB[2] = (char)pos;
                valueSegmentNumberTextbox = strB.ToString();
            }
            else
            {
                pos = 3;
                strB[2] = (char)pos;
                valueSegmentNumberTextbox = strB.ToString();
            }
            SegmentNumberTextbox.Text = valueSegmentNumberTextbox;
        }
 
	}
}
