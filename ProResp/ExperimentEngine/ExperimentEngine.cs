﻿namespace ExperimentEngine
{
    using System;
    using System.Timers;
    using System.Collections.Generic;
    using LI7000Connection;


    public class ExperimentEngine
    {
        private LinkedList<Valve> valvesList;
        private LinkedListNode<Valve> currentNode;
        private int msValveSwitchTime; // Milliseconds until valve switch
        private int msValveDataTime;    //Milliseconds until data is updated
        private Timer? valveDataTimer;
        private Timer? valveSwitchTimer;
        private LI7000Connection LI7000;
        private string lI7000DataHeader;
        private string dateTimeHeader;
        private DateTime startDateTime;

        public event EventHandler<DataUpdateEventArgs> DataUpdated;
        public event EventHandler<DataUpdateEventArgs> ValveSwitched;


        public string DateTimeHeader { get { return dateTimeHeader; } private set { dateTimeHeader = value; } }
        public string LI7000DataHeader { get { return lI7000DataHeader; } private set { lI7000DataHeader = value; } }
        public double DaysSinceStart { get { return (DateTime.Now.Date - startDateTime.Date).TotalDays; } }

        //Abhi: This constructor can be used to create experiment for Check Valves Button
        public ExperimentEngine(List<string> argActiveValves, int argMsValveSwitchTime, Timer argValveSwitchTimer)
        {

        }

        public ExperimentEngine(List<int> argActiveValves, int argMsValveDataTime, int argMsValveSwitchTime, Timer argValveDataTimer, Timer argValveSwitchTimer)
        {
            this.valvesList = new LinkedList<Valve>();
            this.LI7000 = new LI7000Connection();

            this.LI7000DataHeader = this.LI7000.DataHeader;
            this.DateTimeHeader = "Date (mm/dd/yyyy) \t Time (hh:mm)";

            string[] units = LI7000DataHeader.Split('\t');

            foreach (int valveNum in argActiveValves)
            {
                Valve newValve = new Valve(valveNum);

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
                LinkedListNode<Valve> newNode = new LinkedListNode<Valve>(newValve); 
                this.valvesList.AddLast(newNode);
            }

            this.currentNode = valvesList.First;


            this.msValveSwitchTime = argMsValveSwitchTime;
            this.msValveDataTime = argMsValveDataTime;

            //Setup timers last
            this.valveDataTimer = argValveDataTimer;
            this.valveDataTimer.Interval = this.msValveDataTime;
            this.valveDataTimer.Elapsed += this.UpdateValveValue;
            this.valveDataTimer.AutoReset = true;
            this.valveSwitchTimer = argValveSwitchTimer;
            this.valveSwitchTimer.Interval = this.msValveSwitchTime;
            this.valveSwitchTimer.Elapsed += this.SwitchValves;
            this.valveSwitchTimer.AutoReset = true;
        }

        public void Start()
        {
            this.startDateTime = DateTime.Now;
            if (this.valveDataTimer != null)
            {
                this.valveDataTimer.Enabled = true;
            }
            if (this.valveSwitchTimer != null)
            {
                this.valveSwitchTimer.Enabled = true;
            }
            return;
        }

        private void UpdateValveValue(Object source, ElapsedEventArgs e)
        {
            this.currentNode.Value.MeasurementDateTime = DateTime.Now;
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
                            this.currentNode.Value.CO2 = double.Parse(data[i]);
                            break;
                        case 'H':
                            this.currentNode.Value.H2O = double.Parse(data[i]);
                            break;
                        case 'T':
                            this.currentNode.Value.Temperature = double.Parse(data[i]);
                            break;
                    }
                }
                this.DataUpdated.Invoke(this, new DataUpdateEventArgs(this.currentNode.Value));
            }
        }

        //Abhi: Add code to switch valves here. In the end it should invoke this.ValveSwitched event.
        private void SwitchValves(Object source, ElapsedEventArgs e)
        {
        }

        public void Stop()
        {
            if (this.valveDataTimer != null)
            {
                this.valveDataTimer.Stop();
                this.valveDataTimer.Dispose();
                this.valveDataTimer = null;
            }
            if (this.valveSwitchTimer != null)
            {
                this.valveSwitchTimer.Stop();
                this.valveSwitchTimer.Dispose();
                this.valveSwitchTimer = null;
            }

            if (this.LI7000 != null)
            {
                this.LI7000.CloseConnection();
            }
            //Disconnect all devices
            //Close stream
        }
    }
}