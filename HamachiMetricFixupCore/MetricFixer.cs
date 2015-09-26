using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Windows.Forms;

namespace HamachiMetricFixup2
{
    public class MetricFixer
    {
        public const string ROUTE_TO_FIX = "255.255.255.255";

        public List<NetworkAdapter> Adapters { get; private set; }

        public List<RoutingEntry> RoutingTable { get; private set; }

        private static MetricFixer _instance;
        public static MetricFixer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MetricFixer();
                }
                return _instance;
            }
        }

        public MetricFixer()
        {
            Adapters = new List<NetworkAdapter>();
            RoutingTable = new List<RoutingEntry>();
            LoadWMIObjects();
        }


        /// <summary>
        /// Is called in constructor
        /// </summary>
        public void LoadWMIObjects()
        {
            RoutingTable.Clear();
            // fetch routing tables from system
            ManagementClass mcRouting = new ManagementClass("Win32_IP4RouteTable");
            ManagementObjectCollection moTables = mcRouting.GetInstances();
            foreach (ManagementObject obj in moTables)
            {
                RoutingEntry entry = new RoutingEntry(obj);
                RoutingTable.Add(entry);
            }

            Adapters.Clear();
            // fetch adapters from system
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection coll = objMC.GetInstances();
            foreach (ManagementObject obj in coll)
            {
                if (!(bool)obj["IPEnabled"]) continue;
                NetworkAdapter adapter = new NetworkAdapter(obj);
                Adapters.Add(adapter);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameAdapter"></param>
        /// <returns>Returns target metric</returns>
        public int CreateFixJobs(NetworkAdapter gameAdapter, List<MetricJob> fixJobs)
        {

            //= routingTable.Find((e) => e.InterfaceIndex == gameAdapter.InterfaceNumber && e.Destination == ROUTE_TO_FIX && e.Mask == ROUTE_TO_FIX);
            //RoutingEntry gameRouting = (from e in routingTable
            //                           where e.InterfaceIndex == gameAdapter.InterfaceNumber &&
            //                           e.Destination == ROUTE_TO_FIX &&
            //                           e.Mask == ROUTE_TO_FIX
            //                           select e).FirstOrDefault();

            int targetMetric = GetMetricOfAdapter(gameAdapter) + 10;

            int maxMetric = 0;

            fixJobs.Clear();

            foreach (var route in from r in MetricFixer.Instance.RoutingTable where r.Destination == MetricFixer.ROUTE_TO_FIX && r.Mask == MetricFixer.ROUTE_TO_FIX select r)
            {
                if (route.Metric > maxMetric) maxMetric = route.Metric;
                if (route.InterfaceIndex != gameAdapter.InterfaceNumber)
                {
                    if (route.Metric < targetMetric) fixJobs.Add(new MetricJob(route.InterfaceIndex, targetMetric));
                }
            }

            return targetMetric;
        }

        private int GetMetricOfAdapter(NetworkAdapter adapter)
        {
            return (from e in MetricFixer.Instance.RoutingTable
                    where e.InterfaceIndex == adapter.InterfaceNumber &&
                    e.Destination == MetricFixer.ROUTE_TO_FIX &&
                    e.Mask == MetricFixer.ROUTE_TO_FIX
                    select e.Metric).FirstOrDefault();
        }

        public void ExecuteJobs(List<MetricJob> jobs)
        {
            foreach (var job in jobs)
            {
                RoutingEntry entry = MetricFixer.Instance.RoutingTable.Find((r) => r.InterfaceIndex == job.InterfaceID && r.Destination == MetricFixer.ROUTE_TO_FIX && r.Mask == MetricFixer.ROUTE_TO_FIX);
                if (entry != null)
                {
                    entry.Metric = job.TargetMetric;
                    entry.Save();
                }
                else
                    MessageBox.Show("Keine " + MetricFixer.ROUTE_TO_FIX + " Route für Netzwerkkarte gefunden. ID: " + job.InterfaceID, "Hamachi Metric Fixer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
