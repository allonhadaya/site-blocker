namespace SiteBlocker
{
    partial class SiteBlockerForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SiteBlockerForm));
            this.MainTablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.EndTimeSpinner = new System.Windows.Forms.DateTimePicker();
            this.AddButton = new System.Windows.Forms.Button();
            this.SiteTextBox = new System.Windows.Forms.TextBox();
            this.SiteLabel = new System.Windows.Forms.Label();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.ActionLabel = new System.Windows.Forms.Label();
            this.ApplicationTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ApplicationTrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RestoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ApplicationMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hostsFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RootTablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.MainTablePanel.SuspendLayout();
            this.ApplicationTrayMenu.SuspendLayout();
            this.ApplicationMenuStrip.SuspendLayout();
            this.RootTablePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTablePanel
            // 
            this.MainTablePanel.AutoSize = true;
            this.MainTablePanel.ColumnCount = 3;
            this.MainTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33111F));
            this.MainTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33444F));
            this.MainTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33445F));
            this.MainTablePanel.Controls.Add(this.EndTimeSpinner, 1, 1);
            this.MainTablePanel.Controls.Add(this.AddButton, 2, 1);
            this.MainTablePanel.Controls.Add(this.SiteTextBox, 0, 1);
            this.MainTablePanel.Controls.Add(this.SiteLabel, 0, 0);
            this.MainTablePanel.Controls.Add(this.TimeLabel, 1, 0);
            this.MainTablePanel.Controls.Add(this.ActionLabel, 2, 0);
            this.MainTablePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainTablePanel.Location = new System.Drawing.Point(3, 27);
            this.MainTablePanel.Name = "MainTablePanel";
            this.MainTablePanel.RowCount = 2;
            this.MainTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTablePanel.Size = new System.Drawing.Size(465, 39);
            this.MainTablePanel.TabIndex = 0;
            // 
            // EndTimeSpinner
            // 
            this.EndTimeSpinner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EndTimeSpinner.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.EndTimeSpinner.Location = new System.Drawing.Point(157, 16);
            this.EndTimeSpinner.Name = "EndTimeSpinner";
            this.EndTimeSpinner.ShowUpDown = true;
            this.EndTimeSpinner.Size = new System.Drawing.Size(149, 20);
            this.EndTimeSpinner.TabIndex = 1;
            // 
            // AddButton
            // 
            this.AddButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddButton.Location = new System.Drawing.Point(312, 16);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(150, 20);
            this.AddButton.TabIndex = 2;
            this.AddButton.Text = "add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // SiteTextBox
            // 
            this.SiteTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SiteTextBox.Location = new System.Drawing.Point(3, 16);
            this.SiteTextBox.Name = "SiteTextBox";
            this.SiteTextBox.Size = new System.Drawing.Size(148, 20);
            this.SiteTextBox.TabIndex = 0;
            this.SiteTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SiteLabel
            // 
            this.SiteLabel.AutoSize = true;
            this.SiteLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SiteLabel.Location = new System.Drawing.Point(3, 0);
            this.SiteLabel.Name = "SiteLabel";
            this.SiteLabel.Size = new System.Drawing.Size(148, 13);
            this.SiteLabel.TabIndex = 7;
            this.SiteLabel.Text = "Site";
            this.SiteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TimeLabel.Location = new System.Drawing.Point(157, 0);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(149, 13);
            this.TimeLabel.TabIndex = 8;
            this.TimeLabel.Text = "Time";
            this.TimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ActionLabel
            // 
            this.ActionLabel.AutoSize = true;
            this.ActionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ActionLabel.Location = new System.Drawing.Point(312, 0);
            this.ActionLabel.Name = "ActionLabel";
            this.ActionLabel.Size = new System.Drawing.Size(150, 13);
            this.ActionLabel.TabIndex = 9;
            this.ActionLabel.Text = "Action";
            this.ActionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ApplicationTrayIcon
            // 
            this.ApplicationTrayIcon.ContextMenuStrip = this.ApplicationTrayMenu;
            this.ApplicationTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("ApplicationTrayIcon.Icon")));
            this.ApplicationTrayIcon.Text = "Site Blocker";
            this.ApplicationTrayIcon.Visible = true;
            this.ApplicationTrayIcon.DoubleClick += new System.EventHandler(this.ApplicationTrayIcon_DoubleClick);
            // 
            // ApplicationTrayMenu
            // 
            this.ApplicationTrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RestoreToolStripMenuItem,
            this.CloseApplicationToolStripMenuItem});
            this.ApplicationTrayMenu.Name = "ApplicationTrayMenu";
            this.ApplicationTrayMenu.Size = new System.Drawing.Size(168, 48);
            // 
            // RestoreToolStripMenuItem
            // 
            this.RestoreToolStripMenuItem.Name = "RestoreToolStripMenuItem";
            this.RestoreToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.RestoreToolStripMenuItem.Text = "Restore";
            this.RestoreToolStripMenuItem.Click += new System.EventHandler(this.RestoreToolStripMenuItem_Click);
            // 
            // CloseApplicationToolStripMenuItem
            // 
            this.CloseApplicationToolStripMenuItem.Name = "CloseApplicationToolStripMenuItem";
            this.CloseApplicationToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.CloseApplicationToolStripMenuItem.Text = "Close Application";
            this.CloseApplicationToolStripMenuItem.Click += new System.EventHandler(this.CloseApplicationToolStripMenuItem_Click);
            // 
            // ApplicationMenuStrip
            // 
            this.ApplicationMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.ApplicationMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.ApplicationMenuStrip.Name = "ApplicationMenuStrip";
            this.ApplicationMenuStrip.Size = new System.Drawing.Size(471, 24);
            this.ApplicationMenuStrip.TabIndex = 1;
            this.ApplicationMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.hostsFileToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // hostsFileToolStripMenuItem
            // 
            this.hostsFileToolStripMenuItem.Name = "hostsFileToolStripMenuItem";
            this.hostsFileToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.hostsFileToolStripMenuItem.Text = "&Hosts File";
            this.hostsFileToolStripMenuItem.Click += new System.EventHandler(this.hostsFileToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // RootTablePanel
            // 
            this.RootTablePanel.AutoSize = true;
            this.RootTablePanel.ColumnCount = 1;
            this.RootTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.RootTablePanel.Controls.Add(this.ApplicationMenuStrip, 0, 0);
            this.RootTablePanel.Controls.Add(this.MainTablePanel, 0, 1);
            this.RootTablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RootTablePanel.Location = new System.Drawing.Point(0, 0);
            this.RootTablePanel.Name = "RootTablePanel";
            this.RootTablePanel.RowCount = 2;
            this.RootTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.RootTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.RootTablePanel.Size = new System.Drawing.Size(471, 252);
            this.RootTablePanel.TabIndex = 2;
            // 
            // SiteBlockerForm
            // 
            this.AcceptButton = this.AddButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(471, 252);
            this.Controls.Add(this.RootTablePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SiteBlockerForm";
            this.Text = "SiteBlocker";
            this.Resize += new System.EventHandler(this.SiteBlockerForm_Resize);
            this.MainTablePanel.ResumeLayout(false);
            this.MainTablePanel.PerformLayout();
            this.ApplicationTrayMenu.ResumeLayout(false);
            this.ApplicationMenuStrip.ResumeLayout(false);
            this.ApplicationMenuStrip.PerformLayout();
            this.RootTablePanel.ResumeLayout(false);
            this.RootTablePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTablePanel;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.TextBox SiteTextBox;
        private System.Windows.Forms.Label SiteLabel;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Label ActionLabel;
        private System.Windows.Forms.DateTimePicker EndTimeSpinner;
        private System.Windows.Forms.NotifyIcon ApplicationTrayIcon;
        private System.Windows.Forms.ContextMenuStrip ApplicationTrayMenu;
        private System.Windows.Forms.ToolStripMenuItem RestoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseApplicationToolStripMenuItem;
        private System.Windows.Forms.MenuStrip ApplicationMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel RootTablePanel;
        private System.Windows.Forms.ToolStripMenuItem hostsFileToolStripMenuItem;

    }
}