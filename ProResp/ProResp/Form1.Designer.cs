namespace ProResp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CheckAllValves_Button = new System.Windows.Forms.Button();
            this.StartNewExperiment_Button = new System.Windows.Forms.Button();
            this.valveCheckedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.ValveChecklist_Label = new System.Windows.Forms.Label();
            this.Stop_Button = new System.Windows.Forms.Button();
            this.ActiveChamber_Label = new System.Windows.Forms.Label();
            this.CurrentCO2_Label = new System.Windows.Forms.Label();
            this.CurrentH2O_Label = new System.Windows.Forms.Label();
            this.CurrentTemp_Label = new System.Windows.Forms.Label();
            this.CurrentFlow_Label = new System.Windows.Forms.Label();
            this.CurrentFileLocation_Label = new System.Windows.Forms.Label();
            this.PreviousValve_Label = new System.Windows.Forms.Label();
            this.FinalCO2_Label = new System.Windows.Forms.Label();
            this.FinalH2O_Label = new System.Windows.Forms.Label();
            this.FinalTemp_Label = new System.Windows.Forms.Label();
            this.FinalFlow_Label = new System.Windows.Forms.Label();
            this.SelectAllValves = new System.Windows.Forms.Button();
            this.CreateSaveFile_Button = new System.Windows.Forms.Button();
            this.experimentGroupBox = new System.Windows.Forms.GroupBox();
            this.valveWeightGroupBox = new System.Windows.Forms.GroupBox();
            this.experimentGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CheckAllValves_Button
            // 
            this.CheckAllValves_Button.Location = new System.Drawing.Point(50, 100);
            this.CheckAllValves_Button.Name = "CheckAllValves_Button";
            this.CheckAllValves_Button.Size = new System.Drawing.Size(250, 150);
            this.CheckAllValves_Button.TabIndex = 0;
            this.CheckAllValves_Button.Text = "Check All Valves";
            this.CheckAllValves_Button.UseVisualStyleBackColor = true;
            this.CheckAllValves_Button.Click += new System.EventHandler(this.CheckAllValves_Button_Click);
            // 
            // StartNewExperiment_Button
            // 
            this.StartNewExperiment_Button.Location = new System.Drawing.Point(50, 325);
            this.StartNewExperiment_Button.Name = "StartNewExperiment_Button";
            this.StartNewExperiment_Button.Size = new System.Drawing.Size(250, 150);
            this.StartNewExperiment_Button.TabIndex = 1;
            this.StartNewExperiment_Button.Text = "Start New Experiment";
            this.StartNewExperiment_Button.UseVisualStyleBackColor = true;
            this.StartNewExperiment_Button.Click += new System.EventHandler(this.StartNewExperiment_Button_Click);
            // 
            // valveCheckedListBox1
            // 
            this.valveCheckedListBox1.FormattingEnabled = true;
            this.valveCheckedListBox1.Location = new System.Drawing.Point(374, 121);
            this.valveCheckedListBox1.Name = "valveCheckedListBox1";
            this.valveCheckedListBox1.Size = new System.Drawing.Size(271, 508);
            this.valveCheckedListBox1.TabIndex = 2;
            this.valveCheckedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.valveCheckedListBox1_ItemCheck);
            // 
            // ValveChecklist_Label
            // 
            this.ValveChecklist_Label.Location = new System.Drawing.Point(374, 38);
            this.ValveChecklist_Label.Name = "ValveChecklist_Label";
            this.ValveChecklist_Label.Size = new System.Drawing.Size(269, 66);
            this.ValveChecklist_Label.TabIndex = 3;
            this.ValveChecklist_Label.Text = "Select Valves For Experiment";
            this.ValveChecklist_Label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Stop_Button
            // 
            this.Stop_Button.Location = new System.Drawing.Point(726, 143);
            this.Stop_Button.Name = "Stop_Button";
            this.Stop_Button.Size = new System.Drawing.Size(217, 413);
            this.Stop_Button.TabIndex = 4;
            this.Stop_Button.Text = "Stop";
            this.Stop_Button.UseVisualStyleBackColor = true;
            this.Stop_Button.Click += new System.EventHandler(this.Stop_Button_Click);
            // 
            // ActiveChamber_Label
            // 
            this.ActiveChamber_Label.Location = new System.Drawing.Point(52, 50);
            this.ActiveChamber_Label.Name = "ActiveChamber_Label";
            this.ActiveChamber_Label.Size = new System.Drawing.Size(371, 32);
            this.ActiveChamber_Label.TabIndex = 5;
            this.ActiveChamber_Label.Text = "Active Valve: ";
            // 
            // CurrentCO2_Label
            // 
            this.CurrentCO2_Label.Location = new System.Drawing.Point(55, 112);
            this.CurrentCO2_Label.Name = "CurrentCO2_Label";
            this.CurrentCO2_Label.Size = new System.Drawing.Size(374, 32);
            this.CurrentCO2_Label.TabIndex = 6;
            this.CurrentCO2_Label.Text = "Current CO2:";
            // 
            // CurrentH2O_Label
            // 
            this.CurrentH2O_Label.Location = new System.Drawing.Point(52, 181);
            this.CurrentH2O_Label.Name = "CurrentH2O_Label";
            this.CurrentH2O_Label.Size = new System.Drawing.Size(372, 32);
            this.CurrentH2O_Label.TabIndex = 7;
            this.CurrentH2O_Label.Text = "Current H2O:";
            // 
            // CurrentTemp_Label
            // 
            this.CurrentTemp_Label.Location = new System.Drawing.Point(55, 262);
            this.CurrentTemp_Label.Name = "CurrentTemp_Label";
            this.CurrentTemp_Label.Size = new System.Drawing.Size(371, 32);
            this.CurrentTemp_Label.TabIndex = 8;
            this.CurrentTemp_Label.Text = "Current Temperature:";
            // 
            // CurrentFlow_Label
            // 
            this.CurrentFlow_Label.Location = new System.Drawing.Point(55, 412);
            this.CurrentFlow_Label.Name = "CurrentFlow_Label";
            this.CurrentFlow_Label.Size = new System.Drawing.Size(371, 32);
            this.CurrentFlow_Label.TabIndex = 9;
            this.CurrentFlow_Label.Text = "Current Flow:";
            // 
            // CurrentFileLocation_Label
            // 
            this.CurrentFileLocation_Label.AutoSize = true;
            this.CurrentFileLocation_Label.Location = new System.Drawing.Point(1011, 688);
            this.CurrentFileLocation_Label.Name = "CurrentFileLocation_Label";
            this.CurrentFileLocation_Label.Size = new System.Drawing.Size(247, 32);
            this.CurrentFileLocation_Label.TabIndex = 10;
            this.CurrentFileLocation_Label.Text = "Current File Location: ";
            // 
            // PreviousValve_Label
            // 
            this.PreviousValve_Label.AutoSize = true;
            this.PreviousValve_Label.Location = new System.Drawing.Point(499, 44);
            this.PreviousValve_Label.Name = "PreviousValve_Label";
            this.PreviousValve_Label.Size = new System.Drawing.Size(172, 32);
            this.PreviousValve_Label.TabIndex = 11;
            this.PreviousValve_Label.Text = "Previous Valve:";
            // 
            // FinalCO2_Label
            // 
            this.FinalCO2_Label.AutoSize = true;
            this.FinalCO2_Label.Location = new System.Drawing.Point(499, 129);
            this.FinalCO2_Label.Name = "FinalCO2_Label";
            this.FinalCO2_Label.Size = new System.Drawing.Size(121, 32);
            this.FinalCO2_Label.TabIndex = 12;
            this.FinalCO2_Label.Text = "Final CO2:";
            // 
            // FinalH2O_Label
            // 
            this.FinalH2O_Label.AutoSize = true;
            this.FinalH2O_Label.Location = new System.Drawing.Point(499, 223);
            this.FinalH2O_Label.Name = "FinalH2O_Label";
            this.FinalH2O_Label.Size = new System.Drawing.Size(131, 32);
            this.FinalH2O_Label.TabIndex = 13;
            this.FinalH2O_Label.Text = "Final H2O: ";
            // 
            // FinalTemp_Label
            // 
            this.FinalTemp_Label.AutoSize = true;
            this.FinalTemp_Label.Location = new System.Drawing.Point(499, 311);
            this.FinalTemp_Label.Name = "FinalTemp_Label";
            this.FinalTemp_Label.Size = new System.Drawing.Size(135, 32);
            this.FinalTemp_Label.TabIndex = 14;
            this.FinalTemp_Label.Text = "Final Temp:";
            // 
            // FinalFlow_Label
            // 
            this.FinalFlow_Label.AutoSize = true;
            this.FinalFlow_Label.Location = new System.Drawing.Point(499, 412);
            this.FinalFlow_Label.Name = "FinalFlow_Label";
            this.FinalFlow_Label.Size = new System.Drawing.Size(125, 32);
            this.FinalFlow_Label.TabIndex = 15;
            this.FinalFlow_Label.Text = "Final Flow:";
            // 
            // SelectAllValves
            // 
            this.SelectAllValves.Location = new System.Drawing.Point(374, 635);
            this.SelectAllValves.Name = "SelectAllValves";
            this.SelectAllValves.Size = new System.Drawing.Size(271, 46);
            this.SelectAllValves.TabIndex = 16;
            this.SelectAllValves.Text = "Select All Valves";
            this.SelectAllValves.UseVisualStyleBackColor = true;
            this.SelectAllValves.Click += new System.EventHandler(this.SelectAllValves_Click);
            // 
            // CreateSaveFile_Button
            // 
            this.CreateSaveFile_Button.Location = new System.Drawing.Point(50, 550);
            this.CreateSaveFile_Button.Name = "CreateSaveFile_Button";
            this.CreateSaveFile_Button.Size = new System.Drawing.Size(250, 150);
            this.CreateSaveFile_Button.TabIndex = 17;
            this.CreateSaveFile_Button.Text = "Create Data File";
            this.CreateSaveFile_Button.UseVisualStyleBackColor = true;
            this.CreateSaveFile_Button.Click += new System.EventHandler(this.CreateSaveFile_Button_Click);
            // 
            // experimentGroupBox
            // 
            this.experimentGroupBox.Controls.Add(this.FinalFlow_Label);
            this.experimentGroupBox.Controls.Add(this.FinalTemp_Label);
            this.experimentGroupBox.Controls.Add(this.FinalH2O_Label);
            this.experimentGroupBox.Controls.Add(this.FinalCO2_Label);
            this.experimentGroupBox.Controls.Add(this.PreviousValve_Label);
            this.experimentGroupBox.Controls.Add(this.CurrentFlow_Label);
            this.experimentGroupBox.Controls.Add(this.CurrentTemp_Label);
            this.experimentGroupBox.Controls.Add(this.CurrentH2O_Label);
            this.experimentGroupBox.Controls.Add(this.CurrentCO2_Label);
            this.experimentGroupBox.Controls.Add(this.ActiveChamber_Label);
            this.experimentGroupBox.Location = new System.Drawing.Point(1010, 69);
            this.experimentGroupBox.Name = "experimentGroupBox";
            this.experimentGroupBox.Size = new System.Drawing.Size(850, 600);
            this.experimentGroupBox.TabIndex = 18;
            this.experimentGroupBox.TabStop = false;
            this.experimentGroupBox.Text = "Current Experiment Data:";
            // 
            // valveWeightGroupBox
            // 
            this.valveWeightGroupBox.Location = new System.Drawing.Point(1011, 69);
            this.valveWeightGroupBox.Name = "valveWeightGroupBox";
            this.valveWeightGroupBox.Size = new System.Drawing.Size(950, 600);
            this.valveWeightGroupBox.TabIndex = 19;
            this.valveWeightGroupBox.TabStop = false;
            this.valveWeightGroupBox.Text = "Valve Weights (g):";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1974, 729);
            this.Controls.Add(this.experimentGroupBox);
            this.Controls.Add(this.valveWeightGroupBox);
            this.Controls.Add(this.CreateSaveFile_Button);
            this.Controls.Add(this.SelectAllValves);
            this.Controls.Add(this.Stop_Button);
            this.Controls.Add(this.CurrentFileLocation_Label);
            this.Controls.Add(this.ValveChecklist_Label);
            this.Controls.Add(this.valveCheckedListBox1);
            this.Controls.Add(this.StartNewExperiment_Button);
            this.Controls.Add(this.CheckAllValves_Button);
            this.Name = "Form1";
            this.Text = "ProResp";
            this.experimentGroupBox.ResumeLayout(false);
            this.experimentGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button CheckAllValves_Button;
        private Button StartNewExperiment_Button;
        private CheckedListBox valveCheckedListBox1;
        private Label ValveChecklist_Label;
        private Button Stop_Button;
        private Label ActiveChamber_Label;
        private Label CurrentCO2_Label;
        private Label CurrentH2O_Label;
        private Label CurrentTemp_Label;
        private Label CurrentFlow_Label;
        private Label CurrentFileLocation_Label;
        private Label PreviousValve_Label;
        private Label FinalCO2_Label;
        private Label FinalH2O_Label;
        private Label FinalTemp_Label;
        private Label FinalFlow_Label;
        private Button SelectAllValves;
        private Button CreateSaveFile_Button;
        private GroupBox experimentGroupBox;
        private GroupBox valveWeightGroupBox;
    }
}