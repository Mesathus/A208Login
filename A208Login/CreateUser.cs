using System;
using System.Windows.Forms;

namespace A208Login
{
    public partial class CreateUser : Form
    {
        /*
         * This form is for the creation of new entries in the Students DB.  Both the lab assistants and
         * admins should have access to this form.
         */

        public CreateUser()
        {
            InitializeComponent();            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*
         * This function handles inserting the students into the dbo.Students.  It performs validation
         * of name/passwords then calls a SQL function to check for duplicates in the StudentID DB field, 
         * then calls the insert SQL function to add the student to the dbo.Students.
         */
        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (firstBox.Text.Length > 0 && lastBox.Text.Length > 0)
                {
                    /*
                     * This section validates the name as password entry.  It creates boolean values
                     * that are set to false if the text entered in either the first or last name box
                     * contains a character other than a letter or apostrophe.  Current parameterization
                     * in the SQL statements and forbidding the SQL comment character in name entry has so far
                     * prevented injection and precluded the need for apostrophe escape sequencing
                     */
                    bool validname = true;
                    bool validpass = true;
                    string name = firstBox.Text.Trim();
                    foreach (char c in name)
                    {
                        if (!char.IsLetter(c) && c != '\'') //verifies that the name is composed only of
                        {                                   //letters and apostrophe
                            validname = false;                            
                        }
                    }
                    if(!validname)
                    {
                        MessageBox.Show("Please enter a valid first name.");
                    }                    
                    name = lastBox.Text.Trim();
                    foreach (char c in name)
                    {
                        if (!char.IsLetter(c) && c != '\'')
                        {
                            validname = false;                            
                        }
                    }
                    if(!validname)
                    {
                        MessageBox.Show("Please enter a valid last name.");
                    }
                    if (passBox.Text.Length == 7)   //checks to ensure the password is exactly 7 characters
                    {                               //can be adjusted based on current length of YT Student IDs
                        if (passBox.Text == confirmBox.Text)
                        {
                            string password = passBox.Text;
                            foreach (char n in password)
                            {
                                if (!char.IsDigit(n))   //verifies that the password entered is a number
                                {                       //could be replaced with a TryParse but an output
                                    validpass = false;  //value is unnecessary
                                }
                            }
                        }
                    }
                    if (!validpass)
                    {
                        MessageBox.Show("Please enter your seven digit student ID");
                    }
                    if (validname && validpass)
                    {
                        Valid student = new Valid(firstBox.Text, lastBox.Text, passBox.Text);                        
                        //perform a check for duplicate Student IDs
                        if(student.DupeCheck(passBox.Text))
                        {
                            MessageBox.Show("Duplicate Student ID entry detected.");
                        }
                        else if (student.InsertStudent(student))    //attempts to insert new student
                        {
                            MessageBox.Show("Student added.");                            
                        }
                        else
                        {
                            MessageBox.Show("Failed to add student.");
                        }
                        //clear and reset boxes to allow for multiple student entry
                        //can be changed to a simple .Close statement if one entry per form load is preferred
                        firstBox.Clear();
                        lastBox.Clear();
                        passBox.Clear();
                        confirmBox.Clear();
                        firstBox.Focus();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Failed to add student.");
            }
                   
        }
    }
}
