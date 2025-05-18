using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace DataTest
{
	[TestFixture]
	public class VectorTests
	{
		[Test]
		public void Constructor_SetsXAndYCorrectly()
		{
			// Arrange
			int expectedX = 5;
			int expectedY = -3;

			// Act
			Vector vector = new Vector(expectedX, expectedY);

			// Assert
			Assert.AreEqual(expectedX, vector.X);
			Assert.AreEqual(expectedY, vector.Y);
		}

		[Test]
		public void X_SetAndGet_WorksCorrectly()
		{
			// Arrange
			Vector vector = new Vector(0, 0);
			int newX = 10;

			// Act
			vector.X = newX;

			// Assert
			Assert.AreEqual(newX, vector.X);
		}

		[Test]
		public void Y_SetAndGet_WorksCorrectly()
		{
			// Arrange
			Vector vector = new Vector(0, 0);
			int newY = -20;

			// Act
			vector.Y = newY;

			// Assert
			Assert.AreEqual(newY, vector.Y);
		}
	}
}
