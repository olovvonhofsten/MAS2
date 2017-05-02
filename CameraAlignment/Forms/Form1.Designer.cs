namespace CameraAlignment
{
	partial class Form1
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
			this.imageBox1 = new Emgu.CV.UI.ImageBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.xOffsetTB = new System.Windows.Forms.TextBox();
			this.yOffsetTB = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// imageBox1
			// 
			this.imageBox1.Location = new System.Drawing.Point(13, 13);
			this.imageBox1.Name = "imageBox1";
			this.imageBox1.Size = new System.Drawing.Size(803, 514);
			this.imageBox1.TabIndex = 2;
			this.imageBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(34, 566);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(149, 25);
			this.label1.TabIndex = 3;
			this.label1.Text = "X Offset (mm)";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(365, 566);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(148, 25);
			this.label2.TabIndex = 4;
			this.label2.Text = "Y Offset (mm)";
			// 
			// xOffsetTB
			// 
			this.xOffsetTB.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.xOffsetTB.Location = new System.Drawing.Point(189, 566);
			this.xOffsetTB.Name = "xOffsetTB";
			this.xOffsetTB.Size = new System.Drawing.Size(100, 32);
			this.xOffsetTB.TabIndex = 5;
			// 
			// yOffsetTB
			// 
			this.yOffsetTB.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.yOffsetTB.Location = new System.Drawing.Point(519, 571);
			this.yOffsetTB.Name = "yOffsetTB";
			this.yOffsetTB.Size = new System.Drawing.Size(100, 32);
			this.yOffsetTB.TabIndex = 6;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(845, 628);
			this.Controls.Add(this.yOffsetTB);
			this.Controls.Add(this.xOffsetTB);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.imageBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Emgu.CV.UI.ImageBox imageBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox xOffsetTB;
		private System.Windows.Forms.TextBox yOffsetTB;
	}
}

