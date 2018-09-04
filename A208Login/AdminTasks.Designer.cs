namespace A208Login
{
    partial class AdminTasks
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
            this.logListBox = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.updateCreateAdminsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeStudentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCurrentLogToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDatabaseDumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importDatabaseObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupAndDeleteDatabaseEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logNameList = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // logListBox
            // 
            this.logListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logListBox.FormattingEnabled = true;
            this.logListBox.ItemHeight = 37;
            this.logListBox.Location = new System.Drawing.Point(411, 177);
            this.logListBox.Margin = new System.Windows.Forms.Padding(6);
            this.logListBox.Name = "logListBox";
            this.logListBox.Size = new System.Drawing.Size(1549, 818);
            this.logListBox.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateCreateAdminsToolStripMenuItem,
            this.removeStudentsToolStripMenuItem,
            this.openLogToolStripMenuItem1,
            this.databaseManagementToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(12, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(1999, 46);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // updateCreateAdminsToolStripMenuItem
            // 
            this.updateCreateAdminsToolStripMenuItem.Name = "updateCreateAdminsToolStripMenuItem";
            this.updateCreateAdminsToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.updateCreateAdminsToolStripMenuItem.Text = "Update/Create Admins";
            this.updateCreateAdminsToolStripMenuItem.Click += new System.EventHandler(this.updateCreateAdminsToolStripMenuItem_Click);
            // 
            // removeStudentsToolStripMenuItem
            // 
            this.removeStudentsToolStripMenuItem.Name = "removeStudentsToolStripMenuItem";
            this.removeStudentsToolStripMenuItem.Size = new System.Drawing.Size(213, 38);
            this.removeStudentsToolStripMenuItem.Text = "Remove Students";
            this.removeStudentsToolStripMenuItem.Click += new System.EventHandler(this.removeStudentsToolStripMenuItem_Click);
            // 
            // openLogToolStripMenuItem1
            // 
            this.openLogToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLogToolStripMenuItem,
            this.exportCurrentLogToolStripMenuItem1});
            this.openLogToolStripMenuItem1.Name = "openLogToolStripMenuItem1";
            this.openLogToolStripMenuItem1.Size = new System.Drawing.Size(114, 38);
            this.openLogToolStripMenuItem1.Text = "Logging";
            this.openLogToolStripMenuItem1.Click += new System.EventHandler(this.openLogToolStripMenuItem1_Click);
            // 
            // openLogToolStripMenuItem
            // 
            this.openLogToolStripMenuItem.Name = "openLogToolStripMenuItem";
            this.openLogToolStripMenuItem.Size = new System.Drawing.Size(314, 38);
            this.openLogToolStripMenuItem.Text = "Open Log";
            this.openLogToolStripMenuItem.Click += new System.EventHandler(this.openLogToolStripMenuItem_Click);
            // 
            // exportCurrentLogToolStripMenuItem1
            // 
            this.exportCurrentLogToolStripMenuItem1.Name = "exportCurrentLogToolStripMenuItem1";
            this.exportCurrentLogToolStripMenuItem1.Size = new System.Drawing.Size(314, 38);
            this.exportCurrentLogToolStripMenuItem1.Text = "Export Current Log";
            this.exportCurrentLogToolStripMenuItem1.Click += new System.EventHandler(this.exportCurrentLogToolStripMenuItem1_Click);
            // 
            // databaseManagementToolStripMenuItem
            // 
            this.databaseManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createDatabaseDumpToolStripMenuItem,
            this.importDatabaseObjectsToolStripMenuItem,
            this.backupAndDeleteDatabaseEntriesToolStripMenuItem});
            this.databaseManagementToolStripMenuItem.Name = "databaseManagementToolStripMenuItem";
            this.databaseManagementToolStripMenuItem.Size = new System.Drawing.Size(275, 38);
            this.databaseManagementToolStripMenuItem.Text = "Database Management";
            // 
            // createDatabaseDumpToolStripMenuItem
            // 
            this.createDatabaseDumpToolStripMenuItem.Name = "createDatabaseDumpToolStripMenuItem";
            this.createDatabaseDumpToolStripMenuItem.Size = new System.Drawing.Size(498, 38);
            this.createDatabaseDumpToolStripMenuItem.Text = "Create Database Dump";
            this.createDatabaseDumpToolStripMenuItem.Click += new System.EventHandler(this.createDatabaseDumpToolStripMenuItem_Click_1);
            // 
            // importDatabaseObjectsToolStripMenuItem
            // 
            this.importDatabaseObjectsToolStripMenuItem.Name = "importDatabaseObjectsToolStripMenuItem";
            this.importDatabaseObjectsToolStripMenuItem.Size = new System.Drawing.Size(498, 38);
            this.importDatabaseObjectsToolStripMenuItem.Text = "Import Database Objects";
            this.importDatabaseObjectsToolStripMenuItem.Click += new System.EventHandler(this.importDatabaseObjectsToolStripMenuItem_Click_1);
            // 
            // backupAndDeleteDatabaseEntriesToolStripMenuItem
            // 
            this.backupAndDeleteDatabaseEntriesToolStripMenuItem.Name = "backupAndDeleteDatabaseEntriesToolStripMenuItem";
            this.backupAndDeleteDatabaseEntriesToolStripMenuItem.Size = new System.Drawing.Size(498, 38);
            this.backupAndDeleteDatabaseEntriesToolStripMenuItem.Text = "Backup and Delete Database Entries";
            this.backupAndDeleteDatabaseEntriesToolStripMenuItem.Click += new System.EventHandler(this.backupAndDeleteDatabaseEntriesToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(64, 38);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // logNameList
            // 
            this.logNameList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logNameList.FormattingEnabled = true;
            this.logNameList.ItemHeight = 37;
            this.logNameList.Location = new System.Drawing.Point(24, 177);
            this.logNameList.Margin = new System.Windows.Forms.Padding(6);
            this.logNameList.Name = "logNameList";
            this.logNameList.Size = new System.Drawing.Size(282, 374);
            this.logNameList.TabIndex = 2;
            // 
            // AdminTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1999, 1031);
            this.Controls.Add(this.logNameList);
            this.Controls.Add(this.logListBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AdminTasks";
            this.Text = "Administrative tasks";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox logListBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem updateCreateAdminsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeStudentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLogToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exportCurrentLogToolStripMenuItem1;
        private System.Windows.Forms.ListBox logNameList;
        private System.Windows.Forms.ToolStripMenuItem openLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createDatabaseDumpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importDatabaseObjectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupAndDeleteDatabaseEntriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}