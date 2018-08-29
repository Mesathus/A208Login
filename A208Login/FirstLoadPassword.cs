using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A208Login
{
    public partial class FirstLoadPassword : Form
    {
        /*
         * This form is intended to only be opened once to allow for an Administrator
         * password to be created on a fresh database.  This should only occur on the 
         * first program load, once a password is created the function i
         */
        public FirstLoadPassword()
        {
            InitializeComponent();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            string password = passBox.Text;
            if(password.Length >= 8)
            {
                if(password == confirmBox.Text)
                {
                    Valid admin = new Valid();
                    if(admin.UpdateAdmin("Administrator", password))
                    {
                        MessageBox.Show("Administrator password has been updated successfully.");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("An error has occured while updating Administrator password.");
                    }
                }
                else
                {
                    MessageBox.Show("Passwords do not match, please re-enter.");
                    passBox.Clear();
                    confirmBox.Clear();
                }
            }
            else
            {
                MessageBox.Show("Please enter a password of at least eight characters.");
                passBox.Clear();
                confirmBox.Clear();
            }
        }
    }
}
