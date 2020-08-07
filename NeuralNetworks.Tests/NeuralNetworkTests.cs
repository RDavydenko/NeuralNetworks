using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworks.Models;

namespace NeuralNetworks.Tests
{
	[TestClass]
	public class NeuralNetworkTests
	{
		[TestMethod]
		public void FeedForwardTest()
		{
			var outputs = new double[] { 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1 };
			var inputs = new double[,]
			{
                // Результат - Пациент болен - 1
                //             Пациент Здоров - 0

                // Неправильная температура T
                // Хороший возраст A
                // Курит S
                // Правильно питается F
                //T  A  S  F
                { 0, 0, 0, 0 },
				{ 0, 0, 0, 1 },
				{ 0, 0, 1, 0 },
				{ 0, 0, 1, 1 },
				{ 0, 1, 0, 0 },
				{ 0, 1, 0, 1 },
				{ 0, 1, 1, 0 },
				{ 0, 1, 1, 1 },
				{ 1, 0, 0, 0 },
				{ 1, 0, 0, 1 },
				{ 1, 0, 1, 0 },
				{ 1, 0, 1, 1 },
				{ 1, 1, 0, 0 },
				{ 1, 1, 0, 1 },
				{ 1, 1, 1, 0 },
				{ 1, 1, 1, 1 }
			};

			// 4 входных, 1 - выходной, 1 скрытый с 4 нейронами
			var topology = new Topology(
				inputCount: 4,
				outputCount: 1,
				learningRate: 0.1,
				hiddenLayersCount: new int[] { 4 }
				);
			var neuralNetwork = new NeuralNetwork(topology);

			// Обучение
			var difference = neuralNetwork.Learn(outputs, inputs, 100000); // Ср. квадратичное отклонение

			// Использование после обучения
			var results = new List<double>(outputs.Length);
			var inputsLength = inputs.GetLength(0);
			for (int i = 0; i < inputsLength; i++)
			{
				results.Add(neuralNetwork.Predict(NeuralNetwork.GetRow(inputs, i)));
			}

			Debug.WriteLine("Ср. квадратичное отклонение: " + difference);

			// Assert
			for (int i = 0; i < results.Count; i++)
			{
				var expected = Math.Round(outputs[i], 4);
				var actual = Math.Round(results[i], 4);
				Assert.AreEqual(expected, actual, 0.001);
			}
		}

		[TestMethod]
		public void DatasetTest()
		{
			var outputs = new List<double>();
			var inputs = new List<double[]>();
			var inputsCount = 0;
			using (var sr = new StreamReader("datasets/heart.csv"))
			{
				var header = sr.ReadLine(); // Заголовочная строка
				for (; !sr.EndOfStream;)
				{
					var row = sr.ReadLine();
					var values = row.Split(',').Select(v => Convert.ToDouble(v.Replace('.', ','))).ToList();
					var output = values.Last(); // Ожидаемое значение 
					/* !!!!!
					 * В этом датасете - 1 здоров, 0 - болен
					 * !!!!! 
					 */
					var input = values.Take(values.Count - 1).ToArray(); // Массив входных параметров (без ожидаемого значения)
					inputsCount = input.Length; // Считаем количество входных параметров В ОДНОЙ СТРОКЕ

					outputs.Add(output);
					inputs.Add(input);
				}
			}


			// Перемешиваем списки
			LinkedMix(ref inputs, ref outputs);
			LinkedMix(ref inputs, ref outputs);
			LinkedMix(ref inputs, ref outputs);
			LinkedMix(ref inputs, ref outputs);
			LinkedMix(ref inputs, ref outputs);



			// Перегоняю в двумерный массив
			var validInputs = new double[inputs.Count, inputsCount];
			for (int i = 0; i < validInputs.GetLength(0); i++)
			{
				for (int j = 0; j < validInputs.GetLength(1); j++)
				{
					validInputs[i, j] = inputs[i][j];
				}
			}
			// Нормализуем данные
			var normalizedInputs = NeuralNetwork.Normalization(validInputs);			

			Topology topology = new Topology(
				inputCount: inputsCount,
				outputCount: 1,
				learningRate: 1,
				hiddenLayersCount: new int[] { inputsCount / 2 }
				);
			NeuralNetwork neuralNetwork = new NeuralNetwork(topology);

			// Тренируем нейросеть
			double difference = 500;
			for (int i = 0; difference > 10; i++)
			{				
				topology.LearningRate = 0.055 * Math.Pow(0.7, i / 1500.0);
				difference = neuralNetwork.Learn(outputs.ToArray(), normalizedInputs, 1);
			}

			Debug.WriteLine("Квадратичное отклонение: " + difference.ToString());

			// Запуск нейросети
			var resultsAfterTraining = new List<double>();
			for (int i = 0; i < normalizedInputs.GetLength(0); i++)
			{
				var input = NeuralNetwork.GetRow(normalizedInputs, i);
				resultsAfterTraining.Add(neuralNetwork.Predict(input));
			}

			for (int i = 0; i < resultsAfterTraining.Count; i++)
			{
				var expected = Math.Round(outputs[i], 3);
				var actual = Math.Round(resultsAfterTraining[i], 3);
				//Assert.AreEqual(expected, actual);
			}
		}

