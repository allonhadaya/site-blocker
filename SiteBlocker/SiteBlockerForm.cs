using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using SiteBlocker.Properties;

namespace SiteBlocker
{
    public partial class SiteBlockerForm : Form
    {
        private delegate void UpdateTrayIconTextDelegate();
        public delegate void AddSiteToTableDelegate(SiteBlock Value);
        public delegate void RemoveSiteFromTableDelegate(SiteBlock Value);
        public delegate void ShowBalloonTipDelegate(int timeout, String tipTitle, String tipText, ToolTipIcon tipIcon);

        private static UpdateTrayIconTextDelegate UpdateTrayIconText;
        private static ShowBalloonTipDelegate ShowBalloonTip;
        public static AddSiteToTableDelegate AddSiteToTable;
        public static RemoveSiteFromTableDelegate RemoveSiteFromTable;

        private static List<SiteBlock> SiteBlocks = new List<SiteBlock>();

        public SiteBlockerForm()
        {
            InitializeComponent();
            Settings.Default.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Default_PropertyChanged);

            UpdateTrayIconText = UpdateTrayIconTextMethod;
            UpdateNotifyAvailable();
            AddSiteToTable = AddSiteToTableMethod;
            RemoveSiteFromTable = RemoveSiteFromTableMethod;

            ReadBlockList();
        }

