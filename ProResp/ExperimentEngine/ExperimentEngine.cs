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

        //public event EventHandler<>
        
        public List<Valve> ValvesList 
        { 
          get { return new List<Valve>(valvesList); }
          private set { valvesList = value; }
        }
        //public Valve ActiveValve
        //{
        //    get { return new Valve(this.activeValve.Name, this.activeValve.Data); }
        //    private set { this.activeValve = value; }
        //}


        public ExperimentEngine(List<string> argActiveValves, int argMsValveDataTime, int argMsValveSwitchTime, Timer argValveDataTimer)
        {
            this.valvesList = new List<Valve>();
            foreach(string valveName in argActiveValves)
            {
                this.valvesList.Add(new Valve(valveName));
            }

            this.activeValve = valvesList[0];

            this.LI7000 = new LI7000Connection();

            this.LI7000DataHeader = this.LI7000.DataHeader;

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
            }
        }

        public void Stop()
        {
            //Disconnect all devices
            //Close stream
        }



    }
}