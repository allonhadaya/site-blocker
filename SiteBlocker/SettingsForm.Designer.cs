namespace SiteBlocker
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.NotifyCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // NotifyCheckBox
            // 
            this.NotifyCheckBox.AutoSize = true;
            this.NotifyCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NotifyCheckBox.Checked = global::SiteBlocker.Properties.Settings.Default.NotifyAvailable;
            this.NotifyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.NotifyCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SiteBlocker.Properties.Settings.Default, "NotifyAvailable", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.NotifyCheckBox.Location = new System.Drawing.Point(29, 29);
            this.NotifyCheckBox.Margin = new System.Windows.Forms.Padding(20, 20, 3, 3);
            this.NotifyCheckBox.Name = "NotifyCheckBox";
            this.NotifyCheckBox.Size = new System.Drawing.Size(171, 17);
            this.NotifyCheckBox.TabIndex = 1;
            this.NotifyCheckBox.Text = "notify me when site is available";
            this.NotifyCheckBox.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 267);
            this.Controls.Add(this.NotifyCheckBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox NotifyCheckBox;

    }
}