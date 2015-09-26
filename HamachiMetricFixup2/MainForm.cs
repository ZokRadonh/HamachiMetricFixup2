using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Management;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Diagnostics;
using System.Linq;

namespace HamachiMetricFixup2
{
    public partial class MainForm : Form
    {

        public static MainForm Instance;

        private List<MetricJob> fixJobs = new List<MetricJob>();

        public MainForm()
        {
            InitializeComponent();
            Instance = this;

            // add shield if needed
            if (!IsAdmin())
            {
                AddShieldToButton(bFixit);
            }

            // load WMI objects and create wrapper objects
            LoadWMIObjects();
        }

        private void LoadWMIObjects()
        {

            radioAdapter.Tag = MetricFixer.Instance.Adapters;
            radioRoutingTable.Tag = MetricFixer.Instance.RoutingTable;

            // show adapters in listboxes
            cmbDevices.Items.Clear();
            listBox1.Items.Clear();
            cmbDevices.Items.AddRange(MetricFixer.Instance.Adapters.ToArray());
            if (radioRoutingTable.Checked) listBox1.Items.AddRange(MetricFixer.Instance.RoutingTable.ToArray());
            else listBox1.Items.AddRange(MetricFixer.Instance.Adapters.ToArray());
        }

        [DllImport("user32")]
        public static extern UInt32 SendMessage
            (IntPtr hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);

        internal const int BCM_FIRST = 0x1600; //Normal button

        internal const int BCM_SETSHIELD = (BCM_FIRST + 0x000C); //Elevated button

        static internal bool IsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal p = new WindowsPrincipal(id);
            return p.IsInRole(WindowsBuiltInRole.Administrator);
        }

        static internal void AddShieldToButton(Button b)
        {
            b.FlatStyle = FlatStyle.System;
            SendMessage(b.Handle, BCM_SETSHIELD, 0, 0xFFFFFFFF);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }



        private void cmbDevices_SelectedIndexChanged(object sender, EventArgs ea)
        {

            labTargetMetric.Text = MetricFixer.Instance.CreateFixJobs((NetworkAdapter)cmbDevices.SelectedItem, fixJobs).ToString();

            labRoutesToFix.Text = fixJobs.Count.ToString();
            labMaxMetric.Text = "?";

        }

