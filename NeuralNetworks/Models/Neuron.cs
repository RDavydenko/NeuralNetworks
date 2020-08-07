using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Models
{
	public class Neuron
	{
		public List<double> Weights { get; }
		public List<double> Inputs { get; }
		public NeuronType NeuronType { get; }
		public double Output { get; private set; }
		public double Delta { get; private set; }

		public Func<double, double> Activation { get; } // Функция активации
		public Func<double, double> ActivationDx { get; } // Производная функции активации

		public Neuron(int inputCount, NeuronType neuronType = NeuronType.Hidden)
		{
			if (inputCount <= 0)
			{
				throw new Exception("Количество входных сигналов должно быть не менее одного!");
			}

			NeuronType = neuronType;
			Weights = new List<double>(inputCount);
			Inputs = new List<double>(inputCount);
			InitWeightsRandomValues(inputCount); // Заполняем случайными коэффициентами

			// Установка функции активации
			Activation = Functions.Sigmoid;
			ActivationDx = Functions.SigmoidDx;
		}

		public double Predict(List<double> inputs)
		{
			if (inputs.Count != Weights.Count)
			{
				throw new Exception("Количество входных сигналов должно совпадать с количеством весов!");
			}

			// Сохраняем входные сигналы
			for (int i = 0; i < inputs.Count; i++)
			{
				Inputs[i] = inputs[i];
			}

			// Суммирование сигналов
			double sum = 0.0;
			for (int i = 0; i < inputs.Count; i++)
			{
				sum += inputs[i] * Weights[i];
			}

			// Функция активации
			if (NeuronType != NeuronType.Input)
			{
				Output = Activation(sum);
			}
			else
			{
				Output = sum;
			}
			return Output;
		}
		
		public void Learn(double error, double learningRate)
		{
			if (NeuronType == NeuronType.Input)
			{
				return;
			}

			Delta = error * ActivationDx(Output);			

			// Корректировка веса по ошибке
			for (int i = 0; i < Weights.Count; i++)
			{
				var newWeight = Weights[i] - Inputs[i] * Delta * learningRate;
				Weights[i] = newWeight;
			}
		}		

		private void InitWeightsRandomValues(int inputCount)
		{
			var rnd = new Random();
			for (int i = 0; i < inputCount; i++)
			{
				if (NeuronType == NeuronType.Input)
				{
					Weights.Add(1.0);
				}
				else
				{
					int sign = rnd.Next(0, 2) == 0 ? 1 : -1;
					Weights.Add(sign * rnd.NextDouble() / 2); // Случайные от -0.5 до 0.5
				}
				Inputs.Add(0.0);
			}
		}
	}
}
