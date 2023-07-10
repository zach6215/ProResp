namespace ProResp
{
    using ExperimentEngine;
    using System.Timers;
    using System.ComponentModel;
    public partial class Form1 : Form
    {
        const int numOfValves = 24;
        const int msValveSwitchTime = 900000; //15 mins
        const int msValveDataUpdateTime = 5000;
        Timer valveDataTimer;
        ExperimentEngine? experimentEngine = null;

        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < numOfValves; i++)
            {
                valveCheckedListBox1.Items.Add("Valve " + (i+1).ToString());
            }

            valveCheckedListBox1.CheckOnClick = true;
        }

        private void CheckAllValves_Button_Click(object sender, EventArgs e)
        {

        }

        private void StartNewExperiment_Button_Click(object sender, EventArgs e)
        {
            List<string> checkedValves = new List<string>();

            if (experimentEngine != null)
            {
                MessageBox.Show("There is already an experiment running! Please stop this experiment before starting another.", "Error: An Experiment Is Already Running", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach(string checkedItem in valveCheckedListBox1.CheckedItems)
            {
                checkedValves.Add(checkedItem);
            }


            this.valveDataTimer = new Timer();
            this.valveDataTimer.SynchronizingObject = this;
            experimentEngine = new ExperimentEngine(checkedValves, msValveDataUpdateTime, msValveSwitchTime, this.valveDataTimer);

            //foreach(Valve valve in experimentEngine.ValvesList)
            //{
            //    valve.PropertyChanged += this.ValveDataUpdated;
            //}
            

            //Future: disable checkboxlist so current experiment values are shown and can't be changed.
            //One option is to use OnClick event and just undo a check and display error message.

            //Lock start experiment and check valves buttons
            //Unlock stop button

            experimentEngine.Start();

        }

        private void ValveDataUpdated(object sender, PropertyChangedEventArgs e)
        {
            //Valve valveSender;

            //if (sender.GetType() == typeof(Valve))
            //{
            //    valveSender = (Valve)sender;

            //    string[] data = valveSender.Data.Split("|");

            //    for (int i = 0; i < data.Length; i++)
            //    {
            //        switch (data[i][0])
            //        {
            //            case 'C':

            //                this.CurrentCO2_Label.Text = "Current " + data;
            //                break;
            //            case 'H':
            //                this.CurrentH2O_Label.Text = "Current " + data;
            //                break;
            //            case 'T':
            //                this.CurrentTemp_Label.Text = "Current " + data;
            //                break;
            //        }
            //    }
            //}
        }

        private void Stop_Button_Click(object sender, EventArgs e)
        {
            //Call ExperimentEngine.Stop
            //Free ExperimentEngine
        }
    }
}