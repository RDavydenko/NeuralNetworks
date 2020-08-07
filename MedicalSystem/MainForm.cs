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
using NeuralNetworks.Models;

namespace MedicalSystem
{
	public partial class MainForm : Form
	{
		private SystemController _controller;

		public MainForm()
		{
			InitializeComponent();
			messageLabel.Text = string.Empty;
			_controller = SystemController.GetInstance();
		}

		private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
		{
			About about = new About();
			about.ShowDialog();
		}

		private void imageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if(openFileDialog.ShowDialog() == DialogResult.OK)
			{
				var pictureConverter = new PictureConverter();
				string path = openFileDialog.FileName;
				pictureBoxAfter.Image = new Bitmap(Image.FromFile(path), pictureBoxAfter.Width, pictureBoxAfter.Height);
				var inputs = pictureConverter.Convert(path);
				var result = _controller.ImageNetwork.Predict(inputs);

				var convertedBitmap = pictureConverter.ConvertToBitmap(pictureConverter.Width, pictureConverter.Height, inputs);
				pictureBoxBefore.Image = new Bitmap(convertedBitmap, pictureBoxBefore.Width, pictureBoxBefore.Height);
				messageLabel.Text = "Шанс заражения клетки малярией составляет: " + result.ToString("0.0%");
			}
		}

		private void enterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			EnterData enterData = new EnterData();
			var dialogResult = enterData.ShowDialog();
			Patient patient = Patient.GetInstance();

			if (dialogResult == DialogResult.OK)
			{
				var inputs = patient.GetInputs();
				var chance = _controller.DataNetwork.Predict(inputs);
				messageLabel.Text = "Шанс наличия сердечно-сосудистого заболевания составляет: " + chance.ToString("0.0%");
			}
		}
	}
}
