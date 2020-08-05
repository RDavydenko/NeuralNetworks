using System;
using System.Collections.Generic;
using System.Diagnostics;
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
	}
}
