using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SiteBlocker
{
    public partial class PaymentForm : Form
    {
        private SiteBlockerForm.SiteBlock CurrentSiteBlock;
        private static Uri PaymentUri = new Uri(Properties.Resources.PaymentUrl);
        private static Uri PaymentPassedUri = new Uri(Properties.Resources.PaymentPassedUrl);
        private static Uri PaymentFailedUri = new Uri(Properties.Resources.PaymentFailedUrl);

        public PaymentForm(SiteBlockerForm.SiteBlock CurrentSiteBlock)
        {
            this.CurrentSiteBlock = CurrentSiteBlock;
            InitializeComponent();

            PaymentBrowser.Url = PaymentUri;
        }

        private void PaymentBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.Equals(PaymentPassedUri))
            {
                CurrentSiteBlock.Unblock();
            }
            else if (e.Url.Equals(PaymentFailedUri))
            {
                PaymentBrowser.Url = PaymentUri;
            }
        }
    }
}