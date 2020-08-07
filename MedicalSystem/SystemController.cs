using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworks.Models;

namespace MedicalSystem
{
	class SystemController
	{
		// Реализует паттерн Singleton
		private static SystemController _instance; 

		public NeuralNetwork DataNetwork { get; }

		public NeuralNetwork ImageNetwork { get; }

		private SystemController()
		{
			var dataTopology = new Topology(13, 1, 0.1, 7);
			DataNetwork = new NeuralNetwork(dataTopology);

			var imageTopology = new Topology(400, 1, 0.1, 200);
			ImageNetwork = new NeuralNetwork(imageTopology);
		}

		public static SystemController GetInstance()
		{
			if (_instance == null)
			{
				_instance = new SystemController();
			}
			return _instance;
		}

	}
}