		[TestMethod]
		public void FindBestLearningRate()
		{
			var outputs = new List<double>();
			var inputs = new List<double[]>();
			var inputsCount = 0;
			using (var sr = new StreamReader("datasets/heart.csv"))
			{
				var header = sr.ReadLine(); // Заголовочная строка
				for (; !sr.EndOfStream;)
				{
					var row = sr.ReadLine();
					var values = row.Split(',').Select(v => Convert.ToDouble(v.Replace('.', ','))).ToList();
					var output = values.Last(); // Ожидаемое значение 
					/* !!!!!
					 * В этом датасете - 1 здоров, 0 - болен
					 * !!!!! 
					 */
					var input = values.Take(values.Count - 1).ToArray(); // Массив входных параметров (без ожидаемого значения)
					inputsCount = input.Length; // Считаем количество входных параметров В ОДНОЙ СТРОКЕ

					outputs.Add(output);
					inputs.Add(input);
				}
			}

			// Перемешиваем списки
			LinkedMix(ref inputs, ref outputs);
			LinkedMix(ref inputs, ref outputs);
			LinkedMix(ref inputs, ref outputs);
			LinkedMix(ref inputs, ref outputs);
			LinkedMix(ref inputs, ref outputs);

			// Перегоняю в двумерный массив
			var validInputs = new double[inputs.Count, inputsCount];
			for (int i = 0; i < validInputs.GetLength(0); i++)
			{
				for (int j = 0; j < validInputs.GetLength(1); j++)
				{
					validInputs[i, j] = inputs[i][j];
				}
			}
			// Нормализуем данные
			var normalizedInputs = NeuralNetwork.Scalling(validInputs);

			//                         differ  learnR hidden epoch 
			var datas = new List<Tuple<double, double, int[], int>>();
			double minDifference = 100;
			double bestLearningRate;
			int[] bestHidden;
			int bestEpoch;
			List<int[]> hiddenLayers = new List<int[]>
			{
				new int[] {26},
				new int[] {26, 13},
				new int[] {26, 13, 6},
				new int[] {26, 13, 6, 3},
				new int[] {13},
				new int[] {13, 6},
				new int[] {13, 6, 3},
				new int[] {8},
				new int[] {8, 4},
				new int[] {8, 4, 2},
				new int[] {6},
				new int[] {6, 3},
				new int[] {6, 4, 2},
				new int[] {4},
				new int[] {4, 2},
				new int[] {2},
			};

			for (int i = 0; i < hiddenLayers.Count; i++)
			{
				for (double learningRate = 0.002; learningRate == 0.002; learningRate++)
				{
					for (int epoch = 1400; epoch == 1400; epoch++)
					{
						Topology topology = new Topology(
								inputCount: inputsCount,
								outputCount: 1,
								learningRate: learningRate,
								hiddenLayersCount: hiddenLayers[i]
								);
						NeuralNetwork neuralNetwork = new NeuralNetwork(topology);
						double difference = neuralNetwork.Learn(outputs.ToArray(), normalizedInputs, epoch);

						if (difference < minDifference)
						{
							minDifference = difference;
							bestLearningRate = learningRate;
							bestHidden = hiddenLayers[i];
							bestEpoch = epoch;
						}
						datas.Add(new Tuple<double, double, int[], int>(difference, learningRate, hiddenLayers[i], epoch));
					}
				}
			}
			int a = 0;
		}

