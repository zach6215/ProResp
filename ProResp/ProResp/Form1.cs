namespace ProResp
{
    using ExperimentEngine;
    using System.Timers;
    public partial class Form1 : Form
    {
        const int numOfValves = 24;
        const int msValveSwitchTime = 900000; //15 mins
        const int msValveDataUpdateTime = 5000;
        string? filePath;
        StreamWriter outputStream;
        Timer valveDataTimer;
        Timer valveSwitchTimer;
        ExperimentEngine? experimentEngine = null;

        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < numOfValves; i++)
            {
                //Add items to valve checked list box
                this.valveCheckedListBox1.Items.Add("Valve " + (i+1).ToString());

                //Add labels to valve weight group box
                Label newValveWeight_Label = new System.Windows.Forms.Label();
                newValveWeight_Label.Location = new System.Drawing.Point(30 + ((i/8)*300), 65 * ((i%8) + 1));
                newValveWeight_Label.Name = "WeightValve" + (i + 1) + "_Label";
                newValveWeight_Label.Size = new System.Drawing.Size(110, 32);
                newValveWeight_Label.TabIndex = i;
                newValveWeight_Label.Text = "Valve " + (i + 1) + ":";
                this.valveWeightGroupBox.Controls.Add(newValveWeight_Label);

                //Add textboxes to valve weight group box
                TextBox newValveWeight_TextBox = new System.Windows.Forms.TextBox();
                newValveWeight_TextBox.Location = new System.Drawing.Point(140 + ((i/8)*300), 65 * ((i%8) + 1));
                newValveWeight_TextBox.Name = "WeightValve" + (i + 1) + "_TextBox";
                newValveWeight_TextBox.Size = new System.Drawing.Size(175, 39);
                newValveWeight_TextBox.TabIndex = i + 1;
                newValveWeight_TextBox.Enabled = false;
                this.valveWeightGroupBox.Controls.Add(newValveWeight_TextBox);
            }

            this.valveCheckedListBox1.CheckOnClick = true;

            this.experimentGroupBox.Hide();

            this.Stop_Button.Enabled = false;
        }

        //Abhi: Create new experiment using constructor with less arguments. 
        private void CheckAllValves_Button_Click(object sender, EventArgs e)
        {

        }

        private void StartNewExperiment_Button_Click(object sender, EventArgs e)
        {
            List<int> checkedValves = new List<int>();

            if (experimentEngine != null)
            {
                MessageBox.Show("There is already an experiment running! Please stop this experiment before starting another.", "Error: An Experiment Is Already Running", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.filePath == null)
            {
                MessageBox.Show("No file selected! Please select a file to store data.");
                return;
            }
            else
            {
                this.outputStream = new StreamWriter(this.filePath);
            }

            foreach(string checkedItem in valveCheckedListBox1.CheckedItems)
            {
                checkedValves.Add(int.Parse(checkedItem.Replace("Valve ", string.Empty)));
            }

            if (checkedValves.Count < 1)
            {
                MessageBox.Show("No valves selected! Please select valve(s).");
            }

            this.valveDataTimer = new Timer();
            this.valveDataTimer.SynchronizingObject = this;

            this.valveSwitchTimer = new Timer();
            this.valveSwitchTimer.SynchronizingObject = this;

            try
            {
                experimentEngine = new ExperimentEngine(checkedValves, msValveDataUpdateTime, msValveSwitchTime, this.valveDataTimer, this.valveSwitchTimer);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }

            this.experimentEngine.DataUpdated += ValveDataUpdated;
            this.experimentEngine.ValveSwitched += ValvesSwitched;

            this.WriteDataHeader();

            this.FormSetupExperimentRunning();

            this.experimentEngine.Start();
        }

        private void ValveDataUpdated(object sender, DataUpdateEventArgs e)
        {
            this.ActiveChamber_Label.Text = "Active Valve: " + e.ActiveValve.ValveNum;
            this.CurrentCO2_Label.Text = "Current CO2: " + e.ActiveValve.CO2.ToString() + ' ' + e.ActiveValve.CO2Units;
            this.CurrentH2O_Label.Text = "Current H2O: " + e.ActiveValve.H2O.ToString() + ' ' + e.ActiveValve.H2OUnits;
            this.CurrentTemp_Label.Text = "Current Temperature: " + e.ActiveValve.Temperature.ToString() + ' ' + e.ActiveValve.TemperatureUnits;
            this.CurrentFlow_Label.Text = "Current Flow: " + e.ActiveValve.Flow.ToString();
            this.WriteValveData(e.ActiveValve);
        }

        //Abhi: This will be called when ValvesSwitched event is invoked
        private void ValvesSwitched(object sender, DataUpdateEventArgs e)
        {

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

            this.FormSetupNoExperimentRunning();

            //Reset labels
            this.CurrentFileLocation_Label.Text = "Current File Location: ";

            this.outputStream.Close();
            this.filePath = null;
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

        // Form setup when no experiment is running
        private void FormSetupNoExperimentRunning()
        {
            //Disable all unnecessary Buttons
            this.Stop_Button.Enabled = false;

            //Enable all necessary Buttons
            this.valveCheckedListBox1.Enabled = true;
            this.StartNewExperiment_Button.Enabled = true;
            this.CheckAllValves_Button.Enabled = true;
            this.SelectAllValves.Enabled = true;
            this.CreateSaveFile_Button.Enabled = true;

            //Hide all unnecessary Lables
            this.experimentGroupBox.Hide();
            //Show all necessary Lables
            this.valveWeightGroupBox.Show();
        }

        private void FormSetupExperimentRunning()
        {
            //Disable/Enable all buttons and checkedListBox
            this.valveCheckedListBox1.Enabled = false;
            this.StartNewExperiment_Button.Enabled = false;
            this.CheckAllValves_Button.Enabled = false;
            this.SelectAllValves.Enabled = false;
            this.Stop_Button.Enabled = true;
            this.CreateSaveFile_Button.Enabled = false;

            this.valveWeightGroupBox.Hide();
            this.experimentGroupBox.Show();
        }

        private void valveCheckedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int index = e.Index;

            if (e.NewValue == CheckState.Checked)
            {
                foreach(Control textBox in this.valveWeightGroupBox.Controls)
                {
                    if (textBox is TextBox)
                    {
                        if(textBox.Name == "WeightValve" + (index + 1) + "_TextBox")
                        {
                            textBox.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                foreach (Control textBox in this.valveWeightGroupBox.Controls)
                {
                    if (textBox is TextBox)
                    {
                        if (textBox.Name == "WeightValve" + (index + 1) + "_TextBox")
                        {
                            textBox.Enabled = false;
                        }
                    }
                }
            }
        }

        private void WriteDataHeader()
        {
            string header = string.Empty;
            if (this.experimentEngine != null)
            {
                header = "Day of Experiment\t";
                header += this.experimentEngine.DateTimeHeader + "\t" + "Valve\t" + this.experimentEngine.LI7000DataHeader;
                header = header.Replace("pp/m", "(pp/m)").Replace("mm/m","(mm/m)").Replace("T C", "Temp ('C)");
                
                this.outputStream.WriteLine(header);
            }
        }

        private void WriteValveData(Valve argValve)
        {
            string valveData = string.Empty;

            valveData = this.experimentEngine.DaysSinceStart.ToString() + "\t";
            //Datetime
            valveData += argValve.MeasurementDateTime.ToString("MM/dd/yyyy") + "\t";
            valveData += argValve.MeasurementDateTime.ToString("H:mm") + "\t";

            //Data
            valveData += argValve.ValveNum + "\t";
            valveData += argValve.CO2.ToString() + "\t";
            valveData += argValve.H2O.ToString() + "\t";
            valveData += argValve.Temperature.ToString() + "\t";
            valveData += argValve.Flow.ToString() + "\t";

            this.outputStream.WriteLine(valveData);
        }
    }
}