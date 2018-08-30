using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace A208Login
{
    public partial class RemoveStudent : Form
    {
        /*
         * This is an Admin only form that removes students from the database.  It populates a list of
         * student names and the associated StudentIDs to allow faculty to ensure they are removing the
         * correct student.
         */
        public RemoveStudent()
        {
            InitializeComponent();
            OpenFill();
        }

        /*
         * This OpenFill method is functionally similar to the other listbox filling methods throughout
         * the forms, the key difference being this also populates the listbox with StudentIDs for 
         * clarity in the removal process
         */
        private void OpenFill()  //populates the student list with the full database of students
        {
            studentListBox.Items.Clear();
            Valid openFill = new Valid();
            List<string> studentNames = new List<string>(openFill.StudentSearch("%", true));
            if (studentNames.Count > 0)
            {
                for (int i = 0; i < studentNames.Count; i++)
                {
                    string[] student = studentNames[i].Split(',');
                    studentListBox.Items.Add(student[0].Trim() + ", " + student[1].Trim() + ", " + Utility.Decrypt(student[2], false).Trim());
                }
            }
        }

        /*
         * This event handler filters the student list in the same way as the StudentLogin form
         * It carries the same restriction of only filtering by last name
         */
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            string name = searchBox.Text;
            if (name.Length == 0)   //sends a wildcard to the database for a full list when the search box is empty
            {
                name = "%";
            }
            if (name.Length > 1)    //escape sequence for names with apostrophes
            {
                if (name.Contains("'"))
                {
                    for (int i = 0; i < name.Length; i++)
                    {
                        if (name[i] == '\'')
                        {
                            name = name.Insert(i, "\'");
                            i++;
                        }
                    }
                }
            }
            else if (name.Length == 1 && name == "\'")
            {
                name += "'";
            }
            try
            {
                studentListBox.Items.Clear();
                Valid students = new Valid();
                List<string> studentNames = new List<string>(students.StudentSearch(name, true));
                if (studentNames.Count > 0)
                {
                    for (int i = 0; i < studentNames.Count; i++)
                    {
                        string[] student = studentNames[i].Split(',');
                        studentListBox.Items.Add(student[0].Trim() + ", " + student[1].Trim() + ", " + Utility.Decrypt(student[2], false).Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something is broken in the list fill.", ex.Message);
            }
        }

        /*
         * This is the event handler that removes the selected student from the database.  It requires
         * secondary authentication as an added precaution as it does modify the database.
         */
        private void removeButton_Click(object sender, EventArgs e)
        {
            if (studentListBox.SelectedIndex != -1)
            {
                using (var myform = new AdminLogin())
                {
                    //myform.ShowDialog();
                    //if (myform.DialogResult == DialogResult.OK && (myform.authok == "Admin" || myform.authok == "Administrator"))
                    //{
                        string name = studentListBox.SelectedItem.ToString();
                        string[] fullname = name.Split(',');                    //tokenizes name for searching the DB
                        string lastname = fullname[0].Trim();
                        string firstname = fullname[1].Trim();
                        string password = fullname[2].Trim();
                        Valid student = new Valid(lastname, firstname, Utility.Encrypt(password, false));
                        if (student.RemoveStudent(student)) //executes the remove SQL query and returns a boolean if successful
                        {
                            MessageBox.Show("Student removed.");
                            OpenFill();
                        }
                        else
                        {
                            MessageBox.Show("Error removing student.");
                        }
                    //}
                    /*else
                    {
                        MessageBox.Show("Failed to remove student.");
                    }*/
                }
            }
        }
    }
}
