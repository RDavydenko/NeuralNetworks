using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworks.Models;

namespace NeuralNetworks.Tests
{
	[TestClass]
	public class PictureConverterTests
	{
		[TestMethod]
		public void SaveAfterBrightness()
		{
			var pc = new PictureConverter();
			var pixels = pc.Convert(@"Images\Parasitized.png");
			pc.Save(@"C:\image.png", pc.Width, pc.Height, pixels);
		}
	}
}
