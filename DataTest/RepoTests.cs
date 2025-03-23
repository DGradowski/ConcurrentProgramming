using Data;
using Model;

namespace DataTest
{
	public class RepoTests
	{
		[Test]
		public void DataTest()
		{
			BallRepository repo = new BallRepository();
			Assert.That(repo.BallNumber , Is.EqualTo(0));

			Ball ball = new Ball(1, 1);

			repo.Add(ball);

			Assert.That(repo.BallNumber, Is.EqualTo(1));
		
			Assert.That(repo.GetBall(0), Is.EqualTo(ball));
		}
	}
}