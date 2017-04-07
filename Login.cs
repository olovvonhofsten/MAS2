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
    ///  This class handles the OptionForm 
    /// </summary>
    public partial class Login : Form
    {
        ChangePasswordForm changePasswordForm = new ChangePasswordForm();

        /// <summary>
        /// The class constructor
        /// </summary>
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event method for when the user clicks the new user button. Open up a new form which will handle the new user
        /// </summary>
        /// <param name="sender">The control, in this method the button</param> 
        /// <param name="e">The event information</param> 
        private void newUserButton_Click(object sender, EventArgs e)
        {
            CreateNewUserForm newUserForm = new CreateNewUserForm();

            newUserForm.TopMost = true;

            newUserForm.Show();
        }

        /// <summary>
        /// Event method for when the user clicks the login button. Asks the DAL for the correct password and makes sure it is correct
        /// or tell the user otherwise
        /// </summary>
        /// <param name="sender">The control, in this method the button</param> 
        /// <param name="e">The event information</param> 
        private void loginButton_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("PASSWORD DB: " + DAL.GetPasswordForUser(usernameTB.Text));
            //System.Diagnostics.Debug.WriteLine("PASSWORD TB: " + passwordTB.Text);

            string username = DAL.GetPasswordForUser(usernameTB.Text).Trim();

            if (String.Compare(username, "") == 0)
            {
                MessageBox.Show("Username does not exist, please try again");
            }
            else if (String.Compare(username, passwordTB.Text) == 0)
            {
                //System.Diagnostics.Debug.WriteLine("Lösenordet stämmer");
                CurrentUser.SetCurrentUser(usernameTB.Text);

                CurrentUser.SetCurrentUserType(DAL.GetUserTypeForUser(usernameTB.Text));

                this.Hide();
            }
            else 
            {
                //System.Diagnostics.Debug.WriteLine("Lösenordet stämmer inte");
                MessageBox.Show("Wrong password, please try again, use '" + username + "' next time.");
            }
        }

        /// <summary>
        /// Event method for when the user clicks the enter button when inside the password textbox. 
        /// </summary>
        /// <param name="sender">The control, in this method the textbox</param> 
        /// <param name="e">The event information</param> 
        private void passwordTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                loginButton_Click(sender, e);
            }
        }

        /// <summary>
        /// Event method for when the user clicks the change password button. Open up a new form which will handle the password change
        /// </summary>
        /// <param name="sender">The control, in this method the button</param> 
        /// <param name="e">The event information</param> 
        private void changePasswordButton_Click(object sender, EventArgs e)
        {
            changePasswordForm.TopMost = true;

            changePasswordForm.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
