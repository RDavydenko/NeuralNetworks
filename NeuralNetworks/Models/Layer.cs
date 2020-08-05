using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Models
{
	public class Layer
	{
		public List<Neuron> Neurons { get; }
		public int NeuronsCount => Neurons?.Count ?? 0;
		public NeuronType NeuronType { get; }


		public Layer(List<Neuron> neurons, NeuronType neuronType = NeuronType.Hidden)
		{
			Neurons = neurons;
			NeuronType = neuronType;
		}

		public List<double> GetOutputs()
		{
			var outputs = new List<double>(Neurons.Count);
			foreach (var neuron in Neurons)
			{
				outputs.Add(neuron.Output);
			}
			return outputs;
		}
	}
}