        private static void FlushDNS()
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "ipconfig";
            proc.StartInfo.Arguments = "/flushdns";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            proc.WaitForExit();
        }

        private static void ReadBlockList()
        {
            TextReader reader = new StreamReader(System.Environment.GetEnvironmentVariable("windir") + "\\System32\\drivers\\etc\\hosts");
            String[] HostsFile = reader.ReadToEnd().Split('\n');
            reader.Close();

            foreach (String CurrentLine in HostsFile)
            {
                if (CurrentLine.StartsWith("#") || CurrentLine.StartsWith("\r"))
                {
                    continue;
                }
                try
                {
                    SiteBlock.Parse(CurrentLine);
                }
                catch
                {
                    // line not parsed
                }
            }
        }

        private static void WriteBlockList()
        {
            FlushDNS();

            TextWriter writer = new StreamWriter(System.Environment.GetEnvironmentVariable("windir") + "\\System32\\drivers\\etc\\hosts");
            writer.WriteLine("# Site Blocker by Allon Hadaya");
            foreach (SiteBlock CurrentBlock in SiteBlocks)
            {
                writer.WriteLine(CurrentBlock.ToString());
            }
            writer.Close();

            UpdateTrayIconText();
        }

        private static void OpenHostsFile()
        {
            Process proc = new Process();
            proc.StartInfo.FileName = System.Environment.GetEnvironmentVariable("windir") + "\\System32\\notepad";
            proc.StartInfo.Arguments = System.Environment.GetEnvironmentVariable("windir") + "\\System32\\drivers\\etc\\hosts";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
        }

        public class SiteBlock
        {
            private String Site;
            private IPAddress Address;
            private DateTime EndTime;
            private Timer BlockTimer;
            private Boolean Timed;

            public Label SiteLabel;
            public Label TimeLabel;
            public Button RemoveButton;

            // not timed
            public SiteBlock(String Site, IPAddress Address)
            {
                this.Timed = false;
                this.Site = Site;
                this.Address = Address;
                InitializeComponents();

                SiteBlocks.Add(this);
                AddSiteToTable(this);
            }

            // timed
            public SiteBlock(String Site, IPAddress Address, DateTime EndTime)
            {
                this.Timed = true;
                this.Site = Site;
                this.Address = Address;
                this.EndTime = EndTime;
                InitializeComponents();
                InitializeTimer();
                RemoveButton.Enabled = false;

                SiteBlocks.Add(this);
                AddSiteToTable(this);
            }

            private void InitializeComponents()
            {
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
                TimeLabel.Text = Timed ? (EndTime - DateTime.Now).ToString().Split(new Char[] { '.' })[0] : "routed to " + Address.ToString();

                // RemoveButton
                RemoveButton.Dock = System.Windows.Forms.DockStyle.Fill;
                RemoveButton.TabIndex = 3;
                RemoveButton.Text = "remove";
                RemoveButton.UseVisualStyleBackColor = true;
                RemoveButton.Click += delegate(object removeSender, EventArgs eRemove)
                {
                    if (SiteBlocks.Contains(this))
                    {
                        if (Timed)
                        {
                            //new PaymentForm(this).Show();
                            RemoveSiteFromTable(this);
                        }
                        else
                        {
                            Unblock();
                            RemoveSiteFromTable(this);
                        }
                    }
                    else
                    {
                        RemoveSiteFromTable(this);
                    }
                };
            }

            private void InitializeTimer()
            {
                BlockTimer = new Timer();
                BlockTimer.Interval = 1000;
                BlockTimer.Tick += delegate(object tickSender, EventArgs eTick)
                {
                    if (DateTime.Compare(EndTime, DateTime.Now) < 0)
                    {
                        Unblock();
                        RemoveButton.Enabled = true;
                        BlockTimer.Stop();
                        BlockTimer.Dispose();
                        return;
                    }
                    TimeLabel.Text = (EndTime - DateTime.Now).ToString().Split(new Char[] { '.' })[0];
                };
                BlockTimer.Start();
            }

            public void Unblock()
            {
                SiteBlocks.Remove(this);
                WriteBlockList();
                TimeLabel.Text = "available";
                ShowBalloonTip(0, "", Site + " is available", ToolTipIcon.None);
            }

            public override String ToString()
            {
                String result = Address.ToString() + "\t" + Site;
                if (Timed)
                {
                    result += "#" + EndTime.ToString();
                }
                return result;
            }

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
                        return new SiteBlock(NewSite, NewAddress, NewEndTime);
                    }
                    else
                    {
                        return new SiteBlock(NewSite, NewAddress);
                    }
                }
                throw new Exception();
            }
        }

        //-------------------------------- helper methods ------------------------------------------

        private void AddSiteToTableMethod(SiteBlock NewSiteBlock)
        {
            MainTablePanel.RowCount += 1;
            MainTablePanel.RowStyles.Add(new RowStyle());
            int NewSiteIndex = MainTablePanel.RowCount - 1;

            MainTablePanel.Controls.Add(NewSiteBlock.SiteLabel, 0, NewSiteIndex);
            MainTablePanel.Controls.Add(NewSiteBlock.TimeLabel, 1, NewSiteIndex);
            MainTablePanel.Controls.Add(NewSiteBlock.RemoveButton, 2, NewSiteIndex);
        }

        private void RemoveSiteFromTableMethod(SiteBlock SiteBlockToRemove)
        {
            MainTablePanel.Controls.Remove(SiteBlockToRemove.SiteLabel);
            MainTablePanel.Controls.Remove(SiteBlockToRemove.TimeLabel);
            MainTablePanel.Controls.Remove(SiteBlockToRemove.RemoveButton);
        }

        private void UpdateTrayIconTextMethod()
        {
            String TrayText = "Site Blocker";
            foreach (SiteBlock CurrentSite in SiteBlocks)
            {
                TrayText += "\n" + CurrentSite.SiteLabel.Text;
            }
            ApplicationTrayIcon.Text = (TrayText.Length >= 64) ? (TrayText.Substring(0, 60) + "...") : (TrayText);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            EndTimeSpinner.Value = DateTime.Today.AddHours(EndTimeSpinner.Value.Hour).
                                                  AddMinutes(EndTimeSpinner.Value.Minute).
                                                  AddSeconds(EndTimeSpinner.Value.Second);
            SiteBlock NewSiteBlock = new SiteBlock(SiteTextBox.Text, new IPAddress(new byte[] { 0, 0, 0, 0 }), EndTimeSpinner.Value);
            WriteBlockList();
        }

        private void SiteBlockerForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
            }
        }

        private void ApplicationTrayIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void RestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void CloseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsForm().Show();
        }

        private void hostsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenHostsFile();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().Show();
        }

        private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("NotifyAvailable"))
            {
                UpdateNotifyAvailable();
            }
            Settings.Default.Save();
        }

        private void UpdateNotifyAvailable()
        {
            ShowBalloonTip = Settings.Default.NotifyAvailable ?
                 ((ShowBalloonTipDelegate)ApplicationTrayIcon.ShowBalloonTip) :
                 delegate(int timeout, String tipTitle, String tipText, ToolTipIcon tipIcon) { /*nothing*/ };
        }
    }
}