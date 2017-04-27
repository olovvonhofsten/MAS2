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
    ///  This class handles the ImageHistory form
    /// </summary>
    public partial class ImageHistory : Form
    {
        /// <summary>
        /// The class constructor
        /// </summary>
        public ImageHistory()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event method for when the form is loaded
        /// </summary>
        /// <param name="sender">The control, in this method the form</param> 
        /// <param name="e">The event information</param> 
        private void ImageHistory_Load(object sender, EventArgs e)
        {
            fromDatePicker.Value = new DateTime(2016, 04, 01);
            untilDatePicker.Value = DateTime.Now;

            string SQLQuery = "SELECT * FROM MirrorAlignmentSystemDB.dbo.Images WHERE TimeStamp>='" +
                              fromDatePicker.Value + "' AND TimeStamp<='" + untilDatePicker.Value + "'";

            System.Diagnostics.Debug.WriteLine("SQLQuery: " + SQLQuery);
            dataGridView1.DataSource = DAL.GetEventLog(SQLQuery);
        }

        /// <summary>
        /// Event method for when the user clicks the update button. Calls the DAL class which then returns a datasource with the requested data
        /// </summary>
        /// <param name="sender">The control, in this method the button</param> 
        /// <param name="e">The event information</param> 
        private void button1_Click(object sender, EventArgs e)
        {
            string SQLQuery = "SELECT * FROM MirrorAlignmentSystemDB.dbo.Images WHERE TimeStamp>='" +
                              fromDatePicker.Value + "' AND TimeStamp<='" + untilDatePicker.Value + "'";

            System.Diagnostics.Debug.WriteLine("SQLQuery: " + SQLQuery);
            dataGridView1.DataSource = DAL.GetEventLog(SQLQuery);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ImageHistory_SizeChanged(object sender, EventArgs e)
        {
            dataGridView1.Height = this.Height - 84;
            dataGridView1.Width = this.Width - 24;

            dataGridView1.Location = new Point(12, 72);
        }
    }
}
