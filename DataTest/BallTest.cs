using Data;

namespace ModelTest
{
	public class BallTests
	{
		Ball ball;

		[SetUp]
		public void Setup()
		{
			ball = new Ball(1, 5);
		}

		[Test]
		public void GetterTest()
		{
			Assert.That(ball.PosX, Is.EqualTo(1));
			Assert.That(ball.PosY, Is.EqualTo(5));
		}

		[Test]
		public void MoveTest()
		{
			ball.Move(3, 5);
			Assert.That(ball.PosX, Is.EqualTo(4));
			Assert.That(ball.PosY, Is.EqualTo(10));

			ball.Move(-5, -3);
			Assert.That(ball.PosX, Is.EqualTo(-1));
			Assert.That(ball.PosY, Is.EqualTo(7));
		}
	}
}