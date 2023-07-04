using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentEngine
{
    internal class Valve
    {
        private string name;
        private double CO2;
        private double H20;
        private double temp;
        
        internal Valve(string argName)
        {
            name = argName;
        }

        internal void Update()
        {

        }
    }
}
