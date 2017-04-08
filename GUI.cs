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
        string valueLiveCheckState="Checked";

		Point realCoM;

		string currentUser = "Admin";
		string currentUserType = "Admin";

		bool settingsUpdated = false;
		bool manualUpdateOverview = false;

		CameraController cameraController;
		CheckAllForm caf = new CheckAllForm();

		/// <summary>
		/// Gets the caf.
		/// </summary>
		/// <returns></returns>
		public CheckAllForm getCAF()
		{
			return caf;
		}

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

			CheckForIllegalCrossThreadCalls = false;
			//this.ActiveControl = BlackBGNumberLabel;

			DisableLabel();
			ShowSegnum(valueSegmentNumberTextbox);
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

		/// <summary>
		/// Gets the live mode.
		/// </summary>
		/// <returns></returns>
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
			string rval = Alignment;
			if (rval[0] == 'c' && rval[1]=='h')
				Alignment = "over";
			return rval;
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
				ShowSegnum(valueSegmentNumberTextbox);
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

				leftRightPBOne.Image = new Bitmap(image); 

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

				leftRightPBTwo.Image = new Bitmap(image);

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

				upDownPBOne.Image = new Bitmap(image);
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

				upDownPBTwo.Image = new Bitmap(image);
			}
		}

		/// <summary>
		/// Updates the label which states in which direction the engines should run
		/// </summary>
		/// <param name="dir">The direction to be shown to the user</param>  
		public void ShowLeftRightDirection(string dir) 
		{
			//if (this.dirLeftRightLabel.InvokeRequired)
			//{
			//	SetDirLeftRightLabelCallback d = new SetDirLeftRightLabelCallback(ShowLeftRightDirection);
			//	this.Invoke(d, new object[] { dir });
			//}
			//else
			//{
			//	this.dirLeftRightLabel.Text = dirLeftRightLabel.Text = dir;
			//}
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
			/* /menuStrip1.OnKeyDown(sender, e);
			if (Alignment=="over")
			{
				if (e.KeyCode == Keys.Left)
					LTbutton_Click(sender, e);
				if (e.KeyCode == Keys.Right)
					RTbutton_Click(sender, e);
				if (e.KeyCode == Keys.Up)
					UpButton_Click(sender, e);
				if (e.KeyCode == Keys.Down)
					DWbutton_Click(sender, e);
			} */
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
            //MessageBox.Show(valueLiveCheckState);
        }

		char toc(int i)
		{
			return (char)('0' + i);
		}

		class SegmentNumber
		{
			public void FromString(string str)
			{
				int pos0 = str[0] - '0';
				int pos1 = str[1] - '0';
				int pos2 = str[2] - '0';
				int pos3 = str[3] - '0';
				line = pos0 * 10 + pos1;
				seg = pos2;
				sub = pos3;
			}
			public override string ToString() 
			{
				string ss =
					line.ToString();
				if (line < 10)
					ss = '0' + ss;
				ss += seg.ToString();
				ss += sub.ToString();
				return ss;
			}

			private int SubCnt()
			{
				switch(seg)
				{
					case 1: return 1;
					case 2: return 2;
					case 3: return 3;
				}
				return 0;
			}

			private void NxtLine()
			{
				++line;
				if (line >= 12)
					line -= 11;
			}
			private void PrvLine()
			{
				--line;
				if (line <= 0)
					line += 11;
			}

			public void Up()
			{
				if (sub == 1)
				{
					sub = SubCnt();
					PrvLine();
				}
				else
				{
					--sub;
				}
			}

			public void Dn()
			{
				if (sub == SubCnt())
				{
					sub = 1;
					NxtLine();
				}
				else
				{
					++sub;
				}
			}
			public void Lf()
			{
				if (seg > 1) --seg;
				if (sub > SubCnt())
					sub = SubCnt();
			}
			public void Rg()
			{
				if (seg < 3) ++seg;
			}

			private int line, seg, sub;

		}

		SegmentNumber segnum = new SegmentNumber();

		private void UpButton_Click(object sender, EventArgs e)
		{
			segnum.FromString(valueSegmentNumberTextbox);
			segnum.Up();
			string seg = segnum.ToString();
			valueSegmentNumberTextbox = seg;
			SegmentNumberTextbox.Text = seg;
			ShowSegnum(seg);
		}

		private void DWbutton_Click(object sender, EventArgs e)
		{
			segnum.FromString(valueSegmentNumberTextbox);
			segnum.Dn();
			string seg = segnum.ToString();
			valueSegmentNumberTextbox = seg;
			SegmentNumberTextbox.Text = seg;
			ShowSegnum(seg);
		}

		private void RTbutton_Click(object sender, EventArgs e)
		{
			segnum.FromString(valueSegmentNumberTextbox);
			segnum.Rg();
			string seg = segnum.ToString();
			valueSegmentNumberTextbox = seg;
			SegmentNumberTextbox.Text = seg;
			ShowSegnum(seg);
		}

		private void LTbutton_Click(object sender, EventArgs e)
		{
			segnum.FromString(valueSegmentNumberTextbox);
			segnum.Lf();
			string seg = segnum.ToString();
			valueSegmentNumberTextbox = seg;
			SegmentNumberTextbox.Text = seg;
			ShowSegnum(seg);
		}

		private void btnCheckAllFine_Click(object sender, EventArgs e)
		{
			// check all
			bool old = checkBox1.Checked;
			checkBox1.Checked = false;
			Alignment = "checkALLfine";
			caf.Start();
			caf.Title("check all fine");
			caf.ShowDialog();
			checkBox1.Checked = old;
		}

		private void btnCheckAllCoarse_Click(object sender, EventArgs e)
		{
			// check all
			bool old = checkBox1.Checked;
			checkBox1.Checked = false;
			Alignment = "checkALLcoarse";
			caf.Start();
			caf.Title("check all coarse");
			caf.ShowDialog();
			checkBox1.Checked = old;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// show segment number
		/// </summary>
		/// <param name="segnum"></param>
		public void ShowSegnum(string segnum)
		{
			lbl_01.Text = "SEGMENT " + segnum;
			lbl_01.Visible = true;
		}

		/// <summary>
		/// hide segment number
		/// </summary>
		public void HideSegnum()
		{
			lbl_01.Visible = false;
		}

		/// <summary>
		/// show algnment
		/// </summary>
		/// <param name="align_x"></param>
		/// <param name="align_y"></param>
		public void ShowAlign(int align_x, int align_y)
		{
			lbl_03.Text = align_x.ToString() + " mm";
			lbl_03.ForeColor = (Math.Abs(align_x) > 4) ? Color.Red : Color.Green;
			lbl_05.Text = align_y.ToString() + " mm";
			lbl_05.ForeColor = (Math.Abs(align_y) > 4) ? Color.Red : Color.Green;

			lbl_03.Visible = true;
			lbl_05.Visible = true;
		}

		/// <summary>
		/// hide alignment
		/// </summary>
		public void HideAlign()
		{
			lbl_03.Visible = false;
			lbl_05.Visible = false;
		}

		/// <summary>
		/// show rotation
		/// </summary>
		/// <param name="rot_x"></param>
		/// <param name="rot_y"></param>
		/// <param name="rot_z"></param>
		public void ShowRot(int rot_x, int rot_y, int rot_z)
		{
			lbl_07.Text = rot_x.ToString() + " mrad";
			lbl_07.ForeColor = (Math.Abs(rot_x) > 3) ? Color.Red : Color.Green;
			lbl_09.Text = rot_y.ToString() + " mrad";
			lbl_09.ForeColor = (Math.Abs(rot_y) > 3) ? Color.Red : Color.Green;
			lbl_11.Text = rot_z.ToString() + " mrad";
			lbl_11.ForeColor = (Math.Abs(rot_z) > 10) ? Color.Red : Color.Green;

			lbl_07.Visible = true;
			lbl_09.Visible = true;
			lbl_11.Visible = true;
		}

		/// <summary>
		/// hide rotation
		/// </summary>
		public void HideRot()
		{
			lbl_07.Visible = false;
			lbl_09.Visible = false;
			lbl_11.Visible = false;
		}


		/// <summary>
		/// Updates the labels.
		/// </summary>
		/// <param name="segnum">The segment number.</param>
		/// <param name="align_x">The x alignment.</param>
		/// <param name="align_y">The y alignment.</param>
		/// <param name="rot_x">The x rotation.</param>
		/// <param name="rot_y">The y rotation.</param>
		/// <param name="rot_z">The z rotation.</param>
		public void UpdateLabel (
			string segnum,
			int align_x,
			int align_y,
			int rot_x,
			int rot_y,
			int rot_z )
		{
			ShowSegnum(segnum);
			ShowAlign(align_x, align_y);
			ShowRot(rot_x, rot_y, rot_z);

		}

		/// <summary>
		/// Disables the labels.
		/// </summary>
		public void DisableLabel ()
		{
			lbl_01.Visible = false;
			//lbl_02.Visible = false;
			lbl_03.Visible = false;
			//lbl_04.Visible = false;
			lbl_05.Visible = false;
			//lbl_06.Visible = false;
			lbl_07.Visible = false;
			//lbl_08.Visible = false;
			lbl_09.Visible = false;
			//lbl_10.Visible = false;
			lbl_11.Visible = false;
		}

		/// <summary>
		/// hide upper image
		/// </summary>
		public void HideUpperImg()
		{
			pb_Upper.Image = imageList1.Images[0];
			lbl_ul.Visible = false;
			lbl_ur.Visible = false;
		}

		/// <summary>
		/// set upper image l/r only
		/// </summary>
		/// <param name="lft"></param>
		/// <param name="rgt"></param>
		public void SetUpperImgNoval(int lft, int rgt)
		{
			lbl_ul.Visible = false;
			lbl_ur.Visible = false;

			bool neglft = lft < 0;
			if (neglft) lft = -lft;
			bool negrgt = rgt < 0;
			if (negrgt) rgt = -rgt;

			int num = 1 + (neglft ? 0 : 2) + (negrgt ? 0 : 1);
			pb_Upper.Image = imageList1.Images[num];
		}

		/// <summary>
		/// Sets the upper img.
		/// </summary>
		/// <param name="lft">The LFT.</param>
		/// <param name="rgt">The RGT.</param>
		public void SetUpperImg( int lft, int rgt )
		{
			if (lft==0 && rgt==0)
			{
				pb_Upper.Image = imageList1.Images[0];
				lbl_ul.Visible = false;
				lbl_ur.Visible = false;
				return;
			}

			lbl_ul.Visible = true;
			lbl_ur.Visible = true;

			bool neglft = lft < 0;
			if (neglft) lft = -lft;
			bool negrgt = rgt < 0;
			if (negrgt) rgt = -rgt;

			lbl_ul.Text = lft.ToString();
			lbl_ur.Text = rgt.ToString();

			int num = 1 + (neglft ? 0 : 2) + (negrgt ? 0 : 1);
			pb_Upper.Image = imageList1.Images[num];
		}


		/// <summary>
		/// hide lower img
		/// </summary>
		public void HideLowerImg()
		{
			pb_Lower.Image = imageList1.Images[0];
			lbl_ul.Visible = false;
			lbl_ur.Visible = false;
		}

		/// <summary>
		/// set lower image l/r only
		/// </summary>
		/// <param name="lft"></param>
		/// <param name="rgt"></param>
		public void SetLowerImgNoval(int lft, int rgt)
		{
			lbl_ll.Visible = false;
			lbl_lr.Visible = false;

			bool neglft = lft < 0;
			if (neglft) lft = -lft;
			bool negrgt = rgt < 0;
			if (negrgt) rgt = -rgt;

			int num = 1 + (neglft ? 0 : 2) + (negrgt ? 0 : 1);
			pb_Lower.Image = imageList1.Images[num];
		}


		/// <summary>
		/// Sets the lower img.
		/// </summary>
		/// <param name="lft">The LFT.</param>
		/// <param name="rgt">The RGT.</param>
		public void SetLowerImg(int lft, int rgt)
		{
			if (lft == 0 && rgt == 0)
			{
				pb_Lower.Image = imageList1.Images[0];
				lbl_ll.Visible = false;
				lbl_lr.Visible = false;
				return;
			}

			lbl_ll.Visible = true;
			lbl_lr.Visible = true;

			bool neglft = lft < 0;
			if (neglft) lft = -lft;
			bool negrgt = rgt < 0;
			if (negrgt) rgt = -rgt;

			lbl_ll.Text = lft.ToString();
			lbl_lr.Text = rgt.ToString();

			int num = 1 + (neglft ? 0 : 2) + (negrgt ? 0 : 1);
			pb_Lower.Image = imageList1.Images[num];
		}


		/// <summary>
		/// Sets the fine img.
		/// </summary>
		/// <param name="lft">The LFT.</param>
		/// <param name="rgt">The RGT.</param>
		public void SetFineImg(int lft, int rgt)
		{
			if (lft == 0 && rgt == 0)
			{
				pbFine.Image = imageList1.Images[0];
				lbl_fl.Visible = false;
				lbl_fr.Visible = false;
				return;
			}

			lbl_fl.Visible = true;
			lbl_fr.Visible = true;

			bool neglft = lft < 0;
			if (neglft) lft = -lft;
			bool negrgt = rgt < 0;
			if (negrgt) rgt = -rgt;

			lbl_fl.Text = lft.ToString();
			lbl_fr.Text = rgt.ToString();

			int num = 1 + (neglft ? 0 : 2) + (negrgt ? 0 : 1);
			pbFine.Image = imageList1.Images[num];
		}

		


		private bool can_accept = false;

		/// <summary>
		/// Sets the tangetial and radial ofsetts in "fine".
		/// </summary>
		/// <param name="tan">The tan.</param>
		/// <param name="rad">The rad.</param>
		public void SetTanRad(double tan, double rad)
		{
			lbl_tan_ofs.Text = tan.ToString();
			lbl_rad_ofs.Text = rad.ToString();
			can_accept = (Math.Abs(tan) <= 1.0) && (Math.Abs(rad) <= 1.0);
			acceptButton.BackColor = can_accept ? Color.Green : Color.Red;
		}

		private void acceptButton_Click(object sender, EventArgs e)
		{
			//
			if (can_accept)
			{
				string fn = getChosenPath() + "\\image_";
				var now = DateTime.Now;
				fn += now.ToShortDateString() + "_" + now.ToShortTimeString().Replace(':','-') + "_";
				fn += valueSegmentNumberTextbox + ".bmp";
				if (combinedImagePB.Image!=null) combinedImagePB.Image.Save(fn);
				Alignment = "calibrate";
				tabControl1.SelectedIndex = 3;
			}
		}

		private string folder_path = ".";

		private void pathToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
			folderBrowserDialog1.ShowNewFolderButton = true;
			folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
			DialogResult result = folderBrowserDialog1.ShowDialog();
	        if( result == DialogResult.OK )
			{
				folder_path = folderBrowserDialog1.SelectedPath;
			}
		}

		/// <summary>
		/// Gets the chosen path.
		/// </summary>
		/// <returns>the path</returns>
		public string getChosenPath()
		{
			return folder_path;
		}

		private void button1_Click_2(object sender, EventArgs e)
		{
			SetTanRad(1.2, -0.7);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			SetTanRad(0.3, 0.2);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			SetFineImg(2, 2);
		}


	}
}

