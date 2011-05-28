using System;
using System.Windows.Forms;

namespace SiteBlocker
{
    public partial class PaymentForm : Form
    {
        private SiteBlock TargetSite;
        private static Uri PaymentUri = new Uri(Properties.Resources.PaymentUrl);
        private static Uri PaymentPassedUri = new Uri(Properties.Resources.PaymentPassedUrl);
        private static Uri PaymentFailedUri = new Uri(Properties.Resources.PaymentFailedUrl);

        private static PaymentForm SINGLETON = new PaymentForm();
        public static PaymentForm Singleton()
        {
            return SINGLETON;
        }

        private PaymentForm()
        {
            Hide();
            InitializeComponent();
        }

        /// <summary>
        /// Unblocks a SiteBlock early if user completes payment.
        /// </summary>
        /// <param name="TargetSite">Site to be unblocked when payment is complete.</param>
        public void PayFor(SiteBlock TargetSite)
        {
            Show();
            this.TargetSite = TargetSite;
            PaymentBrowser.Url = PaymentUri;
        }

        private void PaymentBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.Equals(PaymentPassedUri))
            {
                TargetSite.Unblock();
            }
            else if (e.Url.Equals(PaymentFailedUri))
            {
                PaymentBrowser.Url = PaymentUri;
            }
        }

        private void PaymentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
    }
}