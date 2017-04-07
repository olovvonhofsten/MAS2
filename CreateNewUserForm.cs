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
    ///  This class handles the CreateNewUser form
    /// </summary>
    public partial class CreateNewUserForm : Form
    {
        /// <summary>
        /// The class constructor
        /// </summary>
        public CreateNewUserForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event method for when the user clicks the create new user button. Check that everything is in order before calling DAL to create the user
        /// </summary>
        /// <param name="sender">The control, in this method the button</param> 
        /// <param name="e">The event information</param> 
        private void createNewUserButton_Click(object sender, EventArgs e)
        {
            if (passwordTB.Text == repeatPasswordTB.Text)
            {
                if (usernameTB.Text.Trim() != "")
                {
                    if (DAL.AddUser(usernameTB.Text, passwordTB.Text))
                    {
                        MessageBox.Show("User added successfully, please contact admin to be put in the correct user group");

                        this.Dispose();
                    }
                    else 
                    {
                        MessageBox.Show("Something went wrong, please contact the administrator");
                    }
                }
                else 
                {
                    MessageBox.Show("Please enter a username");
                }
            }
            else 
            {
                MessageBox.Show("The passwords doesn't match eachother");
            }
       }
    }
}
