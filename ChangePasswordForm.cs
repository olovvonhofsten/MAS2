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
    ///  This class handles the form for when the user wants to change password
    /// </summary>
    public partial class ChangePasswordForm : Form
    {
        /// <summary>
        /// The class constructor
        /// </summary>
        public ChangePasswordForm()
        {
            InitializeComponent();

            textBox4.Focus();
        }

        /// <summary>
        /// Event method for when the user clicks the update button. Calls the DAL class which then updates the settings in the database
        /// </summary>
        /// <param name="sender">The control, in this method the button</param> 
        /// <param name="e">The event information</param> 
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (String.Compare(textBox1.Text.Trim(), "") == 0 || String.Compare(textBox2.Text.Trim(), "") == 0 || String.Compare(textBox3.Text.Trim(), "") == 0 || String.Compare(textBox4.Text.Trim(), "") == 0)
            {
                MessageBox.Show("Some information are missing");
            }
            else
            {
                if (String.Compare(textBox2.Text.Trim(), textBox3.Text.Trim()) == 0)
                {
                    if (DAL.ChangePassword(textBox4.Text, textBox1.Text, textBox2.Text))
                    {
                        MessageBox.Show("The password have been updated");

                        this.Hide();

                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                    }
                    else 
                    {
                        MessageBox.Show("Something went wrong, please contact the administrator");
                    }
                }
                else
                {
                    MessageBox.Show("The new password is not equal please retry");
                }

            }
        }
    }
}
