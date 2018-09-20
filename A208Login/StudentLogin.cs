using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/*
 * Created by: John O'Brien with contributions by YTC lab staff and instructors
 * Version: 1.0
 * Created: Feb 2018
 * Current Version: 1.4
 * Last Updated: 9/4/2018
 * Originally designed for YTC student lab
 * See attached document for initial design specs and notes
 * Initial Administrator password: ComeAtMeBro
 * Initial Lab Assistant password: DontWantNone
 * Initial Admin password intended for testing: yep
 *      The above account should be removed or changed before implementation
 * Encryption key is located in the Utility class
 * If the encryption key is changed, ensure a fresh Admin database is loaded to prompt
 * entry of an Administrator password, otherwise SQL functions will not retrieve decrypted
 * passwords properly
 * All SQL information is located in the Valid(ation) class
 */
 /*
  * 1.4 notes: New feature added to allow for creation of an Administrator password on first program
  * load with an empty database.  This is intended to ease changing the encryption key as default
  * administrator passwords are tied to the default encryption key and must be cleared if the key 
  * is to be changed.
  * 
  * 1.3 notes: New feature added to Admin tasks.  Menu strip item has been added to the logging section
  * to allow for a full database to CSV dump.  As students must be purged manually from this database, 
  * this feature was added to aid admins in comparing the database to lists of currently enrolled students.
  * 
  * 1.2 notes: This version removes the unique name restriction by handling password authentication
  * differently.  Comments have been updated to reflect.
  * 
  * 1.1 notes: General bug fixes
  */

namespace A208Login
{
    public partial class StudentLogin : Form
    {
        /*
            This form is intended to be the primary form used by students
            Upon launching the program the administrator login page opens requiring an
            admin or lab assistant to load the program fully
            Upon launching this form is locked into fullscreen to prevent student tampering
            with the computer
            Alt-Tab will be disabled in future versions, Ctrl-Alt-Del and Alt-F4 require interfering
            with System processes to disable and as such are being left to later users to determine
            if the risk is neccessary
        */
        public StudentLogin()
        {   
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            if(FirstTimeLoad())
            {
                using (var myform = new FirstLoadPassword())
                {
                    do
                    {
                        myform.ShowDialog();
                    }
                    while (myform.DialogResult != DialogResult.OK);
                }
            }
            using (var myform = new AdminLogin()) //calls the admin login page on program startup
            {
                do
                {
                    myform.ShowDialog();
                }
                while (myform.DialogResult != DialogResult.OK);
            }
            OpenFill(); //call the fill method for the student login list box
            ResetDir(); //resets the current directory to the base directory
        }

        private List<string> currentLogins = new List<string>();    //maintains a list of currently logged in students
                                                                    //would be nice if this wasn't global

        
        


        /*
         * This function returns the listbox to a full sorted list of all students in the database
         * Much like the other options that take advantage of Valid class SQL queries the 
         * performance of this will depend on the student database being maintained as students
         * graduate
         */
        private void OpenFill()
        {
            nameListBox.Items.Clear();
            Valid openFill = new Valid();
            List<string> studentNames = new List<string>(openFill.StudentSearch("%"));
            studentNames.Sort();
            if (studentNames.Count > 0)
            {
                for (int i = 0; i < studentNames.Count; i++)
                {
                    nameListBox.Items.Add(studentNames[i]);
                }
            }
        }




