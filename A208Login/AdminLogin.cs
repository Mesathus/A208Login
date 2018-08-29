using System;
using System.Windows.Forms;

namespace A208Login
{
    public partial class AdminLogin : Form
    {
        /*
         * This form is called whenever a login is required from a lab assistant or admin
         * It contains an authok field that holds the type of login LabAssistant/Admin/Administrator
         * so that it can be returned to the calling form in the case that lab assistant is not an 
         * acceptable access level, such as the AdminTasks form
         */
        public AdminLogin()
        {
            InitializeComponent();            
        }
        public string authok { get; set; }  //creates a field we can return to the calling form

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Valid checking = new Valid();
                if (checking.Auth(nameBox.Text, passBox.Text) && passBox.Text.Length > 0)  //Authentication for labassist/admins
                {
                    this.authok = checking.Type(nameBox.Text, passBox.Text);    //Assigns the login type
                    this.DialogResult = DialogResult.OK;                        //to the authok field
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username/password");
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Problem with login form.");
            }            
        }        
    }
}
