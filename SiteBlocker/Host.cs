using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SiteBlocker
{
    /// <summary>
    /// A representation of the hosts file and its members.
    /// </summary>
    public class Host
    {
        #region static members

        private static readonly string HOSTS_FILE = Environment.GetEnvironmentVariable("windir") + @"\System32\drivers\etc\hosts";
        private static readonly List<Host> ENTRIES = ParseHostsFile();
        public static Func<Host, Control[]> AddToUI;
        public static Action<Control[]> RemoveFromUI;

        public static void ViewHostsFile()
        {
            // TODO: Read the entries from file when it exits. Reset the UI?
            new Process() {
                StartInfo = new ProcessStartInfo() {
                    FileName = Environment.GetEnvironmentVariable("windir") + @"\System32\notepad",
                    Arguments = HOSTS_FILE,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true
                }
            }.Start();
        }

        private static List<Host> ParseHostsFile()
        {
            var result = new List<Host>();
            using (var r = new StreamReader(HOSTS_FILE)) {
                foreach (Match m in Regex.Matches(r.ReadToEnd(), @"^\s*[^#](?<ip>.+)\s+(?<host>.+)\s*(?:#(?<date>.*))?$")) {
                    if (m.Success) {
                        DateTime date;
                        if (DateTime.TryParse(m.Groups["date"].Value, out date)) {
                            result.Add(new Host(m.Groups["host"].Value, m.Groups["ip"].Value, date));
                        } else {
                            result.Add(new Host(m.Groups["host"].Value, m.Groups["ip"].Value));
                        }
                    }
                }
            }
            return result;
        }

        private static void WriteHostsFile()
        {
            FlushDNS();
            using (var w = new StreamWriter(HOSTS_FILE)) {
                foreach (var e in ENTRIES) {
                    w.WriteLine(e);
                }
            }
        }

        private static void FlushDNS()
        {
            var p = new Process() {
                StartInfo = new ProcessStartInfo() {
                    FileName = "ipconfig",
                    Arguments = "/flushdns",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true
                }
            };
            p.Start();
            p.WaitForExit();
        }
        #endregion
        
        #region dynamic members

        public readonly string HOST;
        public readonly string REDIRECT;
        public readonly DateTime? END_TIME;
        private Control[] controls;

        public Host(string host, string redirect)
        {
            this.HOST = host;
            this.REDIRECT = redirect;
        }

        public Host(string host, string redirect, DateTime endTime)
        {
            this.HOST = host;
            this.REDIRECT = redirect;
            this.END_TIME = endTime;
        }

        public void Block()
        {
            ENTRIES.Add(this);
            WriteHostsFile();
            controls = AddToUI(this);
        }

        public void Unblock()
        {
            if (ENTRIES.Remove(this)) {
                WriteHostsFile();
                RemoveFromUI(this.controls);
            }
        }

        public override string ToString()
        {
            return REDIRECT + "\t" + HOST + ((END_TIME.HasValue) ? "#" + END_TIME.Value.ToString() : "");
        }
        #endregion
    }
}
