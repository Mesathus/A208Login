using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace A208Login
{
    public partial class CreateUpdateAdmin : Form
    {
        /*
         * This form is intended to create/update/delete Admin and LabAssist accounts from the dbo.Admins.
         * On form load it populates the two list boxes, one from which accounts can be selected for password
         * updates, and one from which accounts can be selected for deletion.  
         * 
         * The two lists are filled differently so that a base Administrator account is excluded from the 
         * deletion list but available for password update.  This prevents the program from being without an
         * Administrator account, but does leave it vulnerable to having the Administrator account password
         * changed should a user have access to that account. 
         */
        public CreateUpdateAdmin()
        {
            InitializeComponent();
            ListFill(true);
            ListFill(false);
        }

        /*
         * Similar to other ListFill methods present this is the method called on load and by other functions
         * to populate the listboxes with updated admin lists.  This method varies from the others in that
         * it requires a boolean value on this form, which is used to determine whether the listbox being
         * populated is associated with functions that can delete users from the dbo.Admins.
         */
        private void ListFill(bool delete)
        {
            if (delete)
            {
                removeListBox.Items.Clear();
                Valid openFill = new Valid();
                List<string> adminNames = new List<string>(openFill.AdminSearch(delete));
                if (adminNames.Count > 0)
                {
                    for (int i = 0; i < adminNames.Count; i++)
                    {
                        removeListBox.Items.Add(adminNames[i]);
                    }
                }
            }
            else
            {
                updateListBox.Items.Clear();
                Valid openFill = new Valid();
                List<string> adminNames = new List<string>(openFill.AdminSearch(delete));
                if (adminNames.Count > 0)
                {
                    for (int i = 0; i < adminNames.Count; i++)
                    {
                        updateListBox.Items.Add(adminNames[i]);
                    }
                }
            }
        }

        /*
         * Function for creating new admin accounts.  Validates that all required fields have been filled on 
         * button press and calls the appropriate Valid class function to insert
         */
        private void createButton_Click(object sender, EventArgs e)
        {
            if (createNameBox.Text != string.Empty)
            {
                if (createPassBox.Text != string.Empty && createConfirmBox.Text == createPassBox.Text)
                {
                    string type;
                    if (labRadioButton.Checked) //if statement to determine the account type being created
                    {
                        type = "LabAssist";
                    }
                    else
                    {
                        type = "Admin";
                    }
                    using (var myform = new AdminLogin())   //calls the AdminLogin form as an additional authorization
                    {
                        myform.ShowDialog();
                        if(myform.DialogResult == DialogResult.OK && (myform.authok == "Admin" || myform.authok == "Administrator"))
                        {
                            Valid Admin = new Valid();
                            if (Admin.InsertAdmin(createNameBox.Text, createPassBox.Text, type))
                            {
                                MessageBox.Show("New user created.");
                            }
                            ListFill(true);
                            ListFill(false);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Password confirmation invalid.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a name for this user.");
            }
        }

        /*
         * Function that manages updating passwords for the admin accounts.  This allows for the updating
         * of all accounts in the dbo.Admins, requiring the current password for security purposes.
         */
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (updateListBox.SelectedIndex != -1)
            {
                if (oldPassBox.Text == string.Empty || updatePassBox.Text == string.Empty || confirmPassBox.Text == string.Empty)
                {
                    MessageBox.Show("Please fill all password boxes.");
                }
                else
                {
                    Valid admin = new Valid();
                    if(admin.Auth(updateListBox.SelectedItem.ToString(), oldPassBox.Text))
                    {
                        if(admin.UpdateAdmin(updateListBox.SelectedItem.ToString(), updatePassBox.Text))
                        {
                            MessageBox.Show("Password update for " + updateListBox.SelectedItem.ToString() + " successful.");
                        }

                    }
                }
            }
        }

        /*
         * Function to remove an account from the dbo.Admins.  This function calls the AdminLogin form as
         * additional authorization.  It is also unable to remove the base Administrator account as it should
         * not be an available option in the listbox.  
         */
        private void removeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(removeListBox.SelectedIndex != -1)
                {
                    using (var myform = new AdminLogin())   //calling the AdminLogin form for additional security
                    {
                        myform.ShowDialog();
                        if(myform.DialogResult == DialogResult.OK && (myform.authok == "Admin" || myform.authok == "Administrator"))
                        {
                            Valid admin = new Valid();
                            if (admin.RemoveAdmin(removeListBox.SelectedItem.ToString()))
                            {
                                MessageBox.Show("Selected admin removed.");
                                ListFill(true);
                                ListFill(false);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
}