		[TestMethod]
		public void RecogniseImages()
		{
			var size = 10; // Количество изображений в выборке для тестов
			var parasitizedPath = @"C:\Users\Roman\Desktop\datasets\malaria\Parasitized";
			var uninfectedPath = @"C:\Users\Roman\Desktop\datasets\malaria\Uninfected";

			var converter = new PictureConverter();
			var testImageInput = converter.Convert(@"Images\Uninfected.png");

			Topology topology = new Topology(
				inputCount: testImageInput.Length,
				outputCount: 1,
				learningRate: 0.1,
				hiddenLayersCount: new int[] { testImageInput.Length / 2 });
			var neuralNetwork = new NeuralNetwork(topology);

			// Обучаем паразитированными изображениями
			double[,] parasitizedInputs = GetInputsData(parasitizedPath, converter, testImageInput, size);
			neuralNetwork.Learn(new double[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 }, parasitizedInputs, 10);

			// Обучаем здоровыми изображениями
			double[,] uninfectedInputs = GetInputsData(uninfectedPath, converter, testImageInput, size);
			neuralNetwork.Learn(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 }, uninfectedInputs, 10);

			// Тестирование 
			var testParasitizedInputs = converter.Convert(@"Images\Parasitized.png").Select(x => (double)x).ToArray();
			double parasitizedOutput = neuralNetwork.Predict(testParasitizedInputs);

			var testUninfectedInputs = converter.Convert(@"Images\Uninfected.png").Select(x => (double)x).ToArray();
			double uninfectedOutput = neuralNetwork.Predict(testUninfectedInputs);

			// Assert

			Assert.AreEqual(1, Math.Round(parasitizedOutput, 2));
			Assert.AreEqual(0, Math.Round(uninfectedOutput, 2));

		}

		// Получить данные из изображений
		private static double[,] GetInputsData(string path, PictureConverter converter, double[] testImageInput, int size)
		{
			var images = Directory.GetFiles(path);
			var inputs = new double[size, testImageInput.Length];
			for (int i = 0; i < size; i++)
			{
				var image = converter.Convert(images[i]);
				for (int j = 0; j < image.Length; j++)
				{
					inputs[i, j] = image[j];
				}
			}
			return inputs;
		}

		// Перемешивает строки в обоих списках (СВЯЗАННО друг с другом)
		private static void LinkedMix<T, V>(ref List<T> inputs, ref List<V> outputs)
		{
			if (inputs.Count != outputs.Count)
			{
				throw new Exception("Списки должны быть одинаковых размеров");
			}

			List<int> indexes = new List<int>();
			for (int i = 0; i < inputs.Count; i++)
			{
				indexes.Add(i);
			}

			var tempInputs = new List<T>();
			var tempOutputs = new List<V>();
			Random rnd = new Random();
			while (indexes.Count > 0)
			{
				int index = indexes[rnd.Next(indexes.Count)];
				tempInputs.Add(inputs[index]);
				tempOutputs.Add(outputs[index]);
				indexes.Remove(index);
			}

			inputs = tempInputs;
			outputs = tempOutputs;
		}

		[TestMethod]
		public void LinkedMixTest()
		{
			List<double[]> inputs = new List<double[]> { new double[] { 1.1, 1.2 }, new double[] { 2.1, 2.2 }, new double[] { 3.1, 3.2 }, new double[] { 4.1, 4.2 }, new double[] { 5.1, 5.2 } };
			List<double> outputs = new List<double> { 1.1, 1.2, 1.3, 1.4, 1.5 };
			LinkedMix(ref inputs, ref outputs);
		}
	}
}
