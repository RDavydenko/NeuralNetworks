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


		public double Predict(params double[] inputSignals)
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
			//var signals = Normalization(inputs);

			var error = 0.0;
			for (int i = 0; i < epoch; i++)
			{
				// Прогоняем все данные из датасета
				for (int j = 0; j < outputs.Length; j++)
				{
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
			double actual = Predict(inputs);
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

		// Масштабирование
		private double[,] Scalling(double[,] inputs)
		{
			var result = new double[inputs.GetLength(0), inputs.GetLength(1)];
			for (int column = 0; column < inputs.GetLength(1); column++)
			{
				var min = inputs[0, column]; // Определяем минимум для конктретного столбца
				var max = inputs[0, column]; // Определяем максимум для конктретного столбца

				for (int row = 1; row < inputs.GetLength(0); row++)
				{
					var current = inputs[row, column];
					if (current < min)
					{
						min = current;
					}
					if (current > max)
					{
						max = current;
					}
				}

				double divided = max - min;
				for (int row = 0; row < inputs.GetLength(0); row++)
				{
					// Масштабирование
					result[row, column] = (inputs[row, column] - min) / divided;
				}
			}
			return result;
		}

		// Нормализация
		private double[,] Normalization(double[,] inputs)
		{
			var result = new double[inputs.GetLength(0), inputs.GetLength(1)];
			for (int column = 0; column < inputs.GetLength(1); column++)
			{
				// Получение среднего значения всех сигналов столбца
				var sum = 0.0;
				for (int row = 0; row < inputs.GetLength(0); row++)
				{
					sum += inputs[row, column];
				}
				var avg = sum / inputs.GetLength(0); // Среднее значение столбца

				// Получение среднего отклонения
				var error = 0.0;
				for (int row = 0; row < inputs.GetLength(0); row++)
				{
					error += (inputs[row, column] - avg) * (inputs[row, column] - avg);
				}
				var standartQuadError = Math.Sqrt(error / inputs.GetLength(0)); // Стандартное квадратичное отклонение элемента от стреднего значения

				// Нормализация
				for (int row = 0; row < inputs.GetLength(0); row++)
				{
					result[row, column] = (inputs[row, column] - avg) / standartQuadError;
				}
			}
			return result;
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


				neuron.Predict(signal);
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
					neuron.Predict(previousLayerSignals);
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
