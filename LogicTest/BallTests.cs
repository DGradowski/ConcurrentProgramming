using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Logic;

namespace LogicTest
{
	[TestFixture]
	public class BallTests
	{
		[Test]
		public void Constructor_SetsPropertiesCorrectly()
		{
			// Arrange
			var position = new Vector(10, 20);
			var velocity = new Vector(3, 4);
			int diameter = 15;

			// Act
			var ball = new Ball(position, velocity, diameter);

			// Assert
			Assert.AreEqual(position.X, ball.Position.X);
			Assert.AreEqual(position.Y, ball.Position.Y);
			Assert.AreEqual(velocity.X, ball.Velocity.X);
			Assert.AreEqual(velocity.Y, ball.Velocity.Y);
			Assert.AreEqual(diameter, ball.Diameter);
		}

		[Test]
		public void Move_ChangesPositionCorrectly()
		{
			// Arrange
			var ball = new Ball(new Vector(0, 0), new Vector(5, 7), 10);

			// Act
			ball.Move();

			// Assert
			Assert.AreEqual(5, ball.Position.X);
			Assert.AreEqual(7, ball.Position.Y);
		}

		[Test]
		public void Move_RaisesNewPositionNotification()
		{
			// Arrange
			var ball = new Ball(new Vector(0, 0), new Vector(1, 1), 10);
			Vector? notifiedPosition = null;

			ball.NewPositionNotification += (sender, pos) =>
			{
				notifiedPosition = pos;
			};

			// Act
			ball.Move();

			// Assert
			Assert.IsNotNull(notifiedPosition);
			Assert.AreEqual(1, notifiedPosition?.X);
			Assert.AreEqual(1, notifiedPosition?.Y);
		}

		[TestCase(0, 0, -5, 5)] // Odbicie od lewej
		[TestCase(692, 0, -5, 5)] // Odbicie od prawej
		[TestCase(0, 0, 5, -5)] // Odbicie od góry
		[TestCase(0, 492, 5, -5)] // Odbicie od dołu
		public void BounceOffWall_ReflectsVelocityCorrectly(int x, int y, int expectedVX, int expectedVY)
		{
			// Arrange
			var ball = new Ball(new Vector(x, y), new Vector(5, 5), 8);

			// Act
			ball.BounceOffWall(700 - 8, 500 - 8); // plansza o wymiarach 692x492

			// Assert
			Assert.AreEqual(expectedVX, ball.Velocity.X);
			Assert.AreEqual(expectedVY, ball.Velocity.Y);
		}

		[Test]
		public void StopMoving_CancelsTokenWithoutException()
		{
			// Arrange
			var ball = new Ball(new Vector(0, 0), new Vector(1, 1), 10);
			ball.StartMovingAsync();

			// Act & Assert
			Assert.DoesNotThrow(() => ball.StopMoving());
		}
	}
}
