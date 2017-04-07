namespace MirrorAlignmentSystem
{
    partial class OptionForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.exposureTimeTB = new System.Windows.Forms.TextBox();
            this.thresholdTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.waitTimeMonitorTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.waitTimeCycleTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.yOffsetTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.xOffsetTB = new System.Windows.Forms.TextBox();
            this.framerateTB = new System.Windows.Forms.TextBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(7, 136);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Update settings";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // exposureTimeTB
            // 
            this.exposureTimeTB.Location = new System.Drawing.Point(248, 282);
            this.exposureTimeTB.Name = "exposureTimeTB";
            this.exposureTimeTB.Size = new System.Drawing.Size(54, 20);
            this.exposureTimeTB.TabIndex = 2;
            this.exposureTimeTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.exposureTimeTB_KeyDown);
            // 
            // thresholdTB
            // 
            this.thresholdTB.Location = new System.Drawing.Point(105, 8);
            this.thresholdTB.Name = "thresholdTB";
            this.thresholdTB.Size = new System.Drawing.Size(54, 20);
            this.thresholdTB.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Threshold:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Wait time monitor:";
            // 
            // waitTimeMonitorTB
            // 
            this.waitTimeMonitorTB.Location = new System.Drawing.Point(105, 34);
            this.waitTimeMonitorTB.Name = "waitTimeMonitorTB";
            this.waitTimeMonitorTB.Size = new System.Drawing.Size(54, 20);
            this.waitTimeMonitorTB.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Wait time cycle:";
            // 
            // waitTimeCycleTB
            // 
            this.waitTimeCycleTB.Location = new System.Drawing.Point(105, 59);
            this.waitTimeCycleTB.Name = "waitTimeCycleTB";
            this.waitTimeCycleTB.Size = new System.Drawing.Size(54, 20);
            this.waitTimeCycleTB.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Y-Offset:";
            // 
            // yOffsetTB
            // 
            this.yOffsetTB.Location = new System.Drawing.Point(105, 110);
            this.yOffsetTB.Name = "yOffsetTB";
            this.yOffsetTB.Size = new System.Drawing.Size(54, 20);
            this.yOffsetTB.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "X-Offset:";
            // 
            // xOffsetTB
            // 
            this.xOffsetTB.Location = new System.Drawing.Point(105, 85);
            this.xOffsetTB.Name = "xOffsetTB";
            this.xOffsetTB.Size = new System.Drawing.Size(54, 20);
            this.xOffsetTB.TabIndex = 16;
            // 
            // framerateTB
            // 
            this.framerateTB.Location = new System.Drawing.Point(248, 209);
            this.framerateTB.Name = "framerateTB";
            this.framerateTB.Size = new System.Drawing.Size(54, 20);
            this.framerateTB.TabIndex = 20;
            this.framerateTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.framerateTB_KeyDown);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(21, 211);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(226, 45);
            this.trackBar1.TabIndex = 22;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 1;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(21, 282);
            this.trackBar2.Maximum = 150;
            this.trackBar2.Minimum = 1;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(226, 45);
            this.trackBar2.TabIndex = 23;
            this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar2.Value = 1;
            this.trackBar2.ValueChanged += new System.EventHandler(this.trackBar2_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 195);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "FPS:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 268);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "Exposure time:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(25, 232);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "label12";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(201, 232);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 29;
            this.label13.Text = "label13";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(201, 303);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 13);
            this.label14.TabIndex = 31;
            this.label14.Text = "label14";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(26, 303);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 13);
            this.label15.TabIndex = 30;
            this.label15.Text = "label15";
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 331);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.framerateTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.yOffsetTB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.xOffsetTB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.waitTimeCycleTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.waitTimeMonitorTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.thresholdTB);
            this.Controls.Add(this.exposureTimeTB);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OptionForm";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.OptionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox exposureTimeTB;
        private System.Windows.Forms.TextBox thresholdTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox waitTimeMonitorTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox waitTimeCycleTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox yOffsetTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox xOffsetTB;
        private System.Windows.Forms.TextBox framerateTB;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
    }
}