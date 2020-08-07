using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MedicalSystem.Models;

namespace MedicalSystem
{
	public partial class EnterData : Form
	{
		public EnterData()
		{
			InitializeComponent();
		}

		private void enterDataBtn_Click(object sender, EventArgs e)
		{
			Patient patient = Patient.GetInstance();
			patient.Age = int.Parse(ageTextBox.Text);
			patient.Sex = (Sex)sexComboBox.SelectedIndex;
			patient.ChestPainType = chestPainTypeComboBox.SelectedIndex;
			patient.BloodPressure = int.Parse(pressureTextBox.Text);
			patient.Cholestoral = int.Parse(cholestoralTextBox.Text);
			patient.BloodSugar = sugarComboBox.SelectedIndex == 1 ? true : false;
			patient.Electrocardiographic = electrocardiographicComboBox.SelectedIndex;
			patient.MaxHeartRate = int.Parse(maxHeartRateTextBox.Text);
			patient.HasAngina = hasAnginaComboBox.SelectedIndex == 1 ? true : false;
			patient.STDepression = double.Parse(stDepressionTextBox.Text.Replace('.',','));
			patient.Slope = slopeComboBox.SelectedIndex;
			patient.MajorVesselsCount = int.Parse(majorVesselsCountTextBox.Text);
			int thalIndex = thalComboBox.SelectedIndex;
			patient.Thal = 
				thalIndex == 0 ? 1 : 
				thalIndex == 1 ? 2 : 7;

			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
