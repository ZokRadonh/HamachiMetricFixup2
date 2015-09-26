using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HamachiMetricFixup2
{
    public class MetricJob
    {
        private int _interfaceID;
        private int _metric;

        public MetricJob(int iface, int metric)
        {
            _interfaceID = iface;
            _metric = metric;
        }

        public int InterfaceID
        {
            get { return _interfaceID; }
        }

        public int TargetMetric
        {
            get { return _metric; }
        }

        public override string ToString()
        {
            return _interfaceID.ToString() + "=" + _metric.ToString();
        }
    }
}
