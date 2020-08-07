using System;

namespace NeuralNetworks.Models
{
	public static class Functions
	{
		private static double a = 0.07; // Коэффициент для LeakyReLU

		public static double Sigmoid(double x)
		{
			return 1.0 / (1.0 + Math.Exp(-x));
		}
		public static double SigmoidDx(double x)
		{
			var sigmoid = Sigmoid(x);
			return sigmoid / (1 - sigmoid);
		}

		public static double BipolarSigmoid(double x)
		{
			return (2 / 1 + Math.Exp(-x)) - 1;
		}
		public static double BipolarSigmoidDx(double x)
		{
			return 0.5 * (1 + BipolarSigmoid(x)) * (1 - BipolarSigmoid(x));
		}

		public static double Tanh(double x)
		{
			return Math.Tanh(x);
		}
		public static double TanhDx(double x)
		{
			var cosh = Math.Cosh(x);
			return 1 / (cosh * cosh);
		}

		public static double ReLU(double x)
		{
			return x > 0 ? x : 0;
		}
		public static double ReLUDx(double x)
		{
			return x > 0 ? 1 : 0;
		}

		public static double LeakyReLU(double x)
		{
			return x > 0 ? x : a * x;
		}
		public static double LeakyReLUDx(double x)
		{
			return x > 0 ? 1 : a;
		}
	}
}
