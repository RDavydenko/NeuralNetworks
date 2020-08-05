using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Models
{
	public class PictureConverter
	{
		public int Boundary { get; set; } = 128; // Пороговое значение при расчете яркости
		public int GroupSize { get; set; } = 10; // Свертка пикселей изображения в группы

		public int Width { get; private set; } // Ширина последнего обработанного изображения
		public int Height { get; private set; } // Высота последнего обработанного изображения

		public List<int> Convert(string path)
		{
			var image = new Bitmap(path);

			var resizedImage = new Bitmap(image, new Size(50, 50));
			var size = resizedImage.Width * resizedImage.Height;

			Width = resizedImage.Width;
			Height = resizedImage.Height;

			var result = new List<int>(size);

			for (int y = 0; y < resizedImage.Height; y++)
			{
				for (int x = 0; x < resizedImage.Width; x++)
				{
					var pixel = resizedImage.GetPixel(x, y);
					var value = GetBrightness(pixel);
					result.Add(value);					
				}
			}

			return result;
		}

		private int GetBrightness(Color pixel)
		{
			var result = 0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B;
			return result < Boundary ? 0 : 1;
		}

		public void Save(string path, int width, int height, List<int> pixels)
		{
			var image = new Bitmap(width, height);
			for (int y = 0; y < image.Height; y++)
			{
				for (int x = 0; x < image.Width; x++)
				{
					Color color = pixels[y * width + x] == 1 ? Color.White : Color.Black;
					image.SetPixel(x, y, color);
				}
			}
			image.Save(path, ImageFormat.Png);
		}
	}
}
