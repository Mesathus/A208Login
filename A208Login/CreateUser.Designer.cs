namespace A208Login
{
    partial class CreateUser
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
            this.firstBox = new System.Windows.Forms.TextBox();
            this.lastBox = new System.Windows.Forms.TextBox();
            this.passBox = new System.Windows.Forms.TextBox();
            this.confirmBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.studentsDataSet1 = new A208Login.StudentsDataSet();
            ((System.ComponentModel.ISupportInitialize)(this.studentsDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // firstBox
            // 
            this.firstBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstBox.Location = new System.Drawing.Point(432, 105);
            this.firstBox.Name = "firstBox";
            this.firstBox.Size = new System.Drawing.Size(239, 56);
            this.firstBox.TabIndex = 0;
            // 
            // lastBox
            // 
            this.lastBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastBox.Location = new System.Drawing.Point(432, 184);
            this.lastBox.Name = "lastBox";
            this.lastBox.Size = new System.Drawing.Size(239, 56);
            this.lastBox.TabIndex = 1;
            // 
            // passBox
            // 
            this.passBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passBox.Location = new System.Drawing.Point(432, 311);
            this.passBox.Name = "passBox";
            this.passBox.Size = new System.Drawing.Size(239, 56);
            this.passBox.TabIndex = 2;
            this.passBox.UseSystemPasswordChar = true;
            // 
            // confirmBox
            // 
            this.confirmBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmBox.Location = new System.Drawing.Point(432, 393);
            this.confirmBox.Name = "confirmBox";
            this.confirmBox.Size = new System.Drawing.Size(239, 56);
            this.confirmBox.TabIndex = 3;
            this.confirmBox.UseSystemPasswordChar = true;
            // 
            // addButton
            // 
            this.addButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.Location = new System.Drawing.Point(124, 555);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(176, 80);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "Add User";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(124, 717);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(176, 80);
            this.exitButton.TabIndex = 5;
            this.exitButton.Text = "Close";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(105, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 51);
            this.label1.TabIndex = 6;
            this.label1.Text = "First Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(105, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(242, 51);
            this.label2.TabIndex = 7;
            this.label2.Text = "Last Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(105, 314);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(237, 51);
            this.label3.TabIndex = 8;
            this.label3.Text = "Student ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(105, 396);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(239, 51);
            this.label4.TabIndex = 9;
            this.label4.Text = "Confirm ID:";
            // 
            // studentsDataSet1
            // 
            this.studentsDataSet1.DataSetName = "StudentsDataSet";
            this.studentsDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // CreateUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 937);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.confirmBox);
            this.Controls.Add(this.passBox);
            this.Controls.Add(this.lastBox);
            this.Controls.Add(this.firstBox);
            this.Name = "CreateUser";
            this.Text = "CreateUser";
            ((System.ComponentModel.ISupportInitialize)(this.studentsDataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox firstBox;
        private System.Windows.Forms.TextBox lastBox;
        private System.Windows.Forms.TextBox passBox;
        private System.Windows.Forms.TextBox confirmBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private StudentsDataSet studentsDataSet1;
    }
}