namespace A208Login
{
    partial class CreateUpdateAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.createNameBox = new System.Windows.Forms.TextBox();
            this.createPassBox = new System.Windows.Forms.TextBox();
            this.createConfirmBox = new System.Windows.Forms.TextBox();
            this.updatePassBox = new System.Windows.Forms.TextBox();
            this.confirmPassBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.createButton = new System.Windows.Forms.Button();
            this.adminRadioButton = new System.Windows.Forms.RadioButton();
            this.labRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.oldPassBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.updateButton = new System.Windows.Forms.Button();
            this.updateListBox = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.removeButton = new System.Windows.Forms.Button();
            this.removeListBox = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // createNameBox
            // 
            this.createNameBox.Location = new System.Drawing.Point(324, 157);
            this.createNameBox.Margin = new System.Windows.Forms.Padding(4);
            this.createNameBox.Name = "createNameBox";
            this.createNameBox.Size = new System.Drawing.Size(148, 44);
            this.createNameBox.TabIndex = 1;
            // 
            // createPassBox
            // 
            this.createPassBox.Location = new System.Drawing.Point(324, 228);
            this.createPassBox.Margin = new System.Windows.Forms.Padding(4);
            this.createPassBox.Name = "createPassBox";
            this.createPassBox.Size = new System.Drawing.Size(148, 44);
            this.createPassBox.TabIndex = 2;
            this.createPassBox.UseSystemPasswordChar = true;
            // 
            // createConfirmBox
            // 
            this.createConfirmBox.Location = new System.Drawing.Point(324, 296);
            this.createConfirmBox.Margin = new System.Windows.Forms.Padding(4);
            this.createConfirmBox.Name = "createConfirmBox";
            this.createConfirmBox.Size = new System.Drawing.Size(148, 44);
            this.createConfirmBox.TabIndex = 3;
            this.createConfirmBox.UseSystemPasswordChar = true;
            // 
            // updatePassBox
            // 
            this.updatePassBox.Location = new System.Drawing.Point(583, 153);
            this.updatePassBox.Margin = new System.Windows.Forms.Padding(4);
            this.updatePassBox.Name = "updatePassBox";
            this.updatePassBox.Size = new System.Drawing.Size(172, 44);
            this.updatePassBox.TabIndex = 7;
            this.updatePassBox.UseSystemPasswordChar = true;
            // 
            // confirmPassBox
            // 
            this.confirmPassBox.Location = new System.Drawing.Point(583, 237);
            this.confirmPassBox.Margin = new System.Windows.Forms.Padding(4);
            this.confirmPassBox.Name = "confirmPassBox";
            this.confirmPassBox.Size = new System.Drawing.Size(172, 44);
            this.confirmPassBox.TabIndex = 8;
            this.confirmPassBox.UseSystemPasswordChar = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.createButton);
            this.groupBox1.Controls.Add(this.adminRadioButton);
            this.groupBox1.Controls.Add(this.labRadioButton);
            this.groupBox1.Controls.Add(this.createNameBox);
            this.groupBox1.Controls.Add(this.createPassBox);
            this.groupBox1.Controls.Add(this.createConfirmBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(40, 87);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(515, 641);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create new user";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 299);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(289, 37);
            this.label3.TabIndex = 0;
            this.label3.Text = "Confirm Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 231);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 37);
            this.label2.TabIndex = 6;
            this.label2.Text = "Enter password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 160);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter user name:";
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(177, 416);
            this.createButton.Margin = new System.Windows.Forms.Padding(4);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(138, 85);
            this.createButton.TabIndex = 4;
            this.createButton.Text = "Create new Admin";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // adminRadioButton
            // 
            this.adminRadioButton.AutoSize = true;
            this.adminRadioButton.Location = new System.Drawing.Point(324, 67);
            this.adminRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.adminRadioButton.Name = "adminRadioButton";
            this.adminRadioButton.Size = new System.Drawing.Size(140, 41);
            this.adminRadioButton.TabIndex = 0;
            this.adminRadioButton.Text = "Admin";
            this.adminRadioButton.UseVisualStyleBackColor = true;
            // 
            // labRadioButton
            // 
            this.labRadioButton.AutoSize = true;
            this.labRadioButton.Checked = true;
            this.labRadioButton.Location = new System.Drawing.Point(20, 67);
            this.labRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.labRadioButton.Name = "labRadioButton";
            this.labRadioButton.Size = new System.Drawing.Size(242, 41);
            this.labRadioButton.TabIndex = 0;
            this.labRadioButton.Text = "Lab Assistant";
            this.labRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.oldPassBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.updateButton);
            this.groupBox2.Controls.Add(this.updateListBox);
            this.groupBox2.Controls.Add(this.updatePassBox);
            this.groupBox2.Controls.Add(this.confirmPassBox);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(608, 87);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(774, 621);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Update users";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(303, 86);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(223, 37);
            this.label6.TabIndex = 0;
            this.label6.Text = "Old password:";
            // 
            // oldPassBox
            // 
            this.oldPassBox.Location = new System.Drawing.Point(583, 79);
            this.oldPassBox.Margin = new System.Windows.Forms.Padding(6);
            this.oldPassBox.Name = "oldPassBox";
            this.oldPassBox.Size = new System.Drawing.Size(172, 44);
            this.oldPassBox.TabIndex = 6;
            this.oldPassBox.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(303, 224);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(260, 80);
            this.label5.TabIndex = 0;
            this.label5.Text = "Confirm password:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(303, 160);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(237, 37);
            this.label4.TabIndex = 0;
            this.label4.Text = "New password:";
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(423, 342);
            this.updateButton.Margin = new System.Windows.Forms.Padding(4);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(174, 92);
            this.updateButton.TabIndex = 9;
            this.updateButton.Text = "Update Admin password";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // updateListBox
            // 
            this.updateListBox.FormattingEnabled = true;
            this.updateListBox.ItemHeight = 37;
            this.updateListBox.Location = new System.Drawing.Point(40, 52);
            this.updateListBox.Margin = new System.Windows.Forms.Padding(4);
            this.updateListBox.Name = "updateListBox";
            this.updateListBox.Size = new System.Drawing.Size(253, 374);
            this.updateListBox.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.removeButton);
            this.groupBox3.Controls.Add(this.removeListBox);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(1422, 87);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(388, 621);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Remove users";
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(70, 454);
            this.removeButton.Margin = new System.Windows.Forms.Padding(4);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(199, 92);
            this.removeButton.TabIndex = 11;
            this.removeButton.Text = "Remove Admin";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // removeListBox
            // 
            this.removeListBox.FormattingEnabled = true;
            this.removeListBox.ItemHeight = 37;
            this.removeListBox.Location = new System.Drawing.Point(52, 52);
            this.removeListBox.Margin = new System.Windows.Forms.Padding(4);
            this.removeListBox.Name = "removeListBox";
            this.removeListBox.Size = new System.Drawing.Size(284, 337);
            this.removeListBox.TabIndex = 10;
            // 
            // CreateUpdateAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1914, 829);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MinimumSize = new System.Drawing.Size(1940, 900);
            this.Name = "CreateUpdateAdmin";
            this.Text = "CreateUpdateAdmin";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox createNameBox;
        private System.Windows.Forms.TextBox createPassBox;
        private System.Windows.Forms.TextBox createConfirmBox;
        private System.Windows.Forms.TextBox updatePassBox;
        private System.Windows.Forms.TextBox confirmPassBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.RadioButton adminRadioButton;
        private System.Windows.Forms.RadioButton labRadioButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.ListBox updateListBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.ListBox removeListBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox oldPassBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}