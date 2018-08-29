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
    public partial class LabPolicy : Form
    {
        /*
         * This form is designed as a substitute for a message box to allow the use of a button labeled
         * "Accept" for lab policies students must adhere to.
         */
        public LabPolicy()
        {
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
