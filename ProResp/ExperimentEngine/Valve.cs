namespace ExperimentEngine
{
    public class Valve
    {
        //public string Name { get; private set; }

        public int ValveNum { get; private set; }
        public DateTime MeasurementDateTime { get; internal set; }

        public double CO2 { get; internal set; }
        public string CO2Units { get; internal set; }

        public double H2O { get; internal set; }
        public string H2OUnits { get; internal set; }

        public double Temperature { get; internal set; }
        public string TemperatureUnits { get; internal set; }

        public double Flow { get; internal set; }
        public string FlowUnits { get; internal set; }

        internal Valve(int argValveNum)
        {
            this.ValveNum = argValveNum;
        }

        internal Valve(int argValveNum, double argCO2, double argH2O, double argTemp, double argFlow)
        {
            this.ValveNum = argValveNum;
            this.CO2 = argCO2;
            this.H2O = argH2O;
            this.Temperature = argTemp;
            this.Flow = argFlow;
        }
    }
}
