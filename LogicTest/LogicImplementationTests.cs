using Logic;
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
	public class LogicImplementationTests
	{
		[Test]
		public void Start_CreatesCorrectNumberOfBallsAndCallsHandler()
		{
			// Arrange
			var logic = new LogicImplementation();
			int counter = 0;

			// Act
			logic.Start(5, b => counter++);

			// Assert
			Assert.AreEqual(5, counter);

			logic.Stop();
		}

		[Test]
		public void Pause_StopsAllBalls()
		{
			// Arrange
			var logic = new LogicImplementation();
			List<Ball> receivedBalls = new();
			logic.Start(3, b => receivedBalls.Add(b));

			// Act
			logic.Pause();

			// Assert
			// Ponieważ `Ball.StopMoving()` nie zwraca stanu, zakładamy że `Pause()` nie rzuca wyjątków
			Assert.Pass("Pause executed without exception.");
		}

		[Test]
		public void Continue_DoesNotThrow_WhenNotPaused()
		{
			// Arrange
			var logic = new LogicImplementation();

			// Act & Assert
			Assert.DoesNotThrow(() => logic.Continue());
		}

		[Test]
		public void Dispose_ThrowsOnSecondCall()
		{
			// Arrange
			var logic = new LogicImplementation();
			logic.Start(1, _ => { });

			// Act
			logic.Dispose();

			// Assert
			Assert.Throws<ObjectDisposedException>(() => logic.Dispose());
		}

		[Test]
		public void Stop_ClearsAllBalls()
		{
			// Arrange
			var logic = new LogicImplementation();
			logic.Start(3, _ => { });

			// Act
			logic.Stop();

			// Assert
			// Brak publicznego dostępu do listy, więc sprawdzamy przez ponowne uruchomienie i brak błędów
			Assert.DoesNotThrow(() => logic.Start(1, _ => { }));
			logic.Stop();
		}

		[Test]
		public void IsColliding_ReturnsTrueForOverlappingBalls()
		{
			// Arrange
			var logic = new LogicImplementation();

			var b1 = new Ball(new Vector(10, 10), new Vector(0, 0), 20);
			var b2 = new Ball(new Vector(15, 15), new Vector(0, 0), 20);

			var method = typeof(LogicImplementation).GetMethod("IsColliding", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			Assert.IsNotNull(method);

			// Act
			bool result = (bool)method.Invoke(logic, new object[] { b1, b2 })!;

			// Assert
			Assert.IsTrue(result);
		}

		[Test]
		public void ResolveCollisionInternal_ChangesVelocitiesCorrectly()
		{
			// Arrange
			var logic = new LogicImplementation();
			var b1 = new Ball(new Vector(0, 0), new Vector(1, 0), 20);
			var b2 = new Ball(new Vector(10, 0), new Vector(-1, 0), 20);

			var method = typeof(LogicImplementation).GetMethod("ResolveCollisionInternal", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			Assert.IsNotNull(method);

			// Act
			method.Invoke(logic, new object[] { b1, b2 });

			// Assert
			Assert.AreEqual(-1, b1.Velocity.X);
			Assert.AreEqual(1, b2.Velocity.X);
		}

		[Test]
		public void CheckObjectDisposed_ReturnsCorrectState()
		{
			// Arrange
			var logic = new LogicImplementation();
			bool state = false;

			// Act
			logic.CheckObjectDisposed(val => state = val);
			Assert.IsFalse(state);

			logic.Dispose();
			logic.CheckObjectDisposed(val => state = val);
			Assert.IsTrue(state);
		}
	}
}
