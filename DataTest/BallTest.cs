using Data;

namespace DataTest
{
	public class BallTests
	{
		Ball ball;

		[SetUp]
		public void Setup()
		{
			ball = new Ball(new Vector(10, 50), new Vector(5, 15));
		}

		[Test]
		public void GetterTest()
		{
			Assert.That(ball.Position.x, Is.EqualTo(10));
			Assert.That(ball.Position.y, Is.EqualTo(50));

			Assert.That(ball.Velocity.x, Is.EqualTo(5));
			Assert.That(ball.Velocity.y, Is.EqualTo(15));
		}

		[Test]
		public void MoveTest()
		{
			ball.Move(100, 100);
			Assert.That(ball.Position.x, Is.EqualTo(15));
			Assert.That(ball.Position.y, Is.EqualTo(65));
			ball.Move(100, 100);
			Assert.That(ball.Position.x, Is.EqualTo(20));
			Assert.That(ball.Position.y, Is.EqualTo(64));
		}
	}
}