namespace ProResp
{
    using ExperimentEngine;
    public partial class Form1 : Form
    {
        const int numOfValves = 24;
        const int msValveSwitchTime = 900000; //15 mins
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

            experimentEngine = new ExperimentEngine(checkedValves, msValveSwitchTime);

            //Future: disable checkboxlist so current experiment values are shown and can't be changed.
            //One option is to use OnClick event and just undo a check and display error message.

            //Lock start experiment and check valves buttons
            //Unlock stop button

        }

        private void Stop_Button_Click(object sender, EventArgs e)
        {
            //Call ExperimentEngine.Stop
            //Free ExperimentEngine
        }
    }
}