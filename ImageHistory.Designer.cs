namespace MirrorAlignmentSystem
{
    partial class ImageHistory
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
            this.filterGroupBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.untilLabel = new System.Windows.Forms.Label();
            this.untilDatePicker = new System.Windows.Forms.DateTimePicker();
            this.fromLabel = new System.Windows.Forms.Label();
            this.fromDatePicker = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.filterGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // filterGroupBox
            // 
            this.filterGroupBox.Controls.Add(this.button1);
            this.filterGroupBox.Controls.Add(this.untilLabel);
            this.filterGroupBox.Controls.Add(this.untilDatePicker);
            this.filterGroupBox.Controls.Add(this.fromLabel);
            this.filterGroupBox.Controls.Add(this.fromDatePicker);
            this.filterGroupBox.Location = new System.Drawing.Point(12, 6);
            this.filterGroupBox.Name = "filterGroupBox";
            this.filterGroupBox.Size = new System.Drawing.Size(750, 60);
            this.filterGroupBox.TabIndex = 11;
            this.filterGroupBox.TabStop = false;
            this.filterGroupBox.Text = "Filter";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(577, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 21);
            this.button1.TabIndex = 9;
            this.button1.Text = "Apply filter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // untilLabel
            // 
            this.untilLabel.AutoSize = true;
            this.untilLabel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.untilLabel.Location = new System.Drawing.Point(163, 16);
            this.untilLabel.Name = "untilLabel";
            this.untilLabel.Size = new System.Drawing.Size(28, 13);
            this.untilLabel.TabIndex = 4;
            this.untilLabel.Text = "Until";
            // 
            // untilDatePicker
            // 
            this.untilDatePicker.Location = new System.Drawing.Point(166, 32);
            this.untilDatePicker.Name = "untilDatePicker";
            this.untilDatePicker.Size = new System.Drawing.Size(151, 20);
            this.untilDatePicker.TabIndex = 3;
            // 
            // fromLabel
            // 
            this.fromLabel.AutoSize = true;
            this.fromLabel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.fromLabel.Location = new System.Drawing.Point(6, 16);
            this.fromLabel.Name = "fromLabel";
            this.fromLabel.Size = new System.Drawing.Size(30, 13);
            this.fromLabel.TabIndex = 2;
            this.fromLabel.Text = "From";
            // 
            // fromDatePicker
            // 
            this.fromDatePicker.Location = new System.Drawing.Point(9, 32);
            this.fromDatePicker.Name = "fromDatePicker";
            this.fromDatePicker.Size = new System.Drawing.Size(151, 20);
            this.fromDatePicker.TabIndex = 1;
            this.fromDatePicker.Value = new System.DateTime(2016, 4, 1, 0, 0, 0, 0);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 72);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(749, 355);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // ImageHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 439);
            this.Controls.Add(this.filterGroupBox);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ImageHistory";
            this.Text = "ImageHistory";
            this.Load += new System.EventHandler(this.ImageHistory_Load);
            this.SizeChanged += new System.EventHandler(this.ImageHistory_SizeChanged);
            this.filterGroupBox.ResumeLayout(false);
            this.filterGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox filterGroupBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label untilLabel;
        private System.Windows.Forms.DateTimePicker untilDatePicker;
        private System.Windows.Forms.Label fromLabel;
        private System.Windows.Forms.DateTimePicker fromDatePicker;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}