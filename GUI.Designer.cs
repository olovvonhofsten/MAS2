namespace MirrorAlignmentSystem
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.administrateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.eventLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.BIA_timer = new System.Windows.Forms.Timer(this.components);
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabOverview = new System.Windows.Forms.TabPage();
			this.btnCheckAllCoarse = new System.Windows.Forms.Button();
			this.btnCheckAllFine = new System.Windows.Forms.Button();
			this.RTbutton = new System.Windows.Forms.Button();
			this.LTbutton = new System.Windows.Forms.Button();
			this.DWbutton = new System.Windows.Forms.Button();
			this.UpButton = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.BlackBGNumberTextBox = new System.Windows.Forms.TextBox();
			this.SegmentNumberTextbox = new System.Windows.Forms.TextBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.overviewImagePB = new System.Windows.Forms.PictureBox();
			this.tabCoarse = new System.Windows.Forms.TabPage();
			this.gbCoarse = new System.Windows.Forms.GroupBox();
			this.lbl_lr_2 = new System.Windows.Forms.Label();
			this.dirUpDownLabel = new System.Windows.Forms.Label();
			this.upDownPBTwo = new System.Windows.Forms.PictureBox();
			this.upDownPBOne = new System.Windows.Forms.PictureBox();
			this.leftRightPBOne = new System.Windows.Forms.PictureBox();
			this.leftRightPBTwo = new System.Windows.Forms.PictureBox();
			this.lbl_lr_1 = new System.Windows.Forms.Label();
			this.dirLeftRightLabel = new System.Windows.Forms.Label();
			this.tabFine = new System.Windows.Forms.TabPage();
			this.combinedImagePB = new System.Windows.Forms.PictureBox();
			this.CoMLabel = new System.Windows.Forms.Label();
			this.tabCal = new System.Windows.Forms.TabPage();
			this.CalibrateImgPB = new System.Windows.Forms.PictureBox();
			this.lbl_01 = new System.Windows.Forms.Label();
			this.lbl_02 = new System.Windows.Forms.Label();
			this.lbl_03 = new System.Windows.Forms.Label();
			this.lbl_04 = new System.Windows.Forms.Label();
			this.lbl_05 = new System.Windows.Forms.Label();
			this.lbl_06 = new System.Windows.Forms.Label();
			this.lbl_07 = new System.Windows.Forms.Label();
			this.lbl_09 = new System.Windows.Forms.Label();
			this.lbl_08 = new System.Windows.Forms.Label();
			this.lbl_11 = new System.Windows.Forms.Label();
			this.lbl_10 = new System.Windows.Forms.Label();
			this.menuStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabOverview.SuspendLayout();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.overviewImagePB)).BeginInit();
			this.tabCoarse.SuspendLayout();
			this.gbCoarse.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.upDownPBTwo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.upDownPBOne)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.leftRightPBOne)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.leftRightPBTwo)).BeginInit();
			this.tabFine.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.combinedImagePB)).BeginInit();
			this.tabCal.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.CalibrateImgPB)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(12, 4, 0, 4);
			this.menuStrip1.Size = new System.Drawing.Size(1413, 27);
			this.menuStrip1.TabIndex = 19;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(89, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imagesToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.administrateToolStripMenuItem,
            this.eventLogToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 19);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// imagesToolStripMenuItem
			// 
			this.imagesToolStripMenuItem.Name = "imagesToolStripMenuItem";
			this.imagesToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
			this.imagesToolStripMenuItem.Text = "Images";
			this.imagesToolStripMenuItem.Click += new System.EventHandler(this.imagesToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
			this.optionsToolStripMenuItem.Text = "Options";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// administrateToolStripMenuItem
			// 
			this.administrateToolStripMenuItem.Name = "administrateToolStripMenuItem";
			this.administrateToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
			this.administrateToolStripMenuItem.Text = "Administrate";
			this.administrateToolStripMenuItem.Click += new System.EventHandler(this.administrateToolStripMenuItem_Click);
			// 
			// eventLogToolStripMenuItem
			// 
			this.eventLogToolStripMenuItem.Name = "eventLogToolStripMenuItem";
			this.eventLogToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
			this.eventLogToolStripMenuItem.Text = "Event Log";
			this.eventLogToolStripMenuItem.Click += new System.EventHandler(this.eventLogToolStripMenuItem_Click);
			// 
			// BIA_timer
			// 
			this.BIA_timer.Interval = 125;
			this.BIA_timer.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabOverview);
			this.tabControl1.Controls.Add(this.tabCoarse);
			this.tabControl1.Controls.Add(this.tabFine);
			this.tabControl1.Controls.Add(this.tabCal);
			this.tabControl1.Location = new System.Drawing.Point(16, 50);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.Padding = new System.Drawing.Point(100, 10);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1387, 710);
			this.tabControl1.TabIndex = 37;
			this.tabControl1.TabIndexChanged += new System.EventHandler(this.tabControl1_TabIndexChanged);
			this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
			// 
			// tabOverview
			// 
			this.tabOverview.BackColor = System.Drawing.Color.Moccasin;
			this.tabOverview.Controls.Add(this.btnCheckAllCoarse);
			this.tabOverview.Controls.Add(this.btnCheckAllFine);
			this.tabOverview.Controls.Add(this.RTbutton);
			this.tabOverview.Controls.Add(this.LTbutton);
			this.tabOverview.Controls.Add(this.DWbutton);
			this.tabOverview.Controls.Add(this.UpButton);
			this.tabOverview.Controls.Add(this.checkBox1);
			this.tabOverview.Controls.Add(this.button1);
			this.tabOverview.Controls.Add(this.button2);
			this.tabOverview.Controls.Add(this.BlackBGNumberTextBox);
			this.tabOverview.Controls.Add(this.SegmentNumberTextbox);
			this.tabOverview.Controls.Add(this.groupBox5);
			this.tabOverview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabOverview.Location = new System.Drawing.Point(4, 48);
			this.tabOverview.Margin = new System.Windows.Forms.Padding(9, 6, 7, 6);
			this.tabOverview.Name = "tabOverview";
			this.tabOverview.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.tabOverview.Size = new System.Drawing.Size(1379, 658);
			this.tabOverview.TabIndex = 0;
			this.tabOverview.Text = "OVERVIEW";
			this.tabOverview.Click += new System.EventHandler(this.tabPage1_Click);
			// 
			// btnCheckAllCoarse
			// 
			this.btnCheckAllCoarse.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCheckAllCoarse.Location = new System.Drawing.Point(890, 398);
			this.btnCheckAllCoarse.Name = "btnCheckAllCoarse";
			this.btnCheckAllCoarse.Size = new System.Drawing.Size(205, 63);
			this.btnCheckAllCoarse.TabIndex = 49;
			this.btnCheckAllCoarse.Text = "Check all Coarse";
			this.btnCheckAllCoarse.UseVisualStyleBackColor = true;
			this.btnCheckAllCoarse.Click += new System.EventHandler(this.btnCheckAllCoarse_Click);
			// 
			// btnCheckAllFine
			// 
			this.btnCheckAllFine.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCheckAllFine.Location = new System.Drawing.Point(890, 303);
			this.btnCheckAllFine.Name = "btnCheckAllFine";
			this.btnCheckAllFine.Size = new System.Drawing.Size(205, 63);
			this.btnCheckAllFine.TabIndex = 48;
			this.btnCheckAllFine.Text = "Check all Fine";
			this.btnCheckAllFine.UseVisualStyleBackColor = true;
			this.btnCheckAllFine.Click += new System.EventHandler(this.btnCheckAllFine_Click);
			// 
			// RTbutton
			// 
			this.RTbutton.BackColor = System.Drawing.Color.White;
			this.RTbutton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RTbutton.BackgroundImage")));
			this.RTbutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.RTbutton.Location = new System.Drawing.Point(1056, 134);
			this.RTbutton.Name = "RTbutton";
			this.RTbutton.Size = new System.Drawing.Size(67, 63);
			this.RTbutton.TabIndex = 47;
			this.RTbutton.UseVisualStyleBackColor = false;
			this.RTbutton.Click += new System.EventHandler(this.RTbutton_Click);
			// 
			// LTbutton
			// 
			this.LTbutton.BackColor = System.Drawing.Color.White;
			this.LTbutton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("LTbutton.BackgroundImage")));
			this.LTbutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.LTbutton.Location = new System.Drawing.Point(907, 134);
			this.LTbutton.Name = "LTbutton";
			this.LTbutton.Size = new System.Drawing.Size(67, 63);
			this.LTbutton.TabIndex = 46;
			this.LTbutton.UseVisualStyleBackColor = false;
			this.LTbutton.Click += new System.EventHandler(this.LTbutton_Click);
			// 
			// DWbutton
			// 
			this.DWbutton.BackColor = System.Drawing.Color.White;
			this.DWbutton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DWbutton.BackgroundImage")));
			this.DWbutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.DWbutton.Location = new System.Drawing.Point(980, 172);
			this.DWbutton.Name = "DWbutton";
			this.DWbutton.Size = new System.Drawing.Size(67, 63);
			this.DWbutton.TabIndex = 45;
			this.DWbutton.UseVisualStyleBackColor = false;
			this.DWbutton.Click += new System.EventHandler(this.DWbutton_Click);
			// 
			// UpButton
			// 
			this.UpButton.BackColor = System.Drawing.Color.White;
			this.UpButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("UpButton.BackgroundImage")));
			this.UpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.UpButton.Location = new System.Drawing.Point(980, 103);
			this.UpButton.Name = "UpButton";
			this.UpButton.Size = new System.Drawing.Size(67, 63);
			this.UpButton.TabIndex = 44;
			this.UpButton.UseVisualStyleBackColor = false;
			this.UpButton.Click += new System.EventHandler(this.UpButton_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.checkBox1.Location = new System.Drawing.Point(40, 48);
			this.checkBox1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(69, 28);
			this.checkBox1.TabIndex = 43;
			this.checkBox1.Text = "LIVE";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(175, 563);
			this.button1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(524, 66);
			this.button1.TabIndex = 42;
			this.button1.Text = "Update Overview image";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button2.Location = new System.Drawing.Point(753, 563);
			this.button2.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(571, 66);
			this.button2.TabIndex = 41;
			this.button2.Text = "Save image";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// BlackBGNumberTextBox
			// 
			this.BlackBGNumberTextBox.Location = new System.Drawing.Point(1015, 12);
			this.BlackBGNumberTextBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.BlackBGNumberTextBox.Name = "BlackBGNumberTextBox";
			this.BlackBGNumberTextBox.Size = new System.Drawing.Size(112, 20);
			this.BlackBGNumberTextBox.TabIndex = 38;
			// 
			// SegmentNumberTextbox
			// 
			this.SegmentNumberTextbox.Location = new System.Drawing.Point(961, 244);
			this.SegmentNumberTextbox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.SegmentNumberTextbox.Name = "SegmentNumberTextbox";
			this.SegmentNumberTextbox.Size = new System.Drawing.Size(108, 20);
			this.SegmentNumberTextbox.TabIndex = 39;
			this.SegmentNumberTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SegmentNumberTextbox_KeyDown);
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.overviewImagePB);
			this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.groupBox5.Location = new System.Drawing.Point(175, 12);
			this.groupBox5.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.groupBox5.Size = new System.Drawing.Size(671, 500);
			this.groupBox5.TabIndex = 27;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Overview Image";
			this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
			// 
			// overviewImagePB
			// 
			this.overviewImagePB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.overviewImagePB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.overviewImagePB.Location = new System.Drawing.Point(12, 34);
			this.overviewImagePB.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.overviewImagePB.Name = "overviewImagePB";
			this.overviewImagePB.Size = new System.Drawing.Size(643, 403);
			this.overviewImagePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.overviewImagePB.TabIndex = 12;
			this.overviewImagePB.TabStop = false;
			// 
			// tabCoarse
			// 
			this.tabCoarse.BackColor = System.Drawing.Color.MistyRose;
			this.tabCoarse.Controls.Add(this.gbCoarse);
			this.tabCoarse.Controls.Add(this.lbl_lr_1);
			this.tabCoarse.Controls.Add(this.dirLeftRightLabel);
			this.tabCoarse.Location = new System.Drawing.Point(4, 48);
			this.tabCoarse.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.tabCoarse.Name = "tabCoarse";
			this.tabCoarse.Size = new System.Drawing.Size(1354, 609);
			this.tabCoarse.TabIndex = 2;
			this.tabCoarse.Text = "COARSE";
			// 
			// gbCoarse
			// 
			this.gbCoarse.BackColor = System.Drawing.Color.Transparent;
			this.gbCoarse.Controls.Add(this.lbl_lr_2);
			this.gbCoarse.Controls.Add(this.dirUpDownLabel);
			this.gbCoarse.Controls.Add(this.upDownPBTwo);
			this.gbCoarse.Controls.Add(this.upDownPBOne);
			this.gbCoarse.Controls.Add(this.leftRightPBOne);
			this.gbCoarse.Controls.Add(this.leftRightPBTwo);
			this.gbCoarse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gbCoarse.Location = new System.Drawing.Point(7, 6);
			this.gbCoarse.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.gbCoarse.Name = "gbCoarse";
			this.gbCoarse.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.gbCoarse.Size = new System.Drawing.Size(651, 637);
			this.gbCoarse.TabIndex = 26;
			this.gbCoarse.TabStop = false;
			this.gbCoarse.Text = "Coarse Alignment";
			this.gbCoarse.Enter += new System.EventHandler(this.gbCoarse_Enter);
			// 
			// lbl_lr_2
			// 
			this.lbl_lr_2.Location = new System.Drawing.Point(734, 573);
			this.lbl_lr_2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.lbl_lr_2.Name = "lbl_lr_2";
			this.lbl_lr_2.Size = new System.Drawing.Size(511, 36);
			this.lbl_lr_2.TabIndex = 19;
			this.lbl_lr_2.Text = " ";
			this.lbl_lr_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// dirUpDownLabel
			// 
			this.dirUpDownLabel.AutoSize = true;
			this.dirUpDownLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dirUpDownLabel.Location = new System.Drawing.Point(662, 896);
			this.dirUpDownLabel.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.dirUpDownLabel.Name = "dirUpDownLabel";
			this.dirUpDownLabel.Size = new System.Drawing.Size(29, 31);
			this.dirUpDownLabel.TabIndex = 17;
			this.dirUpDownLabel.Text = "0";
			// 
			// upDownPBTwo
			// 
			this.upDownPBTwo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.upDownPBTwo.Location = new System.Drawing.Point(11, 331);
			this.upDownPBTwo.Margin = new System.Windows.Forms.Padding(4);
			this.upDownPBTwo.Name = "upDownPBTwo";
			this.upDownPBTwo.Size = new System.Drawing.Size(300, 300);
			this.upDownPBTwo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.upDownPBTwo.TabIndex = 15;
			this.upDownPBTwo.TabStop = false;
			this.upDownPBTwo.WaitOnLoad = true;
			// 
			// upDownPBOne
			// 
			this.upDownPBOne.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.upDownPBOne.Location = new System.Drawing.Point(319, 331);
			this.upDownPBOne.Margin = new System.Windows.Forms.Padding(4);
			this.upDownPBOne.Name = "upDownPBOne";
			this.upDownPBOne.Size = new System.Drawing.Size(300, 300);
			this.upDownPBOne.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.upDownPBOne.TabIndex = 14;
			this.upDownPBOne.TabStop = false;
			this.upDownPBOne.WaitOnLoad = true;
			// 
			// leftRightPBOne
			// 
			this.leftRightPBOne.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.leftRightPBOne.Location = new System.Drawing.Point(11, 23);
			this.leftRightPBOne.Margin = new System.Windows.Forms.Padding(4);
			this.leftRightPBOne.Name = "leftRightPBOne";
			this.leftRightPBOne.Size = new System.Drawing.Size(300, 300);
			this.leftRightPBOne.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.leftRightPBOne.TabIndex = 13;
			this.leftRightPBOne.TabStop = false;
			this.leftRightPBOne.WaitOnLoad = true;
			// 
			// leftRightPBTwo
			// 
			this.leftRightPBTwo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.leftRightPBTwo.Location = new System.Drawing.Point(319, 23);
			this.leftRightPBTwo.Margin = new System.Windows.Forms.Padding(4);
			this.leftRightPBTwo.Name = "leftRightPBTwo";
			this.leftRightPBTwo.Size = new System.Drawing.Size(300, 300);
			this.leftRightPBTwo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.leftRightPBTwo.TabIndex = 12;
			this.leftRightPBTwo.TabStop = false;
			this.leftRightPBTwo.WaitOnLoad = true;
			// 
			// lbl_lr_1
			// 
			this.lbl_lr_1.Location = new System.Drawing.Point(1143, 294);
			this.lbl_lr_1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.lbl_lr_1.Name = "lbl_lr_1";
			this.lbl_lr_1.Size = new System.Drawing.Size(45, 45);
			this.lbl_lr_1.TabIndex = 18;
			this.lbl_lr_1.Text = " 0";
			this.lbl_lr_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// dirLeftRightLabel
			// 
			this.dirLeftRightLabel.AutoSize = true;
			this.dirLeftRightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dirLeftRightLabel.Location = new System.Drawing.Point(1181, 65);
			this.dirLeftRightLabel.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.dirLeftRightLabel.Name = "dirLeftRightLabel";
			this.dirLeftRightLabel.Size = new System.Drawing.Size(29, 31);
			this.dirLeftRightLabel.TabIndex = 16;
			this.dirLeftRightLabel.Text = "0";
			// 
			// tabFine
			// 
			this.tabFine.BackColor = System.Drawing.Color.LemonChiffon;
			this.tabFine.Controls.Add(this.combinedImagePB);
			this.tabFine.Controls.Add(this.CoMLabel);
			this.tabFine.Location = new System.Drawing.Point(4, 48);
			this.tabFine.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.tabFine.Name = "tabFine";
			this.tabFine.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.tabFine.Size = new System.Drawing.Size(1354, 649);
			this.tabFine.TabIndex = 1;
			this.tabFine.Text = "FINE";
			// 
			// combinedImagePB
			// 
			this.combinedImagePB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.combinedImagePB.Location = new System.Drawing.Point(345, 29);
			this.combinedImagePB.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.combinedImagePB.Name = "combinedImagePB";
			this.combinedImagePB.Size = new System.Drawing.Size(604, 581);
			this.combinedImagePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.combinedImagePB.TabIndex = 13;
			this.combinedImagePB.TabStop = false;
			this.combinedImagePB.WaitOnLoad = true;
			// 
			// CoMLabel
			// 
			this.CoMLabel.AutoSize = true;
			this.CoMLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CoMLabel.Location = new System.Drawing.Point(997, 210);
			this.CoMLabel.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.CoMLabel.Name = "CoMLabel";
			this.CoMLabel.Size = new System.Drawing.Size(59, 24);
			this.CoMLabel.TabIndex = 36;
			this.CoMLabel.Text = "CoM:";
			// 
			// tabCal
			// 
			this.tabCal.BackColor = System.Drawing.Color.White;
			this.tabCal.Controls.Add(this.CalibrateImgPB);
			this.tabCal.Location = new System.Drawing.Point(4, 48);
			this.tabCal.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.tabCal.Name = "tabCal";
			this.tabCal.Size = new System.Drawing.Size(1354, 649);
			this.tabCal.TabIndex = 3;
			this.tabCal.Text = "CALIBRATE";
			// 
			// CalibrateImgPB
			// 
			this.CalibrateImgPB.Location = new System.Drawing.Point(288, 3);
			this.CalibrateImgPB.Name = "CalibrateImgPB";
			this.CalibrateImgPB.Size = new System.Drawing.Size(763, 638);
			this.CalibrateImgPB.TabIndex = 0;
			this.CalibrateImgPB.TabStop = false;
			// 
			// lbl_01
			// 
			this.lbl_01.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbl_01.Location = new System.Drawing.Point(15, 766);
			this.lbl_01.Name = "lbl_01";
			this.lbl_01.Size = new System.Drawing.Size(198, 24);
			this.lbl_01.TabIndex = 38;
			this.lbl_01.Text = "SEGMENT XXXX";
			// 
			// lbl_02
			// 
			this.lbl_02.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbl_02.Location = new System.Drawing.Point(232, 766);
			this.lbl_02.Name = "lbl_02";
			this.lbl_02.Size = new System.Drawing.Size(143, 24);
			this.lbl_02.TabIndex = 39;
			this.lbl_02.Text = "Alignment  X:";
			// 
			// lbl_03
			// 
			this.lbl_03.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbl_03.ForeColor = System.Drawing.Color.Green;
			this.lbl_03.Location = new System.Drawing.Point(381, 766);
			this.lbl_03.Name = "lbl_03";
			this.lbl_03.Size = new System.Drawing.Size(86, 24);
			this.lbl_03.TabIndex = 40;
			this.lbl_03.Text = "10mm";
			// 
			// lbl_04
			// 
			this.lbl_04.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbl_04.Location = new System.Drawing.Point(454, 766);
			this.lbl_04.Name = "lbl_04";
			this.lbl_04.Size = new System.Drawing.Size(49, 24);
			this.lbl_04.TabIndex = 41;
			this.lbl_04.Text = "  Y:";
			// 
			// lbl_05
			// 
			this.lbl_05.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbl_05.ForeColor = System.Drawing.Color.Green;
			this.lbl_05.Location = new System.Drawing.Point(509, 766);
			this.lbl_05.Name = "lbl_05";
			this.lbl_05.Size = new System.Drawing.Size(86, 24);
			this.lbl_05.TabIndex = 42;
			this.lbl_05.Text = "10mm";
			// 
			// lbl_06
			// 
			this.lbl_06.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbl_06.Location = new System.Drawing.Point(615, 766);
			this.lbl_06.Name = "lbl_06";
			this.lbl_06.Size = new System.Drawing.Size(80, 24);
			this.lbl_06.TabIndex = 43;
			this.lbl_06.Text = "Rot  X:";
			// 
			// lbl_07
			// 
			this.lbl_07.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbl_07.ForeColor = System.Drawing.Color.Red;
			this.lbl_07.Location = new System.Drawing.Point(701, 766);
			this.lbl_07.Name = "lbl_07";
			this.lbl_07.Size = new System.Drawing.Size(86, 24);
			this.lbl_07.TabIndex = 44;
			this.lbl_07.Text = "5 mrad";
			// 
			// lbl_09
			// 
			this.lbl_09.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbl_09.ForeColor = System.Drawing.Color.Red;
			this.lbl_09.Location = new System.Drawing.Point(826, 766);
			this.lbl_09.Name = "lbl_09";
			this.lbl_09.Size = new System.Drawing.Size(86, 24);
			this.lbl_09.TabIndex = 46;
			this.lbl_09.Text = "5 mrad";
			// 
			// lbl_08
			// 
			this.lbl_08.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbl_08.Location = new System.Drawing.Point(786, 766);
			this.lbl_08.Name = "lbl_08";
			this.lbl_08.Size = new System.Drawing.Size(34, 24);
			this.lbl_08.TabIndex = 45;
			this.lbl_08.Text = "Y:";
			// 
			// lbl_11
			// 
			this.lbl_11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbl_11.ForeColor = System.Drawing.Color.Green;
			this.lbl_11.Location = new System.Drawing.Point(948, 766);
			this.lbl_11.Name = "lbl_11";
			this.lbl_11.Size = new System.Drawing.Size(86, 24);
			this.lbl_11.TabIndex = 48;
			this.lbl_11.Text = "1 mrad";
			// 
			// lbl_10
			// 
			this.lbl_10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbl_10.Location = new System.Drawing.Point(908, 766);
			this.lbl_10.Name = "lbl_10";
			this.lbl_10.Size = new System.Drawing.Size(34, 24);
			this.lbl_10.TabIndex = 47;
			this.lbl_10.Text = "Z:";
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(1413, 799);
			this.Controls.Add(this.lbl_11);
			this.Controls.Add(this.lbl_10);
			this.Controls.Add(this.lbl_09);
			this.Controls.Add(this.lbl_08);
			this.Controls.Add(this.lbl_07);
			this.Controls.Add(this.lbl_06);
			this.Controls.Add(this.lbl_05);
			this.Controls.Add(this.lbl_04);
			this.Controls.Add(this.lbl_03);
			this.Controls.Add(this.lbl_02);
			this.Controls.Add(this.lbl_01);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.menuStrip1);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Location = new System.Drawing.Point(5, 5);
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.Name = "MainWindow";
			this.Text = "Mirror Alignment System";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabOverview.ResumeLayout(false);
			this.tabOverview.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.overviewImagePB)).EndInit();
			this.tabCoarse.ResumeLayout(false);
			this.tabCoarse.PerformLayout();
			this.gbCoarse.ResumeLayout(false);
			this.gbCoarse.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.upDownPBTwo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.upDownPBOne)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.leftRightPBOne)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.leftRightPBTwo)).EndInit();
			this.tabFine.ResumeLayout(false);
			this.tabFine.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.combinedImagePB)).EndInit();
			this.tabCal.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.CalibrateImgPB)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eventLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administrateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.Timer BIA_timer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabOverview;
        private System.Windows.Forms.TabPage tabFine;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.PictureBox overviewImagePB;
        private System.Windows.Forms.PictureBox combinedImagePB;
        private System.Windows.Forms.TabPage tabCoarse;
        private System.Windows.Forms.GroupBox gbCoarse;
        private System.Windows.Forms.Label lbl_lr_2;
        private System.Windows.Forms.Label lbl_lr_1;
        private System.Windows.Forms.Label dirUpDownLabel;
        private System.Windows.Forms.Label dirLeftRightLabel;
        private System.Windows.Forms.PictureBox upDownPBTwo;
        private System.Windows.Forms.PictureBox upDownPBOne;
        private System.Windows.Forms.PictureBox leftRightPBOne;
        private System.Windows.Forms.PictureBox leftRightPBTwo;
        private System.Windows.Forms.TabPage tabCal;
        private System.Windows.Forms.TextBox BlackBGNumberTextBox;
        private System.Windows.Forms.TextBox SegmentNumberTextbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label CoMLabel;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PictureBox CalibrateImgPB;
        private System.Windows.Forms.Button RTbutton;
        private System.Windows.Forms.Button LTbutton;
        private System.Windows.Forms.Button DWbutton;
        private System.Windows.Forms.Button UpButton;
		private System.Windows.Forms.Button btnCheckAllCoarse;
		private System.Windows.Forms.Button btnCheckAllFine;
		private System.Windows.Forms.Label lbl_01;
		private System.Windows.Forms.Label lbl_02;
		private System.Windows.Forms.Label lbl_03;
		private System.Windows.Forms.Label lbl_04;
		private System.Windows.Forms.Label lbl_05;
		private System.Windows.Forms.Label lbl_06;
		private System.Windows.Forms.Label lbl_07;
		private System.Windows.Forms.Label lbl_09;
		private System.Windows.Forms.Label lbl_08;
		private System.Windows.Forms.Label lbl_11;
		private System.Windows.Forms.Label lbl_10;
    }
}

