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
    public partial class CreateUserForm : Form
    {
        public CreateUserForm()
        {
            InitializeComponent();
        }

        private void createUserButton_Click(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == repeatPasswordTextBox.Text)
            {
                if(usernameTextBox.Text.Trim() != "")
                {
                    if (DAL.AddUser(usernameTextBox.Text.Trim(), passwordTextBox.Text))
                    {
                        MessageBox.Show("User added Succesfully, inform the admin to put you in the correct user group");
                        usernameTextBox.Text = "";
                        passwordTextBox.Text = "";
                        repeatPasswordTextBox.Text = "";

                        this.Hide();
                    }
                    else 
                    {
                        MessageBox.Show("Something went wrong, contact the administrator");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a username");
                }
            }
            else 
            {
                MessageBox.Show("The passwords is not identical");
            }
        }
    }
}
