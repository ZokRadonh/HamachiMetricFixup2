using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HamachiMetricFixup2
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm form = new MainForm();
            if (args.Length > 0 && args[0] == "-execute") {
                if (!MainForm.IsAdmin())
                {
                    MessageBox.Show("Operation ohne Administratorrechten nicht möglich.");
                }
                else
                {
                    string[] jobs = new string[args.Length - 1];
                    Array.Copy(args, 1, jobs, 0, args.Length - 1);
                    MetricFixer.Instance.ExecuteJobs(MainForm.CreateListOfStringJobs(jobs));
                    return;
                }
            }
            Application.Run(form);
        }
    }
}