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
        private static List<SiteBlock> SiteBlocks = new List<SiteBlock>();

        public delegate void AddSiteToTableDelegate(SiteBlock Value);
        public delegate void RemoveSiteFromTableDelegate(SiteBlock Value);
        private delegate void UpdateTrayIconTextDelegate();
        public delegate void ShowBalloonTipDelegate(int timeout, String tipTitle, String tipText, ToolTipIcon tipIcon);

        public static AddSiteToTableDelegate AddSiteToTable;
        public static RemoveSiteFromTableDelegate RemoveSiteFromTable;
        private static UpdateTrayIconTextDelegate UpdateTrayIconText;
        private static ShowBalloonTipDelegate ShowBalloonTip;

        public SiteBlockerForm()
        {
            InitializeComponent();
            Settings.Default.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Default_PropertyChanged);

            AddSiteToTable = AddSiteToTableMethod;
            RemoveSiteFromTable = RemoveSiteFromTableMethod;
            UpdateTrayIconText = UpdateTrayIconTextMethod;
            UpdateNotifyAvailable();

            ReadBlockList();
        }

        /// <summary>
        /// Reads the hosts file and attempts to parse each line as a SiteBlock, adding it to the SiteBlocks list.
        /// </summary>
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
                    SiteBlock NewSiteBlock = SiteBlock.Parse(CurrentLine);
                    SiteBlocks.Add(NewSiteBlock);
                    AddSiteToTable(NewSiteBlock);
                }
                catch
                {
                    // line not parsed
                }
            }
        }

        /// <summary>
        /// Writes the SiteBlocks list in the proper format into the hosts file.
        /// </summary>
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

        /// <summary>
        /// Flushes Window's DNS Cache.
        /// </summary>
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

        /// <summary>
        /// Unblocks TargetSite and removes it from the UI.
        /// </summary>
        public static void Unblock(SiteBlock TargetSite)
        {
            SiteBlocks.Remove(TargetSite);
            RemoveSiteFromTable(TargetSite);
            WriteBlockList();
            ShowBalloonTip(0, "", TargetSite.Name() + " is available", ToolTipIcon.None);
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
            SiteBlock NewSiteBlock = new TimedSiteBlock(SiteTextBox.Text, new IPAddress(new byte[] { 0, 0, 0, 0 }), EndTimeSpinner.Value);
            SiteBlocks.Add(NewSiteBlock);
            AddSiteToTable(NewSiteBlock);
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