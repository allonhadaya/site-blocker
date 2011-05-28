using System;
using System.Net;
using System.Windows.Forms;

namespace SiteBlocker
{
    public abstract class SiteBlock
    {
        protected String Site;
        protected IPAddress Address;

        public Label SiteLabel;
        public Label TimeLabel;
        public Button RemoveButton;

        protected SiteBlock(String Site, IPAddress Address)
        {
            this.Site = Site;
            this.Address = Address;
            SiteLabel = new Label();
            TimeLabel = new Label();
            RemoveButton = new Button();

            // Site Label
            SiteLabel.AutoSize = true;
            SiteLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            SiteLabel.TabIndex = 1;
            SiteLabel.Text = Site;
            SiteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // TimeLabel
            TimeLabel.AutoSize = true;
            TimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            TimeLabel.TabIndex = 2;
            TimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // RemoveButton
            RemoveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            RemoveButton.TabIndex = 3;
            RemoveButton.Text = "remove";
            RemoveButton.UseVisualStyleBackColor = true;
        }

        public String Name()
        {
            return Site;
        }

        /// <summary>
        /// Unblocks a site and removes it from the UI.
        /// </summary>
        public abstract void Unblock();

        /// <summary>
        /// Parses a String representation of a SiteBlock and returns the appropriate instance.
        /// </summary>
        public static SiteBlock Parse(String Input)
        {
            String[] FirstSplit;
            String[] SecondSplit;

            String NewSite;
            IPAddress NewAddress = null;
            DateTime NewEndTime = new DateTime();

            FirstSplit = Input.Split(new String[] { "\t", " ", "\r" }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (FirstSplit.Length > 0 && IPAddress.TryParse(FirstSplit[0], out NewAddress))
            {
                SecondSplit = FirstSplit[1].Split(new String[] { "#" }, 2, StringSplitOptions.RemoveEmptyEntries);
                NewSite = SecondSplit[0];
                if (SecondSplit.Length > 1 && DateTime.TryParse(SecondSplit[1], out NewEndTime))
                {
                    return new TimedSiteBlock(NewSite, NewAddress, NewEndTime);
                }
                else
                {
                    return new StaticSiteBlock(NewSite, NewAddress);
                }
            }
            throw new Exception();
        }
    }

    /// <summary>
    /// SiteBlock which is released at a given time.
    /// </summary>
    class TimedSiteBlock : SiteBlock
    {
        private DateTime EndTime;
        private Timer BlockTimer;

        public TimedSiteBlock(String Site, IPAddress Address, DateTime EndTime) : base(Site, Address)
        {
            this.EndTime = EndTime;
            TimeLabel.Text = ((EndTime - DateTime.Now).ToString().Split(new Char[] { '.' })[0]);

            RemoveButton.Click += delegate(object removeSender, EventArgs eRemove)
            {
                PaymentForm.PayFor(this);
            };

            BlockTimer = new Timer();
            BlockTimer.Interval = 1000;
            BlockTimer.Tick += delegate(object tickSender, EventArgs eTick)
            {
                if (DateTime.Compare(EndTime, DateTime.Now) < 0)
                {
                    Unblock();
                    return;
                }
                TimeLabel.Text = (EndTime - DateTime.Now).ToString().Split(new Char[] { '.' })[0];
            };
            BlockTimer.Start();
        }

        public override string ToString()
        {
            return Address.ToString() + "\t" + Site + "#" + EndTime.ToString();
        }

        public override void Unblock()
        {
            SiteLabel.Dispose();
            TimeLabel.Dispose();
            RemoveButton.Dispose();
            BlockTimer.Dispose();
            SiteBlockerForm.Unblock(this);
        }
    }

    /// <summary>
    /// SiteBlock which has no end time.
    /// </summary>
    class StaticSiteBlock : SiteBlock
    {
        public StaticSiteBlock(String Site, IPAddress Address) : base(Site, Address)
        {
            TimeLabel.Text = "routed to " + Address.ToString();

            RemoveButton.Click += delegate(object removeSender, EventArgs eRemove)
            {
                Unblock();
            };
        }

        public override string ToString()
        {
            return Address.ToString() + "\t" + Site;
        }

        public override void Unblock()
        {
            SiteLabel.Dispose();
            TimeLabel.Dispose();
            RemoveButton.Dispose();
            SiteBlockerForm.Unblock(this);
        }
    }
}
