using Model;

namespace Data
{
	public class BallRepository
	{
		private List<Ball> balls = new List<Ball>();

		public BallRepository()
		{

		}

		public void Add(Ball ball)
		{
			balls.Add(ball);
		}


		public Ball GetBall(int id)
		{
			return balls[id];
		}

		public int BallNumber { get => balls.Count; }
	}
}
