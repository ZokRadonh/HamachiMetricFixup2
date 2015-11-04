using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace HamachiMetricFixup2
{
    class NetworkAdapter : HamachiMetricFixup2.IManagementWrapper
    {
        private ManagementObject _mo;

        public NetworkAdapter(ManagementObject managementObject)
        {
            _mo = managementObject;
        }

        public int ConnectionMetric
        {
            get
            {
                uint o = (uint)_mo["IPConnectionMetric"];
                return (int)o;
            }
        }

        public int GatewayMetric
        {
            get
            {
                ushort[] gatewaycosts = (ushort[])_mo["GatewayCostMetric"];
                if (gatewaycosts == null) return 0;
                return (int)gatewaycosts[0];
            }
        }

        public string Name
        {
            get
            {
                return (string)_mo["Description"];
            }
        }

        public string Gateway
        {
            get
            {
                string[] o = (string[])_mo["DefaultIPGateway"];
                return o[0];
            }
        }

        public int InterfaceNumber
        {
            get
            {
                uint o = (uint)_mo["InterfaceIndex"];
                return (int)o;
            }
        }

        public bool SetMetric(int newTargetMetric)
        {
            int gateway = newTargetMetric - ConnectionMetric;
            if (gateway < 0) return false;

            try
            {
                ManagementBaseObject setGateway;
                ManagementBaseObject newGateway =
                    _mo.GetMethodParameters("SetGateways");
                newGateway["DefaultIPGateway"] = new string[] { Gateway }; // TODO: what if this adapter has multiple gateways configured?
                newGateway["GatewayCostMetric"] = new int[] { gateway };

                setGateway = _mo.InvokeMethod("SetGateways", newGateway, null);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
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
            return Name;
        }
    }
}
