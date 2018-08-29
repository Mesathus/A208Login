using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace A208Login
{
    public partial class Logout : Form
    {
        /*
         * The student logout form has been repurposed for use by lab assistants and administrators to
         * log out single students who fail to log themselves out before leaving.  As such, changing to
         * this form now requires going through the AdminLogin form.
         * This form receives a list object when it is called from the StudentLogin form that contains
         * the currently logged in students.  It returns an updated version of this list when the form
         * closes after a student is logged out.
         */
        public Logout(List<string> loggedin)
        {
            InitializeComponent();
            loginListBox.Items.Clear();
            for(int i = 0; i < loggedin.Count; i++) //populates the list with currently logged in students
            {
                loginListBox.Items.Add(loggedin[i]);
            }
            currentStudents = loggedin; //assigns the login list to this forms global list so it can be updated
            loggedin = null;            //by other functions
        }        
        public List<string> currentStudents { get; set; }   //maintains a global list of logged in students


        /*
         * This logout button functions similarly to the logout feature on the StudentLogin form with
         * the exception that it does not require the StudentID to be entered for password authentication.
         * This is due to the form only being accessible to lab assistants who do not have students
         * individual passwords to log them out with
         */
        private void outButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(loginListBox.SelectedIndex != -1)
                {
                    string name = loginListBox.SelectedItem.ToString();
                    string[] fullname = name.Split(',');                    //tokenizes name for searching the DB
                    string lastname = fullname[0].Trim();
                    string firstname = fullname[1].Trim();
                    Valid student = new Valid(firstname, lastname, null);                    
                    //create/append a log file with name, ID, major and timestamp
                    //add to a list for the logout page
                    if (AppendLog(student))
                    {
                        MessageBox.Show("Logout successful.");
                        currentStudents.Remove(name);
                        this.currentStudents = currentStudents;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }                    
                }
                else
                {
                    MessageBox.Show("Please select a student.");
                }            
            }
            catch
            {

            }
        }

        /*
         * This is functionally identical to the AppendLog in the StudentLogin form with the exception
         * that is does not try to create a password entry in the log string
         */
        private bool AppendLog(Valid student)  //Will append a log file of the current date with student name
        {
            try
            {                
                string logDir = Path.Combine(Environment.CurrentDirectory, "Logs");
                DateTime dt = DateTime.Now;
                string date = dt.ToShortDateString();
                string time = dt.ToShortTimeString();
                string[] newdate = (dt.Date.ToString()).Split();
                string[] dateparts = newdate[0].Split('/');
                date = dateparts[0] + "-" + dateparts[1] + "-" + dateparts[2];
                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }
                string logpath = date + ".txt";
                StreamWriter outputfile = new StreamWriter("Logs\\" + logpath, true);  //look at how to specify directories            
                string encryptText = Utility.Encrypt(student.First + "," + student.Last + ",," + date + ",Forced: ," + time, false);
                outputfile.WriteLine(encryptText);
                outputfile.Close();
                return true;
            }
            catch
            {
                MessageBox.Show("Error writing to log.");
            }
            return false;
        }

        private void returnToLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
