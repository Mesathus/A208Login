﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Forms;

namespace A208Login
{
    public partial class AdminTasks : Form
    {
        /*
         * This is the base form for Admin related tasks, basically anything that involves deleting from
         * the database, or viewing/modifying passwords.  Loading this from the StudentLogin form requires
         * the Admin/Administrator login, the LabAssist login type does not have a sufficient access level
         * to open it.
         * 
         * On load this form resets the base directory in case a save/load dialog has changed it and sets
         * sets the path for the log directory.  It then populates the list box with the names of all
         * .txt files it finds in that directory.
         * 
         * This form also contains menu options to redirect to addition Admin task forms, including the
         * RemoveStudent form and the CreateUpdateAdmins form.
         * 
         * Current additions being considered are a search feature for the loglistbox so exporting data
         * isn't necessary for quick checks, the existence of the data export and power of spreadsheets
         * has pushed this to a low priority however.
         */
        public AdminTasks()
        {
            InitializeComponent();
            DirectoryInfo logpath = ResetDir();
            PopLogs(logpath);
        }

        private bool backedUp = false;
        /*
         * This is the function that resets the directory to the base application directory and appends it
         * with \Logs to create a path to the log files.  This path may be modified, but then also needs to
         * be updated in StudentLogin and Logout forms so that logs files are written to the correct
         * directory as well.
         */
        private DirectoryInfo ResetDir()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            DirectoryInfo logDir = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "Logs"));
            return logDir;
        }
        
        /*
         * This module receives the path created above and populates the log listbox with all .txt
         * files it finds in that directory
         */
        private void PopLogs(DirectoryInfo logpath)
        {
            logNameList.Items.Clear();
            FileInfo[] Files = logpath.GetFiles("*.txt");
            foreach(FileInfo file in Files)
            {
                logNameList.Items.Add(file.Name);
            }
        }

        private void openLogToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }
        /*
         * This function displays a selected log file on the form for Admins to scroll through.
         * It opens the log file and decrypts each line into the listbox, replacing the commas with
         * tab stops to readability
         */
        private void openLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                logListBox.Items.Clear();
                if (logNameList.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Select a log to display.");
                }
                else
                {
                    string logname = logNameList.SelectedItem.ToString();
                    using (StreamReader inputfile = new StreamReader("Logs\\" + logname))
                    {
                        while (!inputfile.EndOfStream)
                        {
                            string logline = Utility.Decrypt(inputfile.ReadLine(), false);
                            logline = logline.Replace(',', '\t');
                            logListBox.Items.Add(logline);
                        }
                        logname = null;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error opening log file.");
            }
        }
        /*
         * This function is for the exporting of the logfiles into plain text.  It allows for exporting in
         * either .txt formats or .csv formats for use in spreadsheets.  This function utilizes the 
         * SaveFileDialog, creating the need to reset the log directory in the other forms.
         * 
         * This function is specific to writing the open/displayed log file as it is predicated on 
         * previewing the data before deciding on writing to plain text.  This can be changed based on need.
         */
        private void exportCurrentLogToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();

                saveFile.Filter = "txt files (*.txt)|*.txt|csv files (*.csv)|*.csv";
                saveFile.FilterIndex = 2;
                saveFile.RestoreDirectory = true;   //Should reset the directory when saving is finished.
                                                    //Maintaining the directory in other forms is still 
                                                    //performed for reliability.
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    using(StreamWriter outputfile = new StreamWriter(saveFile.FileName))
                    {
                        //first line creates the header line for later importing the data
                        outputfile.WriteLine("First Name,Last Name,Student ID,Date,Login/out,Timestamp,Major given,Login Reason");
                        //cycle through the displayed log and write each line as plain text
                        for (int i = 0; i < logListBox.Items.Count; i++)
                        {
                            string logline = logListBox.Items[i].ToString();
                            logline = logline.Replace('\t',',');    //character substitution to revert back to comma separated
                            outputfile.WriteLine(logline);          //format when writing to plain text
                        }
                    }                    
                }
                saveFile = null;    //removes the save dialog from memory
            }
            catch
            {
                MessageBox.Show("Error exporting plain text log file.");
            }
        }


        /*
         * The following functions are to open the remaining admin forms, the RemoveStudent form to purge
         * students who are no longer enrolled and the Update/CreateAdmin form to add/remove LabAssist and
         * Admin accounts from the Admin database and update passwords for any of the Admin accounts
         * 
         * These menu items do not call the AdminLogin form as an Admin is expected to have logged in before 
         * the base for is accessed.  They also contain their own authorization when their functions are
         * called.
         */

        private void removeStudentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form myform = new RemoveStudent())
            {
                myform.ShowDialog();
            }
            PopLogs(ResetDir());
        }

        private void updateCreateAdminsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form myform = new CreateUpdateAdmin())
            {
                myform.ShowDialog();
            }
            PopLogs(ResetDir());
        }

        
        private void createDatabaseDumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void importDatabaseObjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        /*
         * The following function is called from the Full Database Dump menu item, it retrieves the
         * entire student DB and creates a CSV file with it.  This module was created to aid in 
         * determining which students need to be purged from the database.
         */
        private void createDatabaseDumpToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();

                saveFile.Filter = "txt files (*.txt)|*.txt|csv files (*.csv)|*.csv|xml files (*.xml)|*.xml";
                saveFile.FilterIndex = 2;
                saveFile.RestoreDirectory = true;   //Should reset the directory when saving is finished.
                                                    //Maintaining the directory in other forms is still 
                                                    //performed for reliability.
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter outputfile = new StreamWriter(saveFile.FileName))
                    {
                        //first line creates the header line for later importing the data
                        outputfile.WriteLine("Last Name, First Name, Student ID");

                        Valid allStudents = new Valid();
                        List<string> fullDB = new List<string>(allStudents.StudentSearch("%", true));

                        //cycle through the retrieved list and write each line as plain text
                        for (int i = 0; i < fullDB.Count; i++)
                        {
                            string[] logLine = fullDB[i].Split(',');
                            outputfile.WriteLine(logLine[0] + "," + logLine[1] + "," + Utility.Decrypt(logLine[2], false));          //writing each list item to file
                        }
                        allStudents = null;
                        fullDB = null;
                        backedUp = true;
                    }
                }
                saveFile = null;    //removes the save dialog from memory
            }
            catch
            {
                MessageBox.Show("Error exporting plain text log file.");
                backedUp = false;
            }
        }

        /*
         *      Method added to allow the mass importing of database objects.  This function has specific formatting requirements
         *      for its data files, requiring them to be in Last Name, First Name, StudentID order for correct entry into the database.
         *      A note appears in a message box when clicked, but is still contingent on correct user input.
         */
        private void importDatabaseObjectsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //message to user to inform of input requirements
            MessageBox.Show("When importing students, the file must be in a comma separated format." + Environment.NewLine +
                "Students must be listed LastName,FirstName,StudentID." + Environment.NewLine +
                "Student IDs of fewer than 7 characters will have 0s appended to the beginning to meet length requirements.");
            try
            {
                using (OpenFileDialog readFile = new OpenFileDialog())
                {
                    readFile.Filter = "txt files (*.txt)|*.txt|csv files (*.csv)|*.csv|xml files (*.xml)|*.xml";
                    readFile.FilterIndex = 2;
                    readFile.RestoreDirectory = true;   //restore directory to default after files are selected
                    int counter = 0;    //counter to track how many students are successfully added
                    if (readFile.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamReader inputFile = new StreamReader(readFile.FileName))
                        {
                            while (!inputFile.EndOfStream)  //loop the entirety of the file
                            {
                                string[] inputData = inputFile.ReadLine().Split(',');   //split each line, hence the comma requirement
                                if (inputData[2].Length <= 7 && int.TryParse(inputData[2], out int pass))   //check to see if the user ID meets DB requirements
                                {   
                                    while (inputData[2].Length < 7) //the DB requires user IDs to be 7 digits, as Excel trims beginning 0s we must add them
                                    {
                                        inputData[2] = "0" + inputData[2];  //append 0s to the ID until it is 7 digits long
                                    }
                                    Valid student = new Valid(inputData[1].Trim(), inputData[0].Trim(), inputData[2]);    //create the student, encryption is handled in the Valid class functions
                                    if (!student.DupeCheck(student.Pass))   //check to see if the user is already in the DB
                                    {
                                        student.InsertStudent(student);     //insert new students
                                        counter++;  //increment counter to track number of students added
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    //Message box is displayed if the third value on each comma separated line is not a number
                                    MessageBox.Show("An error has occured.  Failed to find a number for Student ID" +
                                        " or Student ID was greater than seven digits long.");
                                }
                            }
                            MessageBox.Show(counter.ToString() + " students imported successfully.");   //displays the student counter to compare against
                                                                                                        //expected number of students added
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("An error has occured while attempting to import students." + Environment.NewLine +
                    "Import process has been terminated.");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)    //Exit menu item added for convenience so the user doesn't have to use ControlBox items
        {
            this.Close();
        }

        /*
         *  Method created to backup and scrub the database.  This is intended to be used each semester to allow administrators to import a current
         *  list of enrolled students.  
         */
        private void backupAndDeleteDatabaseEntriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Warning, this will remove all entries from the student database.");
            try
            {
                createDatabaseDumpToolStripMenuItem.PerformClick(); //call the full DB dump function
                if (backedUp == true)   //check against form property set by the DB dump function
                {
                    Valid student = new Valid("filler", "filler", "%"); //creates a student with a wildcard password for a full delete
                    if (student.RemoveStudent(student))
                    {
                        MessageBox.Show("Database has been backed up and scrubbed.");
                    }
                }                
            }
            catch
            {
                MessageBox.Show("An error has occured while performing the delete process.");
            }
        }
    }
}
