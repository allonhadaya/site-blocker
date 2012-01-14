using System;
using System.Windows.Forms;

namespace SiteBlocker
{
    public partial class SiteBlockerForm : Form
    {
        public SiteBlockerForm()
        {
            InitializeComponent();

            Host.AddToUI = AddToUI;
            Host.RemoveFromUI = RemoveFromUI;
        }

        private Control[] AddToUI(Host host)
        {
            var siteLabel = new Label() {
                AutoSize = true,
                Dock = DockStyle.Fill,
                TabIndex = 1,
                Text = host.HOST,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            var timeLabel = new Label() {
                AutoSize = true,
                Dock = DockStyle.Fill,
                TabIndex = 2,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            var removeButton = new Button() {
                Dock = DockStyle.Fill,
                TabIndex = 3,
                Text = "remove",
                UseVisualStyleBackColor = true
            };

            if (host.END_TIME.HasValue) {
                timeLabel.Text = host.END_TIME.ToString();

                var t = new Timer() {
                    Enabled = true,
                    Interval = 1000
                };
                
                t.Tick += (sender, e) => {
                    if ((host.END_TIME.Value - DateTime.Now).TotalSeconds > 0) {
                        timeLabel.Text = TimeSpan.Parse(timeLabel.Text).Subtract(new TimeSpan(0, 0, 1)).ToString();
                        t.Dispose();
                    }
                    else {
                        host.Unblock();
                    }
                };

                removeButton.Click += (sender, e) => {
                    host.Unblock();
                };
            } else {
                timeLabel.Text = "routed to " + host.REDIRECT;

                removeButton.Click += (sender, e) => {
                    host.Unblock();
                };
            }

            MainTablePanel.RowCount++;
            MainTablePanel.RowStyles.Add(new RowStyle());
            MainTablePanel.Controls.Add(siteLabel, 0, -1);
            MainTablePanel.Controls.Add(timeLabel, 1, -1);
            MainTablePanel.Controls.Add(removeButton, 2, -1);

            return new Control[] { siteLabel, timeLabel, removeButton };
        }

        private void RemoveFromUI(Control[] controls)
        {
            foreach (var c in controls) {
                MainTablePanel.Controls.Remove(c);
                c.Dispose();
            };
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            EndTimeSpinner.Value = DateTime.Today
                .AddHours(EndTimeSpinner.Value.Hour)
                .AddMinutes(EndTimeSpinner.Value.Minute)
                .AddSeconds(EndTimeSpinner.Value.Second);
            new Host(SiteTextBox.Text, "0.0.0.0", EndTimeSpinner.Value).Block();
        }
    }
}