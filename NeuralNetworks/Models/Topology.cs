using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Models
{
	public class Topology
	{
		public int InputCount { get; }
		public int OutputCount { get; }
		public List<int> HiddenLayersCounts { get; }
		public double LearningRate { get; }
			
		public Topology(int inputCount, int outputCount, double learningRate, params int[] hiddenLayersCount)
		{
			InputCount = inputCount;
			OutputCount = outputCount;
			LearningRate = learningRate;
			HiddenLayersCounts = new List<int>(hiddenLayersCount);
		}
	}
}