        /*
         * The following region is used to update the student name listbox for faster logins
         * It queries the database on text change to match users
         * The size of the DB could cause this function to run slowly if old users aren't pruned
         * 
         * The first section is designed to escape apostrophes in user names to not interfere with
         * SQL queries
         * The try block uses a function of the Valid class to retrieve all student entries that
         * partial match the last name entered
         */
        #region ListBoxUpdater
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            string name = textBox1.Text;
            if(name.Length == 0)
            {
                name = "%";
            }
            if (name.Length > 1)
            {
                if (name.Contains("'"))
                {
                    for(int i = 0; i < name.Length; i++)
                    {
                        if(name[i] == '\'')
                        {
                            name = name.Insert(i, "\'");
                            i++;
                        }
                    }
                }
            }
            else if(name.Length == 1 && name == "\'")
            {
                name += "'";
            }            
            try
            {
                nameListBox.Items.Clear();
                Valid students = new Valid();
                List<string> studentNames = new List<string>(students.StudentSearch(name));
                studentNames.Sort();
                if (studentNames.Count > 0)
                {
                    for (int i = 0; i < studentNames.Count; i++)
                    {
                        nameListBox.Items.Add(studentNames[i]);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Something is broken in the list fill.", ex.Message);                
            }
            //nameListBox.BeginUpdate
        }
        #endregion




        /*
         * This function was designed to maintain a base directory in the event of save dialogs changing the current directory
         * This prevents log files from being dumped into the wrong directory
         */
        private void ResetDir()  //Resets the current directory to help control where log files are created
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        
        
        /*
         * The primary function of this form, this button performs validation checking on user input
         * and queries the student DB for a password to match
         * It creates tokens for the name to pass into Valid class search functions and calls both
         * the Valid.Auth function for this purpose and also the Append function built into this form to
         * add an entry to the log file matching the current date
         * Upon login this also plays a system sound to notify lab assistant of a valid login and
         * reiterates the lab policies on display in the lab
         */
        private void button1_Click(object sender, EventArgs e)  //Validates student input and logs them in
        {
            try
            {
                if((nameListBox.SelectedIndex != -1) && (passBox.Text.Length == 7))  //collects a name from the list box
                {                                                                    //and password from textbox
                    if (labRadio.Checked == true || tutorRadio.Checked == true || workRadio.Checked == true)
                    {
                        string name = nameListBox.SelectedItem.ToString();
                        string[] fullname = name.Split(',');                    //tokenizes name for searching the DB
                        string lastname = fullname[0].Trim();
                        string firstname = fullname[1].Trim();
                        Valid student = new Valid(firstname, lastname, passBox.Text);
                        if(student.Auth(firstname, lastname, passBox.Text))    //executes DB query to match passwords
                        {

                            //create/append a log file with name, ID, major and timestamp
                            //add to a list for the logout page
                            if (AppendLog(student, "Time in: "))
                            {
                                currentLogins.Add(name);
                                System.Media.SystemSounds.Asterisk.Play();      //system sound for lab assistants                                
                                Form policy = new LabPolicy();
                                policy.ShowDialog();
                                MessageBox.Show("Login successful.");
                                /*
                                MessageBox.Show("\t    LAB POLICIES: \n " +         //displays lab policies on login
                                    "No food or drink allowed in the lab. \n " +
                                    "No children allowed in the lab. \n " +
                                    "No vaping allowed in the lab. \n" +
                                    "10 page print limit per student per day.");*/
                                passBox.Clear();
                                majorBox.Clear();
                                textBox1.Focus();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Incorrect password.");
                            passBox.Clear();
                            passBox.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please choose why you are logging in today.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select your name and enter your seven digit Student ID.");
                }
                
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error in Login button", ex.Message);
            }
        }
        
        
        
        /*
         * This module creates a date time and an entry string for logging purposes, then adds
         * an entry to a log file based on the current date and creates that file if it does not already exist
         * 
         */
        private bool AppendLog(Valid student, string inout)  //Will append a log file of the current date with student name
        {
            try
            {
                string major, reason;
                if (majorBox.Text.Length > 0)
                {
                    major = majorBox.Text;
                    if (major.Contains(","))
                    {
                        for (int i = 0; i < major.Length; i++)
                        {
                            if (major[i] == ',')
                            {
                                major = major.Remove(i, 1);
                                i--;
                            }
                        }
                    }
                }
                else
                {
                    major = "unspecified";      //major is an optional entry this is provided for
                }                               //spacing reasons in the log viewer
                if (labRadio.Checked)
                {
                    reason = "Outside lab";
                }
                else if (tutorRadio.Checked)
                {
                    reason = "Tutoring";
                }
                else
                {
                    reason = "Class/Homework";
                }
                labRadio.Checked = false;
                tutorRadio.Checked = false;
                workRadio.Checked = false;
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
                using (StreamWriter outputfile = new StreamWriter("Logs\\" + logpath, true))
                {
                    string encryptText = Utility.Encrypt(student.First + "," + student.Last + "," + student.Pass + "," + date + "," + inout + ',' + time + "," + major + "," + reason, false);
                    outputfile.WriteLine(encryptText);
                    outputfile.Close();
                    return true;
                }
            }
            catch
            {
                MessageBox.Show("Error writing to log.");
            }
            return false;
        }
        
        
        
        /*
         * The following region is composed of entries for the menu buttons to open other forms
         * Generally, all of these menu strip options first open the AdminLogin form to validate
         * the user
         * Some of the options specifically require a higher admin level than the Lab Assistant
         * these options are denoted by a decision statement that checks the authok value returned 
         * by the AdminLogin form, this value denotes the admin type each login is assigned in the Admin DB
         * 
         */

        #region MenuStripItems
        private void AddStudent_Click(object sender, EventArgs e)  //Opens the add student form
        {
            using (Form myform = new AdminLogin())  //Opens the admin login form to control adding to DB
            {
                var result = myform.ShowDialog();
                if(result == DialogResult.OK)   //This generic dialog check looks for a successful
                {                               //admin login of any type
                    Form userform = new CreateUser();
                    userform.ShowDialog();
                    OpenFill();                 //repopulates the list after a student is added
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)  //Exits the program with Admin approval
        {            
            using (Form myform = new AdminLogin())
            {                
                var result = myform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    clearLoginsToolStripMenuItem.PerformClick();
                    this.Close();
                }
            }
        }

        private void AdminTasks_Click(object sender, EventArgs e)  //Opens Administrator form
        {
            
            using (var myform = new AdminLogin())
            {
                var result = myform.ShowDialog();
                //The following check uses the above mentioned method for determining if an admin
                //or a lab assistant is attempting to open the admin form as only admins should
                //be allowed access
                //The form is declared as a form instead of a var because we expect no return values
                if (result == DialogResult.OK && (myform.authok == "Admin" || myform.authok == "Administrator"))
                {
                    Form adminform = new AdminTasks();
                    adminform.ShowDialog();
                    OpenFill();     //repopulates the student list as admins can remove students
                }
                else
                {
                    MessageBox.Show("Incorrect username/password.");
                }
            }
        }

        private void Logout_Click(object sender, EventArgs e)   //Opens the student logout form for the lab assistants
        {                                                       //to do single user logouts
            if (currentLogins.Count > 0)
            {
                using(var adminform = new AdminLogin())
                {
                    adminform.ShowDialog();
                    if (adminform.DialogResult == DialogResult.OK)
                    {
                        using (var myform = new Logout(currentLogins))  //passes the current login list to the logout form
                        {
                            var result = myform.ShowDialog();   //Declared as var because we expect an update when this closes
                            if (result == DialogResult.OK)
                            {
                                currentLogins = myform.currentStudents; //receives an updated list from the logout form
                            }
                            OpenFill(); //repopulates the student login list
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No students currently logged in.");
            }
        }   

        private void clearLoginsToolStripMenuItem_Click(object sender, EventArgs e)  //allows labassist/admins to log everyone out at end of day
        {
            if (currentLogins.Count != 0)
            {
                using (Form myform = new AdminLogin())
                {
                    myform.ShowDialog();
                    if (myform.DialogResult == DialogResult.OK)
                    {

                        for (int i = 0; i < currentLogins.Count; i++)
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
                            using (StreamWriter outputfile = new StreamWriter("Logs\\" + logpath, true))
                            {
                                string[] name = currentLogins[i].Split(',');
                                outputfile.WriteLine(Utility.Encrypt(name[1].Trim() + "," + name[0].Trim() + ",," + date + ",Force out: ," + time, false));
                                outputfile.Close();
                            }
                        }
                        currentLogins.Clear();
                        MessageBox.Show("All users logged out.");
                        OpenFill();
                    }
                }
            }
        }
        #endregion
        

        /*
         * This button to open the student logout form has been removed in favor or the function below it
         * which will allow students to log in and out from the same form.  The student logout form still
         * exists but is reserved for admin use.  Kept here for posterity.
         * 
        private void logoutButton_Click(object sender, EventArgs e)  //Alternative logout button to open the logout form
        {                                                           //Created as a more user friendly alternative to menu item
            if (currentLogins.Count > 0)
            {
                using (var myform = new Logout(currentLogins))  //passes the current login list to the logout form
                {
                    var result = myform.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        currentLogins = myform.currentStudents; //receives an updated list from the logout form
                    }
                    OpenFill(); //repopulates the student login list
                }
            }
            else
            {
                MessageBox.Show("No students currently logged in.");
            }
        }*/
        
        
        
        /*
         * The following button event handler is presented as an alternative to opening the student logout form using either
         * of the previous methods
         * Instead of calling the logout form this method reuses the existing code from the logout form to handle appending
         * the daily log and updating the currentlogin list
         * This method requires users to find their name in the list, requiring them to search or filter through the list in
         * the same way as when they log in, the example set by the previous login program required the use of a seperate form
         * to make searching the list for logouts faster, but the addition of a search box may obsolete the need for this form
         * This option is then presented as an alternative
         */
        private void button2_Click(object sender, EventArgs e)
        {
            if (currentLogins.Count > 0)
            {
                if (nameListBox.SelectedIndex != -1 && passBox.Text.Length == 7)
                {
                    string name = nameListBox.SelectedItem.ToString();
                    string[] fullname = name.Split(',');                    //tokenizes name for searching the DB
                    string lastname = fullname[0].Trim();
                    string firstname = fullname[1].Trim();
                    Valid student = new Valid(firstname, lastname, passBox.Text);
                    if (currentLogins.Contains(name))      //ensures the student logging out is logged in
                    {
                        if (student.Auth(firstname, lastname, passBox.Text))    //executes DB query to match passwords
                        {
                            //create/append a log file with name, ID, major and timestamp
                            //add to a list for the logout page
                            if (AppendLog(student, "Time out: "))
                            {
                                MessageBox.Show("Logout successful.");
                                currentLogins.Remove(name);
                                passBox.Clear();
                                majorBox.Clear();
                                OpenFill();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("You are not currently logged in.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select your name and enter your student ID.");
                }
            }
            else
            {
                MessageBox.Show("No students currently logged in.");
            }
        }

        //Function to create an Administrator password if none exists
        private bool FirstTimeLoad()
        {
            Valid admin = new Valid();
            if(admin.Auth("Administrator", " "))
            {
                return true;
            }
            return false;
        }

        /*
         * The following region disables access to escape key combinations such as Alt-Tab, Alt-Esc,
         * etc., excepting Alt-Ctrl-Del as keyboard hooks cannot disable that functionality
         * 
         * Originally sourced from https://social.msdn.microsoft.com/Forums/vstudio/en-US/d15600ee-150c-4a4f-b50f-71187de3bf94/disable-alt-tab-in-c-program
         * 
         * Adapted for use here by John Dennis
         */

        #region "Disable Special Keys"

        private delegate int LowLevelKeyboardProcDelegate(int nCode, int
            wParam, ref KBDLLHOOKSTRUCT lParam);

        // Pin the delegate in memory by storing it in a private field
        private static readonly LowLevelKeyboardProcDelegate _disableKeysHook = new LowLevelKeyboardProcDelegate(LowLevelKeyboardProc);
        
        [DllImport("user32.dll", EntryPoint = "SetWindowsHookExA", CharSet = CharSet.Ansi)]
        private static extern int SetWindowsHookEx(
           int idHook,
           LowLevelKeyboardProcDelegate lpfn,
           int hMod,
           int dwThreadId);

        [DllImport("user32.dll")]
        private static extern int UnhookWindowsHookEx(int hHook);


        [DllImport("user32.dll", EntryPoint = "CallNextHookEx", CharSet = CharSet.Ansi)]
        private static extern int CallNextHookEx(
            int hHook, int nCode,
            int wParam, ref KBDLLHOOKSTRUCT lParam);


        const int WH_KEYBOARD_LL = 13;
        private static int intLLKey;
        private static KBDLLHOOKSTRUCT lParam;


        private struct KBDLLHOOKSTRUCT
        {
            public int vkCode;
            int scanCode;
            public int flags;
            int time;
            int dwExtraInfo;
        }

        private static int LowLevelKeyboardProc(
            int nCode, int wParam,
            ref KBDLLHOOKSTRUCT lParam)
        {
            bool blnEat = false;
            switch (wParam)
            {
                case 256:
                case 257:
                case 260:
                case 261:
                    //Alt+Tab, Alt+Esc, Ctrl+Esc, Windows Key
                    if (((lParam.vkCode == 9) && (lParam.flags == 32)) ||
                    ((lParam.vkCode == 27) && (lParam.flags == 32)) || ((lParam.vkCode ==
                    27) && (lParam.flags == 0)) || ((lParam.vkCode == 91) && (lParam.flags
                    == 1)) || ((lParam.vkCode == 92) && (lParam.flags == 1)) || ((true) &&
                    (lParam.flags == 32)))
                    {
                        blnEat = true;
                    }
                    break;
            }

            if (blnEat)
                return 1;
            else return CallNextHookEx(0, nCode, wParam, ref lParam);

        }

        private void KeyboardHook(object sender, EventArgs e)
        {
            intLLKey =
                SetWindowsHookEx(

                WH_KEYBOARD_LL,

                //new LowLevelKeyboardProcDelegate(LowLevelKeyboardProc),

                _disableKeysHook,   // Pass in the pinned delegate so it never gets moved

                System.Runtime.InteropServices.Marshal.GetHINSTANCE(
                  System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0]).ToInt32(), 0);
        }

        private void ReleaseKeyboardHook()
        {
            intLLKey = UnhookWindowsHookEx(intLLKey);
        }

        private void StudentLogin_Load(object sender, EventArgs e)
        {
            KeyboardHook(this, e);
        }
        #endregion
    }
}
