namespace ProResp
{
    public partial class Form1 : Form
    {
        const int numOfValves = 24;

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

        }
    }
}