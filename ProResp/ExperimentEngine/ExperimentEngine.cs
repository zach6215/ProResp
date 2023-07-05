namespace ExperimentEngine
{
    using System;
    using System.Timers;
    using LI7000Connection;
    using System.ComponentModel;

    public class ExperimentEngine:INotifyPropertyChanged
    {
        private List<Valve> activeValvesList;
        private int msValveSwitch; // Milliseconds until valve switch
        private Valve activeValve;
        private Timer valveValueTimer;
        private Timer updateActiveValveTimer;
        private TextWriter outputFile;
        private LI7000Connection LI7000;
        public event PropertyChangedEventHandler PropertyChanged;
        

        public ExperimentEngine(List<string> argActiveValves, int argMsValveSwitch)
        {
            this.activeValvesList = new List<Valve>();
            foreach(string valveName in argActiveValves)
            {
                this.activeValvesList.Add(new Valve(valveName));
            }

            this.msValveSwitch = argMsValveSwitch;

            valveValueTimer = new Timer(this.msValveSwitch);
            valveValueTimer.Elapsed += this.UpdateValveValue;
            valveValueTimer.AutoReset = true;
            valveValueTimer.Enabled = true;

            LI7000 = new LI7000Connection();
        }

        private void UpdateValveValue(Object source, ElapsedEventArgs e)
        {

        }

        public void Stop()
        {
            //Disconnect all devices
            //Close stream
        }



    }
}