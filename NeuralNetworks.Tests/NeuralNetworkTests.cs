using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
                // ��������� - ������� ����� - 1
                //             ������� ������ - 0

                // ������������ ����������� T
                // ������� ������� A
                // ����� S
                // ��������� �������� F
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

			// 4 �������, 1 - ��������, 1 ������� � 4 ���������
			var topology = new Topology(
				inputCount: 4,
				outputCount: 1,
				learningRate: 0.1,
				hiddenLayersCount: new int[] { 4 }
				);
			var neuralNetwork = new NeuralNetwork(topology);

			// ��������
			var difference = neuralNetwork.Learn(outputs, inputs, 100000); // ��. ������������ ����������

			// ������������� ����� ��������
			var results = new List<double>(outputs.Length);
			var inputsLength = inputs.GetLength(0);
			for (int i = 0; i < inputsLength; i++)
			{
				results.Add(neuralNetwork.FeedForward(NeuralNetwork.GetRow(inputs, i)));
			}

			Debug.WriteLine("��. ������������ ����������: " + difference);

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
				var header = sr.ReadLine(); // ������������ ������
				for (; !sr.EndOfStream;)
				{
					var row = sr.ReadLine();
					var values = row.Split(',').Select(v => Convert.ToDouble(v.Replace('.', ','))).ToList();
					var output = values.Last(); // ��������� ��������
					var input = values.Take(values.Count - 1).ToArray(); // ������ ������� ���������� (��� ���������� ��������)
					inputsCount = input.Length; // ������� ���������� ������� ���������� � ����� ������

					outputs.Add(output);
					inputs.Add(input);
				}
			}

			// ��������� � ��������� ������
			var validInputs = new double[inputs.Count, inputsCount];
			for (int i = 0; i < validInputs.GetLength(0); i++)
			{
				for (int j = 0; j < validInputs.GetLength(1); j++)
				{
					validInputs[i, j] = inputs[i][j];
				}
			}

			Topology topology = new Topology(
				inputCount: inputsCount,
				outputCount: 1,
				learningRate: 0.1,
				hiddenLayersCount: new int[] { inputsCount / 2 }
				);
			NeuralNetwork neuralNetwork = new NeuralNetwork(topology);

			// ��������� ���������
			double difference = neuralNetwork.Learn(outputs.ToArray(), validInputs, 1000);
			Debug.WriteLine("������������ ����������: " + difference.ToString());

			// ������ ���������
			var resultsAfterTraining = new List<double>(outputs.Count);
			foreach (var input in inputs)
			{
				resultsAfterTraining.Add(neuralNetwork.FeedForward(input));
			}

			for (int i = 0; i < resultsAfterTraining.Count; i++)
			{
				var expected = Math.Round(outputs[i], 3);
				var actual = Math.Round(resultsAfterTraining[i], 3);
				Assert.AreEqual(expected, actual);
			}
		}
	}
}
