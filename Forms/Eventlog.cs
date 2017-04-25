using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MirrorAlignmentSystem
{
    /// <summary>
    ///  This class handles the EventLog form
    /// </summary>
    public partial class Eventlog : Form
    {
        /// <summary>
        /// The class constructor
        /// </summary>
        public Eventlog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event method for when the form is loaded
        /// </summary>
        /// <param name="sender">The control, in this method the form</param> 
        /// <param name="e">The event information</param> 
        private void Eventlog_Load(object sender, EventArgs e)
        {
            fromDatePicker.Value = new DateTime(2016, 04, 01);
            untilDatePicker.Value = DateTime.Now;

            string SQLQuery = "SELECT * FROM MirrorAlignmentSystemDB.dbo.EventLog WHERE TimeStamp>='" +
                                            fromDatePicker.Value + "' AND TimeStamp<='" + untilDatePicker.Value + "'";

            System.Diagnostics.Debug.WriteLine("SQLQuery: " + SQLQuery);
            dataGridView1.DataSource = DAL.GetEventLog(SQLQuery);
        }

        /// <summary>
        /// Event method for when the user clicks the update button. Calls the DAL class which then updates the settings in the database
        /// </summary>
        /// <param name="sender">The control, in this method the button</param> 
        /// <param name="e">The event information</param> 
        private void button1_Click(object sender, EventArgs e)
        {
            string SQLQuery = "SELECT * FROM MirrorAlignmentSystemDB.dbo.EventLog WHERE TimeStamp>='" +
                                                        fromDatePicker.Value + "' AND TimeStamp<='" + untilDatePicker.Value + "'";

            System.Diagnostics.Debug.WriteLine("SQLQuery: " + SQLQuery);
            dataGridView1.DataSource = DAL.GetEventLog(SQLQuery);
        }
    }
}
