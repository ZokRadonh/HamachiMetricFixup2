using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace HamachiMetricFixup2
{
    public class MetricFixer
    {
        private const string RouteToFix = "255.255.255.255";

       

        public List<RoutingEntry> RoutingTable { get; } = new List<RoutingEntry>();

        private static MetricFixer _instance;
        public static MetricFixer Instance => _instance ?? (_instance = new MetricFixer());

        private MetricFixer()
        {
            LoadWmiObjects();
        }

        /// <summary>
        /// Is called in constructor
        /// </summary>
        public void LoadWmiObjects()
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
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameAdapter"></param>
        /// <returns>Returns target metric</returns>
        public static int CreateFixJobs(int gameAdapter, List<MetricJob> fixJobs)
        {

            //= routingTable.Find((e) => e.InterfaceIndex == gameAdapter.InterfaceNumber && e.Destination == RouteToFix && e.Mask == RouteToFix);
            //RoutingEntry gameRouting = (from e in routingTable
            //                           where e.InterfaceIndex == gameAdapter.InterfaceNumber &&
            //                           e.Destination == RouteToFix &&
            //                           e.Mask == RouteToFix
            //                           select e).FirstOrDefault();

            int targetMetric = GetMetricOfAdapter(gameAdapter) + 10;

            int maxMetric = 0;

            fixJobs.Clear();

            foreach (var route in from r in Instance.RoutingTable where r.Destination == RouteToFix && r.Mask == RouteToFix select r)
            {
                if (route.Metric > maxMetric) maxMetric = route.Metric;
                if (route.InterfaceIndex != gameAdapter)
                {
                    if (route.Metric < targetMetric) fixJobs.Add(new MetricJob(route.InterfaceIndex, targetMetric));
                }
            }

            return targetMetric;
        }

        private static int GetMetricOfAdapter(int adapter)
        {
            return (from e in Instance.RoutingTable
                    where e.InterfaceIndex == adapter &&
                    e.Destination == RouteToFix &&
                    e.Mask == RouteToFix
                    select e.Metric).FirstOrDefault();
        }

        public static void ExecuteJobs(IEnumerable<MetricJob> jobs)
        {
            foreach (var job in jobs)
            {
                RoutingEntry entry = Instance.RoutingTable.Find(r => r.InterfaceIndex == job.InterfaceID && r.Destination == RouteToFix && r.Mask == RouteToFix);
                if (entry != null)
                {
                    entry.Metric = job.TargetMetric;
                    entry.Save();
                }
                else
                    MessageBox.Show("Keine " + RouteToFix + " Route für Netzwerkkarte gefunden. ID: " + job.InterfaceID, "Hamachi Metric Fixer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