        private void bFixit_Click(object sender, EventArgs e)
        {
            if (fixJobs.Count == 0)
            {
                MessageBox.Show(this, "Die Metriken scheinen so korrekt zu sein, um über " + cmbDevices.SelectedItem.ToString() + " zu spielen.\r\nKeine Metrik-Änderungen sind notwendig.", "Hamachi Metric Fixer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            bFixit.Enabled = false;
            if (!(cmbDevices.SelectedItem is NetworkAdapter))
            {
                MessageBox.Show(this, "Erst Netzwerkkarte zum spielen auswählen!", "Hamachi Metric Fixer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Action fix = new Action(() =>
            {
                if (IsAdmin())
                {
                    MetricFixer.Instance.ExecuteJobs(fixJobs);
                }
                else
                {
                    StartElevated("-execute " + CreateStringOfJobList(fixJobs));
                }
            });
            fix.BeginInvoke(new AsyncCallback(FixComplete), null);

        }

        private void FixComplete(IAsyncResult result)
        {
            if (result.IsCompleted)
            {
                Invoke(new Action(() =>
                {
                    int device = cmbDevices.SelectedIndex;
                    bFixit.Enabled = true;
                    MetricFixer.Instance.LoadWMIObjects();
                    LoadWMIObjects();
                    cmbDevices.SelectedIndex = device;
                    if (fixJobs.Count == 0) MessageBox.Show(this, "Metriken wurden erfolgreich gesetzt.", "Hamachi Metric Fixer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else MessageBox.Show(this, "Eine oder mehrere Routen konnten nicht hinreichend manipuliert werden.", "Hamachi Metric Fixer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            }
        }       

        public static string CreateStringOfJobList(List<MetricJob> jobs)
        {
            string str = "";
            foreach (var job in jobs)
            {
                str += job.ToString();
                str += " ";
            }
            return str.Trim();
        }

        public static List<MetricJob> CreateListOfStringJobs(string[] arguments)
        {
            List<MetricJob> jobs = new List<MetricJob>();
            foreach (var str in arguments)
            {

                string[] data = str.Split('=');
                int ifaceID, metric;
                if (!int.TryParse(data[0], out ifaceID)) {
                    MessageBox.Show("Ungültige Parameterübergabe. " + data[0] + " ist keine gültige Netzwerkkarten ID.", "Hamachi Metric Fixer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                if (!int.TryParse(data[1], out metric)) {
                    MessageBox.Show("Ungültige Parameterübergabe. " + data[1] + " ist keine gültige Zahl.", "Hamachi Metric Fixer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                jobs.Add(new MetricJob(ifaceID, metric));
            }
            return jobs;
        }



        private NetworkAdapter GetSaveManagementObject(object displayValue)
        {
            if (displayValue is DisplayValue)
            {
                if (((DisplayValue)displayValue).value is ManagementObject)
                {
                    return new NetworkAdapter((ManagementObject)((DisplayValue)displayValue).value);
                }
            }
            return null;
        }

        internal static void StartElevated(string cmdline)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Application.ExecutablePath;
            startInfo.Verb = "runas";
            startInfo.Arguments = cmdline;
            try
            {
                Process p = new Process();
                p.StartInfo = startInfo;
                p.Exited += new EventHandler(p_Exited);
                p.Start();
                p.WaitForExit();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                return;
            }
        }

        static void p_Exited(object sender, EventArgs e)
        {
        }

        public void setMetric(ManagementObject adapter, int metric)
        {
            try
            {
                ManagementBaseObject setGateway;
                ManagementBaseObject newGateway = adapter.GetMethodParameters("SetGateways");
                newGateway["DefaultIPGateway"] = new string[] { adapter.GetPropertyValue("DefaultIPGateway").ToString() };
                newGateway["GatewayCostMetric"] = new int[] { metric };
                setGateway = adapter.InvokeMethod("SetGateways", newGateway,  null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void setGateway(string gateway)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();
            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    try
                    {
                        ManagementBaseObject setGateway;
                        ManagementBaseObject newGateway =
                            objMO.GetMethodParameters("SetGateways");
                        newGateway["DefaultIPGateway"] = new string[] { gateway };
                        newGateway["GatewayCostMetric"] = new int[] { 40 };
                        setGateway = objMO.InvokeMethod("SetGateways", newGateway, null);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        } 

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            if (!(listBox1.SelectedItem is IManagementWrapper)) return;
            IManagementWrapper adapter = (IManagementWrapper)listBox1.SelectedItem;
            foreach (PropertyData prop in adapter.ManagementObj.Properties)
            {
                listBox2.Items.Add(new DisplayValue(prop, prop.Name));
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            if (!(listBox2.SelectedItem is DisplayValue)) return;
            DisplayValue dv = (DisplayValue)listBox2.SelectedItem;
            object value = ((PropertyData)dv.value).Value;
            if (value == null)
            {
                listBox3.Items.Add("null");
            }
            else if (value is Array)
            {
                foreach (var a in (IEnumerable)value)
                {
                    listBox3.Items.Add(a.ToString());
                }
            }
            else
            {
                listBox3.Items.Add(value.ToString());
            }
        }

        private void debugListboxSwitch(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                object info = ((RadioButton)sender).Tag;
                listBox3.Items.Clear();
                listBox2.Items.Clear();
                listBox1.Items.Clear();

                foreach (var obj in (IEnumerable)info)
                {
                    listBox1.Items.Add(obj);
                }
            }
        }

        private void MainForm_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show(this, "Gesetzte Metriken verfallen nach einem Computer-Neustart.\r\nManche Spiele erfordern zusätzlich, dass Hamachi als erster Netzwerkadapter in der Reihenfolge ist. Diese Einstellung kann HMF nicht vornehmen.", "Hamachi Metric Fixer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }


    public class DisplayValue
    {
        public object value;
        public string display = null;

        public DisplayValue(object o)
        {
            value = o;
        }

        public DisplayValue(object o, string str)
        {
            value = o;
            display = str;
        }

        public override string ToString()
        {
            if (display == null)
            {
                if (value is ManagementObject)
                {
                    return ((ManagementObject)value).GetPropertyValue("Description").ToString();
                }
                return "?";
            }
            else
            {
                return display;
            }
        }
    }
}