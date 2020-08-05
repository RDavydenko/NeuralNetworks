using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Models
{
	public class NeuralNetwork
	{
		public Topology Topology { get; }

		public List<Layer> Layers { get; }

		public NeuralNetwork(Topology topology)
		{
			Topology = topology;

			Layers = new List<Layer>();

			CreateInputLayer();
			CreateHiddenLayers();
			CreateOutputLayer();
		}


		public double FeedForward(params double[] inputSignals)
		{
			// Отправка входных данных на входные нейроны
			SendSignalsToInputNeurons(inputSignals);

			// Проходимся по всем слоям после входных
			FeedForwardAllLayersAfterInput();

			// Возращаем результат выходного слоя	
			return Layers.Last().Neurons.Max(n => n.Output);
		}

		public double Learn(double[] outputs, double[,] inputs, int epoch)
		{
			var error = 0.0;

			for (int i = 0; i < epoch; i++)
			{
				for(int j = 0; j < outputs.Length; j++)
				{
					// Прогоняем все данные из датасета
					double[] row = GetRow(inputs, j);
					error += Backpropagation(outputs[j], row);
				}
			}

			return error / epoch; // Возвращает среднюю ошибку
		}

		public static double[] GetRow(double[,] matrix, int column)
		{
			var count = matrix.GetLength(1);
			var array = new double[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = matrix[column, i];
			}
			return array;
		}

		private double Backpropagation(double expected, params double[] inputs)
		{
			double actual = FeedForward(inputs);
			double difference = actual - expected;
			var lastLayer = Layers.Last();
			foreach (var neuron in lastLayer.Neurons) // Обучение последнего слоя 
			{
				neuron.Learn(difference, Topology.LearningRate);
			}

			for (int i = Layers.Count - 2; i >= 0; i--)
			{
				var layer = Layers[i];
				var previousLayer = Layers[i + 1]; // Обученный слой
				for (int j = 0; j < layer.NeuronsCount; j++)
				{
					var neuron = layer.Neurons[j];
					for (int k = 0; k < previousLayer.NeuronsCount; k++)
					{
						var previousNeuron = previousLayer.Neurons[k];
						var error = previousNeuron.Weights[j] * previousNeuron.Delta;
						neuron.Learn(error, Topology.LearningRate);
					}
				}
			}
			return difference * difference; // Возращается квадратичная ошибка
		}

		private void SendSignalsToInputNeurons(params double[] inputSignals)
		{
			if (inputSignals.Length != Layers[0].NeuronsCount)
			{
				throw new Exception("Количество входных сигналов должно быть равно количеству входных нейронов!");
			}

			for (int i = 0; i < inputSignals.Length; i++)
			{
				// Сигнал один, т.к входящий сигнал для Input-нейрона один
				var signal = new List<double>() { inputSignals[i] };
				var neuron = Layers[0].Neurons[i];


				neuron.FeedForward(signal);
			}
		}

		private void FeedForwardAllLayersAfterInput()
		{
			// Проходимся по всем слоям после входных
			for (int i = 1; i < Layers.Count; i++)
			{
				var previousLayerSignals = Layers[i - 1].GetOutputs();
				var layer = Layers[i];

				foreach (var neuron in layer.Neurons)
				{
					neuron.FeedForward(previousLayerSignals);
				}
			}
		}

		private void CreateInputLayer()
		{
			var inputNeurons = new List<Neuron>(Topology.InputCount);
			for (int i = 0; i < Topology.InputCount; i++)
			{
				var neuron = new Neuron(1, NeuronType.Input);
				inputNeurons.Add(neuron);
			}
			var inputLayer = new Layer(inputNeurons, NeuronType.Input);
			Layers.Add(inputLayer);
		}

		private void CreateHiddenLayers()
		{
			for (int i = 0; i < Topology.HiddenLayersCounts.Count; i++)
			{
				var hiddenNeurons = new List<Neuron>();
				var lastLayer = Layers.Last();
				for (int j = 0; j < Topology.HiddenLayersCounts[i]; j++)
				{
					var neuron = new Neuron(lastLayer.NeuronsCount, NeuronType.Hidden);
					hiddenNeurons.Add(neuron);
				}
				var hiddenLayer = new Layer(hiddenNeurons, NeuronType.Hidden);
				Layers.Add(hiddenLayer);
			}
		}

		private void CreateOutputLayer()
		{
			var outputNeurons = new List<Neuron>(Topology.OutputCount);
			var lastLayer = Layers.Last();
			for (int i = 0; i < Topology.OutputCount; i++)
			{
				var neuron = new Neuron(lastLayer.NeuronsCount, NeuronType.Output);
				outputNeurons.Add(neuron);
			}
			var outputLayer = new Layer(outputNeurons, NeuronType.Output);
			Layers.Add(outputLayer);
		}
	}
}
