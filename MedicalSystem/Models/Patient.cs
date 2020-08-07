using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSystem.Models
{
	public class Patient
	{
		// Реализует паттерн Singleton
		private static Patient _instance;

		public int Age { get; set; }
		public Sex Sex { get; set;}
		public int ChestPainType { get; set;}
		public int BloodPressure { get; set;}
		public int Cholestoral { get; set;}
		public bool BloodSugar { get; set;} // true - 1, false - 0
		public int Electrocardiographic { get; set;}
		public int MaxHeartRate { get; set;}
		public bool HasAngina { get; set;} // true - 1, false - 0
		public double STDepression { get; set;} 
		public int Slope { get; set;} 
		public int MajorVesselsCount { get; set;} 
		public int Thal { get; set;}

		private Patient()
		{

		}
		
		public static Patient GetInstance()
		{
			if (_instance == null)
			{
				_instance = new Patient();
			}
			return _instance;
		}	

		public double[] GetInputs()
		{
			var props = typeof(Patient).GetProperties();
			var inputs = new double[props.Length];
			for (int i = 0; i < props.Length; i++)
			{
				inputs[i] = Convert.ToDouble(props[i].GetValue(this));
			}
			return inputs;
		}
	}

	public enum Sex
	{
		Female = 0,
		Male = 1
	}
}
