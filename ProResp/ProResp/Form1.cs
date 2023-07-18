namespace ProResp
{
    using ExperimentEngine;
    using System.Timers;
    public partial class Form1 : Form
    {
        const int numOfValves = 24;
        const int msValveSwitchTime = 900000; //15 mins
        const int msValveDataUpdateTime = 5000;
        string filePath;
        StreamWriter outputStream;
        Timer valveDataTimer;
        ExperimentEngine? experimentEngine = null;

        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < numOfValves; i++)
            {
                this.valveCheckedListBox1.Items.Add("Valve " + (i+1).ToString());
            }

            this.CurrentFlow_Label.Hide();

            this.valveCheckedListBox1.CheckOnClick = true;

            this.Stop_Button.Enabled = false;
        }

        //Abhi: This should just itterate through all valves. 
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

            if (checkedValves.Count < 1)
            {
                MessageBox.Show("No valves selected! Please select valve(s)");
            }

            this.valveDataTimer = new Timer();
            this.valveDataTimer.SynchronizingObject = this;

            try
            {
                experimentEngine = new ExperimentEngine(checkedValves, msValveDataUpdateTime, msValveSwitchTime, this.valveDataTimer);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }

            this.experimentEngine.DataUpdated += ValveDataUpdated;

            this.valveCheckedListBox1.Enabled = false;
            this.StartNewExperiment_Button.Enabled = false;
            this.CheckAllValves_Button.Enabled = false;
            this.SelectAllValves.Enabled = false;
            this.Stop_Button.Enabled = true;

            experimentEngine.Start();
        }

        private void ValveDataUpdated(object sender, DataUpdateEventArgs e)
        {
            this.ActiveChamber_Label.Text = "Active Valve: " + e.ActiveValve.Name;
            this.CurrentCO2_Label.Text = "Current CO2: " + e.ActiveValve.CO2.ToString() + ' ' + e.ActiveValve.CO2Units;
            this.CurrentH2O_Label.Text = "Current H2O: " + e.ActiveValve.H2O.ToString() + ' ' + e.ActiveValve.H2OUnits;
            this.CurrentTemp_Label.Text = "Current Temperature: " + e.ActiveValve.Temperature.ToString() + ' ' + e.ActiveValve.TemperatureUnits;
            this.CurrentFlow_Label.Text = "Current Flow: " + e.ActiveValve.Flow.ToString();
        }

        private void SelectAllValves_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.valveCheckedListBox1.Items.Count; i++)
            {
                this.valveCheckedListBox1.SetItemChecked(i, true);
            }
        }

        private void Stop_Button_Click(object sender, EventArgs e)
        {
            //Close connections in experimentEngine and Stop/Dispose timers
            this.experimentEngine.Stop();

            this.experimentEngine = null;

            //Enable ValveCheckedListBox, Start experiment button, CheckAllValves button,
            //SelectAllValves button, and CreateSaveFile button.
            this.valveCheckedListBox1.Enabled = true;
            this.StartNewExperiment_Button.Enabled = true;
            this.CheckAllValves_Button.Enabled = true;
            this.SelectAllValves.Enabled = true;
            this.CreateSaveFile_Button.Enabled = true;

            //Reset labels
            this.CurrentFileLocation_Label.Text = "Current File Location: ";

            //Disable Stop button.
            this.Stop_Button.Enabled = false;
        }

        private void CreateSaveFile_Button_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt";

            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.filePath = saveFileDialog1.FileName;
                this.CurrentFileLocation_Label.Text = "Current File Location: " + this.filePath;

            }
        }
    }
}