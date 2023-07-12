using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentEngine
{
    public class DataUpdateEventArgs:EventArgs
    {
        public Valve ActiveValve { get; private set; }

        internal DataUpdateEventArgs(Valve argActiveValve)
        {
            ActiveValve = argActiveValve;
        }
    }
}
