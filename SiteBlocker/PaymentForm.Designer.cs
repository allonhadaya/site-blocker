namespace SiteBlocker
{
    partial class PaymentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentForm));
            this.PaymentBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // PaymentBrowser
            // 
            this.PaymentBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PaymentBrowser.Location = new System.Drawing.Point(0, 0);
            this.PaymentBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.PaymentBrowser.Name = "PaymentBrowser";
            this.PaymentBrowser.Size = new System.Drawing.Size(602, 307);
            this.PaymentBrowser.TabIndex = 0;
            this.PaymentBrowser.Url = new System.Uri("", System.UriKind.Relative);
            this.PaymentBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.PaymentBrowser_DocumentCompleted);
            // 
            // PaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 307);
            this.Controls.Add(this.PaymentBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PaymentForm";
            this.Text = "Unlock Site";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser PaymentBrowser;
    }
}