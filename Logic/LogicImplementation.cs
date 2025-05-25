using Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
	public class LogicImplementation : LogicAbstractAPI
	{
		private List<Ball> balls = new List<Ball>();
		private int boardWidth = 700 - 8;//700 - 28;
		private int boardHeight = 500 - 8;//500 - 29;
		private bool paused = false;
		private bool Disposed = false;

		public override void Dispose()
		{
			if (Disposed) throw new ObjectDisposedException(nameof(LogicImplementation));
			foreach (var b in balls) b.StopMoving();
			collisionTokenSource?.Cancel();
			Disposed = true;
		}

		public override void Pause()
		{
			paused = true;
			foreach (var b in balls) b.StopMoving();
		}

		public override void Continue()
		{
			if (!paused) return;
			paused = false;
		}

		public override void Start(int numberOfBalls, Action<Logic.Ball> upperLayerHandler)
		{
			if (Disposed) throw new ObjectDisposedException(nameof(LogicImplementation));
			Random random = new Random();
			balls.Clear();
			for (int i = 0; i < numberOfBalls; i++)
			{
				int posX = random.Next(boardWidth - 30);
				int posY = random.Next(boardHeight - 30);
				int velX = random.Next(7) - 3;
				int velY = random.Next(7) - 3;
				Data.Vector pos = new Data.Vector(posX, posY);
				Data.Vector vel = new Data.Vector(velX, velY);

				Ball ball = new Ball(pos, vel, 20);
				balls.Add(ball);
				upperLayerHandler(ball);
				Task.Run(async () =>
				{
					while (!Disposed)
					{
						if (!paused)
						{
							ball.Move();
							ball.BounceOffWall(boardWidth, boardHeight);
						}
						await Task.Delay(30); // 30 ms = ~33 FPS
					}
				});
			}
			StartCollisionDetection();
		}

		public override void Stop()
		{
			foreach (var b in balls)
			{
				b.StopMoving();
			}
			balls.Clear();
			collisionTokenSource?.Cancel();
		}

		private void HandleCollisions()
		{
			for (int i = 0; i < balls.Count; i++)
			{
				for (int j = i + 1; j < balls.Count; j++)
				{
					Ball b1 = balls[i];
					Ball b2 = balls[j];
					if (IsColliding(b1, b2))
					{
						ResolveCollision(b1, b2);
					}
				}
			}
		}

		private bool IsColliding(Ball b1, Ball b2)
		{
			var dx = b1.Position.X - b2.Position.X;
			var dy = b1.Position.Y - b2.Position.Y;
			var distance = Math.Sqrt(dx * dx + dy * dy);
			return distance < b1.Diameter; // Średnica = suma promieni (bo są równe)
		}

		private void ResolveCollision(Ball b1, Ball b2)
		{

			object lock1 = b1.GetLock();
			object lock2 = b2.GetLock();

			if (lock1.GetHashCode() < lock2.GetHashCode())
				lock (lock1)
					lock (lock2)
						ResolveCollisionInternal(b1, b2);
			else
				lock (lock2)
					lock (lock1)
						ResolveCollisionInternal(b1, b2);
		}

		private void ResolveCollisionInternal(Ball b1, Ball b2)
		{

			Vector pos1 = b1.Position;
			Vector pos2 = b2.Position;
			Vector vel1 = b1.Velocity;
			Vector vel2 = b2.Velocity;

			double dx = pos2.X - pos1.X;
			double dy = pos2.Y - pos1.Y;
			double dist = Math.Sqrt(dx * dx + dy * dy);
			if (dist == 0) return; // kulki się nakładają

			// Jednostkowy wektor normalny
			double nx = dx / dist;
			double ny = dy / dist;

			// Jednostkowy wektor styczny
			double tx = -ny;
			double ty = nx;

			// Składowe prędkości
			double v1n = vel1.X * nx + vel1.Y * ny;
			double v1t = vel1.X * tx + vel1.Y * ty;
			double v2n = vel2.X * nx + vel2.Y * ny;
			double v2t = vel2.X * tx + vel2.Y * ty;

			// Zamiana składowych normalnych (równa masa)
			double v1nAfter = v2n;
			double v2nAfter = v1n;

			// Przekształcenie na wektory prędkości
			double v1x = v1nAfter * nx + v1t * tx;
			double v1y = v1nAfter * ny + v1t * ty;
			double v2x = v2nAfter * nx + v2t * tx;
			double v2y = v2nAfter * ny + v2t * ty;

			// Ustawienie nowych prędkości jako int (zaokrąglenie)
			b1.Velocity = new Vector((int)Math.Round(v1x), (int)Math.Round(v1y));
			b2.Velocity = new Vector((int)Math.Round(v2x), (int)Math.Round(v2y));
		}

		CancellationTokenSource collisionTokenSource;

		private void StartCollisionDetection()
		{
			collisionTokenSource = new CancellationTokenSource();
			var token = collisionTokenSource.Token;

			Task.Run(async () =>
			{
				while (!token.IsCancellationRequested)
				{
					HandleCollisions();
					await Task.Delay(100, token);
				}
			});
		}

		[Conditional("DEBUG")]
		public void CheckObjectDisposed(Action<bool> returnInstanceDisposed)
		{
			returnInstanceDisposed(Disposed);
		}
	}
}
