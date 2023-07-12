namespace ExperimentEngine
{
    using System;
    using System.Timers;
    using LI7000Connection;


    public class ExperimentEngine
    {
        private List<Valve> valvesList;
        private Valve activeValve;
        private int msValveSwitchTime; // Milliseconds until valve switch
        private int msValveDataTime;    //Milliseconds until data is updated
        private Timer valveDataTimer;  //CHANGE TIMERS TO WINDOWS.FORMS.TIMER
        private Timer updateActiveValveTimer;
        private TextWriter outputFile;
        private LI7000Connection LI7000;
        private string LI7000DataHeader;

        public event EventHandler<DataUpdateEventArgs> DataUpdated;
        
        public List<Valve> ValvesList 
        { 
          get { return new List<Valve>(valvesList); }
          private set { valvesList = value; }
        }
        public Valve ActiveValve
        {
            get { return this.activeValve; }
            private set { this.activeValve = value; }
        }


        public ExperimentEngine(List<string> argActiveValves, int argMsValveDataTime, int argMsValveSwitchTime, Timer argValveDataTimer)
        {
            this.valvesList = new List<Valve>();
            this.LI7000 = new LI7000Connection();

            this.LI7000DataHeader = this.LI7000.DataHeader;

            string[] units = LI7000DataHeader.Split('\t');

            foreach (string valveName in argActiveValves)
            {
                Valve newValve = new Valve(valveName);

                for (int i = 0; i < units.Length; i++)
                {
                    if (units[i].Contains("CO2"))
                    {
                        units[i] = units[i].Substring(units[i].IndexOf(' ') + 1);
                        newValve.CO2Units = units[i];
                    }
                    else if (units[i].Contains("H2O"))
                    {
                        units[i] = units[i].Substring(units[i].IndexOf(' ') + 1);
                        newValve.H2OUnits = units[i];
                    }
                    else if (units[i].Contains('T'))
                    {
                        units[i] = units[i].Substring(units[i].IndexOf(' ') + 1);
                        newValve.TemperatureUnits = units[i];
                    }
                }
                this.valvesList.Add(newValve);
            }

            this.activeValve = valvesList[0];


            this.msValveSwitchTime = argMsValveSwitchTime;
            this.msValveDataTime = argMsValveDataTime;

            //Setup timers last
            this.valveDataTimer = argValveDataTimer;
            this.valveDataTimer.Interval = this.msValveDataTime;
            this.valveDataTimer.Elapsed += this.UpdateValveValue;
            this.valveDataTimer.AutoReset = true;
            //valveValueTimer.SynchronizingObject = argSynchronizingObject;
        }

        public void Start()
        {
            this.valveDataTimer.Enabled=true;
            return;
        }

        private void UpdateValveValue(Object source, ElapsedEventArgs e)
        {
            string? response = LI7000.Poll();
            string newData = string.Empty;

            if (response != null && response.Substring(0, 5) == "DATA\t")
            {
                response = response.Substring(5);
                response = response.Replace("\n", string.Empty);

                string[] headers = LI7000DataHeader.Split('\t');
                string[] data = response.Split('\t');
                for (int i = 0; i < headers.Length; i++)
                {
                    switch (headers[i][0])
                    {
                        case 'C':
                            this.activeValve.CO2 = double.Parse(data[i]);
                            break;
                        case 'H':
                            this.activeValve.H2O = double.Parse(data[i]);
                            break;
                        case 'T':
                            this.activeValve.Temperature = double.Parse(data[i]);
                            break;
                    }
                }
                this.DataUpdated.Invoke(this, new DataUpdateEventArgs(this.activeValve));
            }
        }

        public void Stop()
        {
            if (this.valveDataTimer != null)
            {
                this.valveDataTimer.Stop();
                this.valveDataTimer.Dispose();
                this.valveDataTimer = null;
            }
            if (this.updateActiveValveTimer != null)
            {
                this.updateActiveValveTimer.Stop();
                this.updateActiveValveTimer.Dispose();
                this.updateActiveValveTimer = null;
            }

            this.LI7000.CloseConnection();
            //Disconnect all devices
            //Close stream
        }



    }
}