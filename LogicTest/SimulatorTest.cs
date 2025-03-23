using Logic;
using Data;
using Model;

namespace LogicTest
{
	public class SimulatorTests
	{
		Simulator sim;
		BallRepository repo;

		[SetUp]
		public void Setup()
		{
			repo = new BallRepository();
			sim = new Simulator(repo);
		}

		[Test]
		public void Test()
		{
			Assert.That(repo.BallNumber, Is.EqualTo(0));

			sim.AddBall(0, 1);

			Assert.That(repo.BallNumber, Is.EqualTo(1));

			List<Ball> balls = sim.GetAllBalls();

			Assert.That(balls.Count, Is.EqualTo(1));
		}
	}
}