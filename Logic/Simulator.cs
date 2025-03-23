using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Model;

namespace Logic
{
	public class Simulator
	{
		BallRepository repo;

		public Simulator()
		{
			repo = new BallRepository();
		}

		public Simulator(BallRepository repo)
		{
			this.repo = repo;
		}

		public void AddBall(int x, int y)
		{
			repo.Add(new Ball(x, y));
		}

		public void SimulateBalls()
		{
			Random rnd = new Random();
			for (int i = 0; i < repo.BallNumber; i++)
			{
				Ball ball = repo.GetBall(i);
				int x = rnd.Next(-5, 5);
				int y = rnd.Next(-5, 5);
				ball.Move(x, y);
			}
		}

        public List<Ball> GetAllBalls()
        {
            List<Ball> balls = new List<Ball>();
            for (int i = 0; i < repo.BallNumber; i++)
            {
                balls.Add(repo.GetBall(i));
            }
            return balls;
        }

    }
}
