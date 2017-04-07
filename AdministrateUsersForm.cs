using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MirrorAlignmentSystem
{
    /// <summary>
    ///  This class handles the form that take care of the administration form
    /// </summary>
    public partial class AdministrateUsersForm : Form
    {
        string userToChange;
        string newUserType;

        /// <summary>
        /// The class constructor
        /// </summary>
        public AdministrateUsersForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event method for when the form is loaded
        /// </summary>
        /// <param name="sender">The control, in this method the form</param> 
        /// <param name="e">The event information</param> 
        private void AdministrateUsersForm_Load(object sender, EventArgs e)
        {
            string SQLQuery = "SELECT * FROM UserData";

            //System.Diagnostics.Debug.WriteLine("SQLQuery: " + SQLQuery);

            System.Diagnostics.Debug.WriteLine("SQLQuery: " + SQLQuery);
            dataGridView1.DataSource = DAL.GetUserData(SQLQuery);

            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
        }

        /// <summary>
        /// Event method for when the user leaves a row. Calls the DAL to update the user type
        /// </summary>
        /// <param name="sender">The control, in this method the datagridview</param> 
        /// <param name="e">The event information</param> 
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("LEAVING");

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            newUserType = Convert.ToString(selectedRow.Cells["Usertype"].Value);

            DAL.UpdateUserType(userToChange, newUserType);
        }

        /// <summary>
        /// Event method for when the user enters a row. Let's the program know which user that is going to be changed
        /// </summary>
        /// <param name="sender">The control, in this method the datagridview</param> 
        /// <param name="e">The event information</param> 
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ENTER");

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            userToChange = Convert.ToString(selectedRow.Cells["Username"].Value);
        }

        private void updateButton_Click(object sender, EventArgs e)
        {

        }
    }
}
