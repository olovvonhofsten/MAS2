namespace CameraAlignment
{
	partial class CameraAlignmentWindow
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
			this.Xoffset_label = new System.Windows.Forms.Label();
			this.Yoffset_label = new System.Windows.Forms.Label();
			this.xOffsetTB = new System.Windows.Forms.TextBox();
			this.yOffsetTB = new System.Windows.Forms.TextBox();
			this.CameraAlignment_pictureBox = new System.Windows.Forms.PictureBox();
			this.SetCrosshair_button = new System.Windows.Forms.Button();
			this.zOffsetTB = new System.Windows.Forms.TextBox();
			this.Zoffset_label = new System.Windows.Forms.Label();
			this.Exposure_trackBar = new System.Windows.Forms.TrackBar();
			this.Exposure_label = new System.Windows.Forms.Label();
			this.ExposureMin_label = new System.Windows.Forms.Label();
			this.ExposureMax_label = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.CameraAlignment_pictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Exposure_trackBar)).BeginInit();
			this.SuspendLayout();
			// 
			// Xoffset_label
			// 
			this.Xoffset_label.AutoSize = true;
			this.Xoffset_label.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Xoffset_label.Location = new System.Drawing.Point(12, 573);
			this.Xoffset_label.Name = "Xoffset_label";
			this.Xoffset_label.Size = new System.Drawing.Size(149, 25);
			this.Xoffset_label.TabIndex = 3;
			this.Xoffset_label.Text = "X Offset (mm)";
			// 
			// Yoffset_label
			// 
			this.Yoffset_label.AutoSize = true;
			this.Yoffset_label.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Yoffset_label.Location = new System.Drawing.Point(283, 573);
			this.Yoffset_label.Name = "Yoffset_label";
			this.Yoffset_label.Size = new System.Drawing.Size(148, 25);
			this.Yoffset_label.TabIndex = 4;
			this.Yoffset_label.Text = "Y Offset (mm)";
			// 
			// xOffsetTB
			// 
			this.xOffsetTB.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.xOffsetTB.Location = new System.Drawing.Point(167, 570);
			this.xOffsetTB.Name = "xOffsetTB";
			this.xOffsetTB.Size = new System.Drawing.Size(100, 32);
			this.xOffsetTB.TabIndex = 5;
			this.xOffsetTB.Text = "0";
			// 
			// yOffsetTB
			// 
			this.yOffsetTB.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.yOffsetTB.Location = new System.Drawing.Point(437, 570);
			this.yOffsetTB.Name = "yOffsetTB";
			this.yOffsetTB.Size = new System.Drawing.Size(100, 32);
			this.yOffsetTB.TabIndex = 6;
			this.yOffsetTB.Text = "0";
			// 
			// CameraAlignment_pictureBox
			// 
			this.CameraAlignment_pictureBox.Location = new System.Drawing.Point(12, 64);
			this.CameraAlignment_pictureBox.Name = "CameraAlignment_pictureBox";
			this.CameraAlignment_pictureBox.Size = new System.Drawing.Size(821, 478);
			this.CameraAlignment_pictureBox.TabIndex = 7;
			this.CameraAlignment_pictureBox.TabStop = false;
			// 
			// SetCrosshair_button
			// 
			this.SetCrosshair_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SetCrosshair_button.Location = new System.Drawing.Point(677, 623);
			this.SetCrosshair_button.Name = "SetCrosshair_button";
			this.SetCrosshair_button.Size = new System.Drawing.Size(120, 40);
			this.SetCrosshair_button.TabIndex = 8;
			this.SetCrosshair_button.Text = "Set Crosshair";
			this.SetCrosshair_button.UseVisualStyleBackColor = true;
			this.SetCrosshair_button.Click += new System.EventHandler(this.SetCrosshair_button_Click);
			// 
			// zOffsetTB
			// 
			this.zOffsetTB.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.zOffsetTB.Location = new System.Drawing.Point(697, 570);
			this.zOffsetTB.Name = "zOffsetTB";
			this.zOffsetTB.Size = new System.Drawing.Size(100, 32);
			this.zOffsetTB.TabIndex = 10;
			this.zOffsetTB.Text = "7000";
			// 
			// Zoffset_label
			// 
			this.Zoffset_label.AutoSize = true;
			this.Zoffset_label.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Zoffset_label.Location = new System.Drawing.Point(543, 573);
			this.Zoffset_label.Name = "Zoffset_label";
			this.Zoffset_label.Size = new System.Drawing.Size(147, 25);
			this.Zoffset_label.TabIndex = 9;
			this.Zoffset_label.Text = "Z Offset (mm)";
			// 
			// Exposure_trackBar
			// 
			this.Exposure_trackBar.Location = new System.Drawing.Point(110, 13);
			this.Exposure_trackBar.Name = "Exposure_trackBar";
			this.Exposure_trackBar.Size = new System.Drawing.Size(723, 45);
			this.Exposure_trackBar.TabIndex = 11;
			this.Exposure_trackBar.Scroll += new System.EventHandler(this.Exposure_trackBar_Scroll);
			// 
			// Exposure_label
			// 
			this.Exposure_label.AutoSize = true;
			this.Exposure_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Exposure_label.Location = new System.Drawing.Point(12, 13);
			this.Exposure_label.Name = "Exposure_label";
			this.Exposure_label.Size = new System.Drawing.Size(76, 20);
			this.Exposure_label.TabIndex = 10;
			this.Exposure_label.Text = "Exposure";
			// 
			// ExposureMin_label
			// 
			this.ExposureMin_label.AutoSize = true;
			this.ExposureMin_label.Location = new System.Drawing.Point(110, 45);
			this.ExposureMin_label.Name = "ExposureMin_label";
			this.ExposureMin_label.Size = new System.Drawing.Size(24, 13);
			this.ExposureMin_label.TabIndex = 12;
			this.ExposureMin_label.Text = "Min";
			// 
			// ExposureMax_label
			// 
			this.ExposureMax_label.AutoSize = true;
			this.ExposureMax_label.Location = new System.Drawing.Point(809, 45);
			this.ExposureMax_label.Name = "ExposureMax_label";
			this.ExposureMax_label.Size = new System.Drawing.Size(27, 13);
			this.ExposureMax_label.TabIndex = 13;
			this.ExposureMax_label.Text = "Max";
			// 
			// CameraAlignmentWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(845, 687);
			this.Controls.Add(this.ExposureMax_label);
			this.Controls.Add(this.ExposureMin_label);
			this.Controls.Add(this.Exposure_label);
			this.Controls.Add(this.Exposure_trackBar);
			this.Controls.Add(this.zOffsetTB);
			this.Controls.Add(this.Zoffset_label);
			this.Controls.Add(this.SetCrosshair_button);
			this.Controls.Add(this.CameraAlignment_pictureBox);
			this.Controls.Add(this.yOffsetTB);
			this.Controls.Add(this.xOffsetTB);
			this.Controls.Add(this.Yoffset_label);
			this.Controls.Add(this.Xoffset_label);
			this.Name = "CameraAlignmentWindow";
			this.Text = "Form1";
			this.Shown += new System.EventHandler(this.InitCamera);
			((System.ComponentModel.ISupportInitialize)(this.CameraAlignment_pictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Exposure_trackBar)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label Xoffset_label;
		private System.Windows.Forms.Label Yoffset_label;
		private System.Windows.Forms.TextBox xOffsetTB;
		private System.Windows.Forms.TextBox yOffsetTB;
		private System.Windows.Forms.PictureBox CameraAlignment_pictureBox;
		private System.Windows.Forms.Button SetCrosshair_button;
		private System.Windows.Forms.TextBox zOffsetTB;
		private System.Windows.Forms.Label Zoffset_label;
		private System.Windows.Forms.TrackBar Exposure_trackBar;
		private System.Windows.Forms.Label Exposure_label;
		private System.Windows.Forms.Label ExposureMin_label;
		private System.Windows.Forms.Label ExposureMax_label;
	}
}

