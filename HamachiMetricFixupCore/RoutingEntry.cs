using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace HamachiMetricFixup2
{
    public class RoutingEntry : IManagementWrapper
    {
        private ManagementObject _mo;

        public RoutingEntry(ManagementObject mo)
        {
            _mo = mo;
        }

        public string Caption
        {
            get
            {
                return (string)_mo["Caption"];
            }
        }

        public string Destination { get { return (string)_mo["Destination"]; } set { _mo["Destination"] = value; } }

        public int InterfaceIndex
        {
            get { return (int)_mo["InterfaceIndex"]; }
            set { _mo["InterfaceIndex"] = value; }
        }

        public NetworkAdapter Interface
        {
            get { 
                return MetricFixer.Instance.Adapters.Find((a) => a.InterfaceNumber == InterfaceIndex);
            }
        }

        public string Mask
        {
            get { return (string)_mo["Mask"]; }
            set { _mo["Mask"] = value; }
        }

        public int Metric
        {
            get { return (int)_mo["Metric1"]; }
            set { _mo["Metric1"] = value; }
        }

        public string NextHop
        {
            get { return (string)_mo["NextHop"]; }
            set { _mo["NextHop"] = value; }
        }

        public string Status
        {
            get { return (string)_mo["Status"]; }
        }

        public ManagementObject ManagementObj
        {
            get
            {
                return _mo;
            }
        }

        public override string ToString()
        {
            if (Interface == null) return InterfaceIndex + "->" + Caption;
            else return Interface.Name + "->" + Caption;
        }

        public void Save()
        {
            _mo.Put();
        }
    }
}
