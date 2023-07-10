namespace ExperimentEngine
{
    using System.ComponentModel;
    public class Valve
    {
        public string Name
        {
            get;
            private set; 
        }

        public double CO2
        {
            get;
            internal set;
        }

        public double H2O
        {
            get;
            internal set;
        }

        public double Temperature
        {
            get;
            internal set;
        }

        public double Flow
        {
            get;
            internal set;
        }

        internal Valve(string argName)
        {
            this.Name = argName;
        }
    }
}
