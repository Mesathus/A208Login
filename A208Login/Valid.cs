using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace A208Login
{
    class Valid
    {

        /*
         * This class provides for the creation of a validation object containing the student first and last
         * name, and a password that can then be compared to the existing student database
         * 
         * Currently, this class is also dependant on the Utility class to handle the encryption and decryption
         * of passwords.
         * 
         * The following has been partially addressed.  Duplicate names are now allowed, but duplicate Student IDs
         * are not.  A side effect of how this database is constructed allows users with matching names to sign in
         * with each others Student IDs as the ID is both the primary key and the password.  This is bad practice
         * but simplifies the login process for users and should be acceptable since no personal information can
         * be retrieved by users in this manner.
         * 
         * A suggestion has been given to correct this which would require an additional field to serve as primary
         * key.  Students may be asked to input their YorkTech ID, for instance this users jobrienXXXX ID where
         * XXXX is the last 4 digits of the Student ID.  This would change the login method for students and
         * require a fair bit of rebuilding, but would be useful for authentication reasons.
         * 
         * The following note about the previous version is left in for posterity.
         *  
         * --------------------------------------------------
         * One significant "bug" still present in the current version of this validation class is the C#
         * database propensity for ignoring case.  This made the inclusion of LOWER(@param) in SQL queries 
         * unneccessary as well as removal of the string.ToLower() methods in the main program possible
         * Unfortunately this leads to the DB queries being unable to currently handle multiple entries with
         * the same name, requiring a secondary duplicate checking method found below
         * This behavior prevents students with the same first and last name from being entered into the database
         * even if they have seperate StudentIDs as the SQL reader check only returns password at index 0 even if it
         * contains multiple lines 
         * --------------------------------------------------
         */


            /*
             * The following are the Name and ID declarations for this class
             */

        #region Class declarations
        private string _fname;
        private string _lname;
        private string _password;

        public Valid(string fname, string lname, string password)
        {
            _fname = fname;
            _lname = lname;
            _password = password;
        }
        public Valid()
        {

        }

        public string First
        {
            get { return _fname; }
        }
        public string Last
        {
            get { return _lname; }
        }
        public string Pass
        {
            get { return _password; }
        }
        #endregion


        /*
         * The following region contains the methods used to retrieve passwords from the database.  It also
         * contains the methods for checking duplicate entries, though the name checking method is no longer
         * used with the change to the password retrievel method.  The penultimate method retrieves
         * the admin type for checking to see if a LabAssistant or an Admin used the AdminLogin form.  The
         * final method is a private method that actually executes the SQL commands created by several of
         * these authentication methods.
         */
        #region PasswordRetrieval

        /*
         * The Auth method overloads are used to determine if the login is for a student or an administrator
         * Student logins must provide two name strings, one for first name and one for last name.
         * Admin logins only provide the name of the Administrator login
         * This determines which SQL connection and statement is sent to the retrieve password module
         */
        public bool Auth(string name, string pass)  //authentication for administrators
        {            
            using (SqlConnection connectionstring = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\Admins.mdf;Integrated Security = True"))
            { 
                string querypassword = "SELECT Password FROM dbo.Admin WHERE Admin.Name = @name;";
                SqlCommand cmd = new SqlCommand();
                SqlParameter nameParam = new SqlParameter("@name", SqlDbType.VarChar, 30);
                nameParam.Value = name;
                cmd.Parameters.Add(nameParam);
                cmd.Connection = connectionstring;
                cmd.CommandText = querypassword;
                if (RetrievePass(cmd).Contains(pass)) //This checks to see if the returned string contains
                {                                     //the password provided.  This methodology is less
                    return true;                      //important for admin logins as names are unique in this table
                }
            }
            return false;
        }

        public bool Auth(string fname, string lname, string pass)  //authentication for students
        {
            if(pass.Trim().Length == 0)
            {
                return false;
            }
            using (SqlConnection connectionstring = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Students.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                string querypassword = "SELECT StudentID FROM dbo.Student WHERE (Student.FirstName = @fname) AND (Student.Lastname = @lname);";
                SqlCommand cmd = new SqlCommand();
                SqlParameter firstParam = new SqlParameter("@fname", SqlDbType.VarChar, 30);
                SqlParameter lastParam = new SqlParameter("@lname", SqlDbType.VarChar, 30);
                firstParam.Value = fname;
                lastParam.Value = lname;
                cmd.Connection = connectionstring;
                cmd.CommandText = querypassword;
                cmd.Parameters.Add(firstParam);
                cmd.Parameters.Add(lastParam);
                if (RetrievePass(cmd).Contains(pass))   //As in the previous method, this checks the returned password
                {                                       //string contains the password provided.  This is the workaround
                    return true;                        //for allowing duplicate names in the current DB design
                }
            }
            return false;            
        }

        public bool DupeCheck(string pass)  //checks database for duplicate student IDs
        {
            SqlConnection connectionstring = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Students.mdf;Integrated Security=True;Connect Timeout=30");
            string querypassword = "SELECT LastName FROM dbo.Student WHERE Student.StudentID = @password;";
            SqlCommand cmd = new SqlCommand();
            SqlParameter passParam = new SqlParameter("@password", SqlDbType.NVarChar, 50);
            cmd.Connection = connectionstring;
            cmd.CommandText = querypassword;
            passParam.Value = Utility.Encrypt(pass, false);
            cmd.Parameters.Add(passParam);
            connectionstring.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)     //This checks to see if values were returned by the reader
            {                       //The actual values do not need to be checked, as any value returned
                reader.Close();     //indicates an entry with that Student ID already exists
                connectionstring.Close();
                return true;
            }
            connectionstring.Close();
            return false;            
        }
        /*
         * The following function has become obsolete with the change to password retrieval that allows duplicate
         * names to exist in the student DB.  It is left here for posterity and should the previous methodology
         * be reintroduced.
         */


         /*
        public bool DupeCheck(string firstname, string lastname)  //checks database for duplicate student IDs
        {
            SqlConnection connectionstring = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Students.mdf;Integrated Security=True;Connect Timeout=30");
            string querypassword = "SELECT FirstName, LastName FROM dbo.Student WHERE Student.FirstName = @first AND Student.LastName = @last;";
            SqlCommand cmd = new SqlCommand();
            SqlParameter passParam = new SqlParameter("@password", SqlDbType.NVarChar, 50);
            cmd.Connection = connectionstring;
            cmd.CommandText = querypassword;
            cmd.Parameters.AddWithValue("@first", firstname);
            cmd.Parameters.AddWithValue("@last", lastname);
            connectionstring.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                reader.Close();
                connectionstring.Close();
                return true;
            }
            connectionstring.Close();
            return false;
        }*/


        /*
         * This function is used by the admin login form to differentiate between the Lab Assist logins
         * and the Admin logins when trying to access forms that are forbidden to Lab Assistants
         */
        public string Type(string name, string pass)  //authenticates password and returns admin/labassist type
        {
            SqlConnection connectionstring = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\Admins.mdf;Integrated Security = True");
            string querypassword = "SELECT Password FROM dbo.Admin WHERE Admin.Name = @name;";
            SqlCommand cmd = new SqlCommand();
            SqlParameter nameParam = new SqlParameter("@name", SqlDbType.VarChar, 30);
            nameParam.Value = name;
            cmd.Parameters.Add(nameParam);
            cmd.Connection = connectionstring;
            cmd.CommandText = querypassword;
            if (RetrievePass(cmd).Contains(pass))
            {
                querypassword = "SELECT Type FROM dbo.Admin WHERE Admin.Name = @name;";
                cmd.CommandText = querypassword;
                using (cmd.Connection)
                {
                    cmd.Connection.Open();
                    cmd.Prepare();
                    SqlDataReader reader = cmd.ExecuteReader();
                    string results = "";
                    while(reader.Read())
                    {
                        results = reader.GetValue(0).ToString();
                    }
                    return results;
                }
            }
            return "";
        }
        /*
         * The following is a module created to execute SQL Commands from the various other validation 
         * functions and return a decrypted string which contains the password(s) matching the user name
         */
        private string RetrievePass(SqlCommand cmd)  //password retrieval function
        {
            string results = null;
            try
            { 
                SqlConnection sqlcon = cmd.Connection;
                sqlcon.Open();
                cmd.Prepare();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results = results + Utility.Decrypt(reader.GetValue(0).ToString(), false) + " ";                    
                }
                sqlcon.Close();
                reader.Close();
            }
            catch(Exception)
            {
                return " "; /*this return is intended to be used solely for use with the FirstLoadPassword
                             *form, and the Form.StudentLogin.FirstTimeLoad function.  If this return value
                             *occurs anywhere else notify creator so security updates can be made.
                             *This return value should only occur if a null password is retrieved from the
                             *database*/
            }
            results = results.Trim();
            return results;
        }
        #endregion

        /*
         * The following region contains the methods that alter the table, including insertion and removal
         * methods for students and admins, and the update method to change Admin passwords.
         * They all function similarly, creating parameterized SQL commands to send to the respective database
         */

        #region TableManipulation

        public bool InsertStudent(Valid student)
        {            
            try
            {
                string connectionstring = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Students.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection sqlcon = new SqlConnection(connectionstring))
                {
                    SqlParameter fnameParam = new SqlParameter("@fname", SqlDbType.NVarChar, 50);
                    SqlParameter lnameParam = new SqlParameter("@lname", SqlDbType.NVarChar, 50);
                    SqlParameter passParam = new SqlParameter("@password", SqlDbType.NVarChar, 50);
                    SqlCommand cmd = new SqlCommand();
                    string insertname = "INSERT INTO dbo.Student (StudentID, FirstName, LastName) VALUES (@password, @fname, @lname);";                
                    sqlcon.Open();
                    fnameParam.Value = student.First;
                    lnameParam.Value = student.Last;
                    passParam.Value = Utility.Encrypt(student.Pass, false);
                    cmd.CommandText = insertname;
                    cmd.Connection = sqlcon;
                    cmd.Parameters.Add(passParam);
                    cmd.Parameters.Add(lnameParam);
                    cmd.Parameters.Add(fnameParam);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    connectionstring = null;                    
                }
                return true;
            }
            catch
            {
                return false;
            }            
        }

        //9/4/18 change, SQL statement from = to LIKE to support database scrubbing method in Forms.AdminTasks
        public bool RemoveStudent(Valid student)
        {
            try
            {                
                string connectionstring = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Students.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection sqlcon = new SqlConnection(connectionstring))
                {
                    SqlParameter passParam = new SqlParameter("@password", SqlDbType.NVarChar, 50);                    
                    string removename = "DELETE FROM dbo.Student WHERE Student.StudentID LIKE @password;";
                    passParam.Value = student.Pass;
                    SqlCommand cmd = new SqlCommand(removename, sqlcon);
                    cmd.Parameters.Add(passParam);
                    sqlcon.Open();
                    cmd.Prepare();                    
                    cmd.ExecuteNonQuery();
                    connectionstring = null;
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool InsertAdmin(string name, string password, string type)
        {
            try
            {
                string connectionstring = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Admins.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection sqlcon = new SqlConnection(connectionstring))
                {
                    SqlParameter passParam = new SqlParameter("@password", SqlDbType.NVarChar, 100);
                    SqlParameter nameParam = new SqlParameter("@name", SqlDbType.NVarChar, 30);
                    SqlParameter typeParam = new SqlParameter("@type", SqlDbType.NVarChar, 10);
                    string insertAdmin = "INSERT INTO dbo.Admin (Type, Name, Password) VALUES (@type, @name, @password);";
                    SqlCommand cmd = new SqlCommand(insertAdmin, sqlcon);
                    passParam.Value = Utility.Encrypt(password, false);
                    nameParam.Value = name;
                    typeParam.Value = type;
                    cmd.Parameters.Add(passParam);
                    cmd.Parameters.Add(nameParam);
                    cmd.Parameters.Add(typeParam);
                    sqlcon.Open();
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    connectionstring = null;
                }
                return true;
            }
            catch
            {

            }
            return false;
        }

        public bool UpdateAdmin(string name, string password)
        {
            try
            {
                string connectionstring = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Admins.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection sqlcon = new SqlConnection(connectionstring))
                {
                    string updateadmin = "UPDATE dbo.Admin SET Admin.Password = @password Where Admin.Name = @name;";
                    SqlParameter passParam = new SqlParameter("@password", SqlDbType.VarChar, 100);
                    SqlParameter nameParam = new SqlParameter("@name", SqlDbType.VarChar, 30);
                    SqlCommand cmd = new SqlCommand(updateadmin, sqlcon);
                    passParam.Value = Utility.Encrypt(password, false);
                    nameParam.Value = name;
                    cmd.Parameters.Add(passParam);
                    cmd.Parameters.Add(nameParam);
                    sqlcon.Open();
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    connectionstring = null;                    
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool RemoveAdmin(string name)
        {
            try
            {
                string connectionstring = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Admins.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection sqlcon = new SqlConnection(connectionstring))
                {
                    string removeAdmin = "DELETE FROM dbo.Admin WHERE Admin.Name = @name;";
                    SqlParameter nameParam = new SqlParameter("@name", SqlDbType.NVarChar, 30);
                    nameParam.Value = name;
                    SqlCommand cmd = new SqlCommand(removeAdmin, sqlcon);
                    cmd.Parameters.Add(nameParam);
                    sqlcon.Open();
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    connectionstring = null;
                    return true;
                }                
            }
            catch
            {
                return false;
            }
        }
        #endregion

        /*
         * The following region contains list<string> returning methods used by the various forms to
         * populate student and admin listboxes for easy name selection
         */

        #region ListFillMethods

        /*
         * The first method is used by the student login form to populate and filter the list as the 
         * name search box is updated
         */
        public List<string> StudentSearch(string lname)  //used to filter names and fill student list boxes
        {
            try
            {
                string connectionstring = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Students.mdf;Integrated Security=True;Connect Timeout=30";
                List<string> studentNames = new List<string>();
                using (SqlConnection sqlcon = new SqlConnection(connectionstring))
                {
                    SqlParameter lnameParam = new SqlParameter("@lname", SqlDbType.NVarChar, 50);
                    SqlCommand cmd = new SqlCommand();
                    string querylnames = "SELECT Student.FirstName, Student.LastName FROM dbo.Student WHERE (Student.LastName LIKE '%" + @lname + "%');";
                    sqlcon.Open();
                    cmd.Connection = sqlcon;
                    cmd.CommandText = querylnames;
                    lnameParam.Value = lname;
                    cmd.Parameters.Add(lnameParam);
                    cmd.Prepare();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        studentNames.Add(reader.GetValue(1).ToString() + ", " + reader.GetValue(0).ToString());
                    }
                    reader.Close();
                    sqlcon.Close();
                    reader = null;
                    connectionstring = null;
                    return studentNames;
                }
            }
            catch(Exception)
            {
                throw new Exception("Error creating student list");
            }
        }

        /*
         * This function works similarly to the previous one, however it also retrieves the Student ID field
         * The primary function of this list is to populate the remove student list in the Admin form, thus
         * the Student ID is needed to correctly identify students with the same name
         */
        public List<string> StudentSearch(string lname, bool admin)  //used to filter names and fill student list boxes
        {
            try
            {
                if (admin)
                {
                    string connectionstring = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Students.mdf;Integrated Security=True;Connect Timeout=30";
                    using (SqlConnection sqlcon = new SqlConnection(connectionstring))
                    {
                        List<string> studentNames = new List<string>();
                        SqlParameter lnameParam = new SqlParameter("@lname", SqlDbType.Text, 50);
                        SqlCommand cmd = new SqlCommand();
                        string querylnames = "SELECT Student.FirstName, Student.LastName, Student.StudentID FROM dbo.Student WHERE (LOWER(Student.LastName) LIKE '%" + @lname + "%');";
                        sqlcon.Open();
                        cmd.Connection = sqlcon;
                        cmd.CommandText = querylnames;
                        lnameParam.Value = lname.ToLower();
                        cmd.Parameters.Add(lnameParam);
                        cmd.Prepare();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            studentNames.Add(reader.GetValue(1).ToString() + ", " + reader.GetValue(0).ToString() + ", " + reader.GetValue(2).ToString());
                        }
                        reader.Close();
                        sqlcon.Close();
                        reader = null;
                        connectionstring = null;
                        return studentNames;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw new Exception("Error creating student list");
            } 
        }

        /*
         * Silimlar to the above features, this function returns a list<string> for use in populating
         * list boxes with names.  In this case, the list returns either all of the LabAssistant and Admin
         * user names, or all of those in addition to the base Administrator name depending on whether the
         * delete boolean value is set to true or false respectively.  The purpose of this is to provide a
         * single function that can fill both the admin update list box used to change passwords including 
         * the base Administrator account, or the delete list box where the base Administrator account is
         * unavailable to prevent a complete loss of access to authorization in the AdminLogin form
         */

        public List<string> AdminSearch(bool delete)
        {
            try
            {
                List<string> adminNames = new List<string>();
                string querynames;
                string connectionstring = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Admins.mdf;Integrated Security=True;Connect Timeout=30";
                if (delete)
                {
                    querynames = "SELECT Admin.Name FROM dbo.Admin WHERE Admin.Name != 'Administrator';";
                }
                else
                {
                    querynames = "SELECT Admin.Name FROM dbo.Admin;";
                }
                using (SqlConnection sqlcon = new SqlConnection(connectionstring))
                {
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = sqlcon;
                    cmd.CommandText = querynames;
                    cmd.Prepare();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        adminNames.Add(reader.GetValue(0).ToString());
                    }
                    reader.Close();
                    sqlcon.Close();
                    reader = null;
                    connectionstring = null;
                }
                return adminNames;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
