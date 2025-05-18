using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
	public class Ball : Data.IBall
	{
		private Vector position;
		private Vector velocity;
		private int diameter;

		private readonly object locked = new();
		private CancellationTokenSource? cts;

		public Ball(Vector position, Vector velocity, int diameter)
		{
			this.position = position;
			this.velocity = velocity;
			this.diameter = diameter;
		}

		public event EventHandler<Vector>? NewPositionNotification;

		public Vector Position
		{
			get { lock (locked) return position; }
		}

		public int Diameter { get => diameter; }

		public Vector Velocity { get => velocity; set => velocity = value; }

		public event PropertyChangedEventHandler? PropertyChanged;

		private void RaiseNewPositionChangeNotification()
		{
			NewPositionNotification?.Invoke(this, Position);
		}

		public void Move()
		{
			lock (locked)
			{
				position = new Vector(Position.X + Velocity.X, Position.Y + Velocity.Y);
			}
			RaiseNewPositionChangeNotification();
		}

		public void StartMovingAsync()
		{
			cts = new CancellationTokenSource();
			var token = cts.Token;

			Task.Run(async () =>
			{
				while (!token.IsCancellationRequested)
				{
					BounceOffWall(700 - 8, 500 - 8);
					Move();
					await Task.Delay(100, token); // delay 100ms
				}
			}, token);
		}

		public void StopMoving()
		{
			cts?.Cancel();
		}

		public void BounceOffWall(int boardWidth, int boardHeight)
		{
			lock (locked)
			{
				if (position.X <= 0 || position.X + diameter >= boardWidth)
				{
					velocity = new Vector(-velocity.X, velocity.Y);
				}
				if (position.Y <= 0 || position.Y + diameter >= boardHeight)
				{
					velocity = new Vector(velocity.X, -velocity.Y);
				}
			}
		}

		public object GetLock() => locked;
	}
}
