using Data;
using System.Numerics;

namespace Data
{
	public class Ball : IBall
	{
		private Vector position;
		private Vector velocity;

		public Ball(Vector initialPosition, Vector initialVelocity)
		{
			Position = initialPosition;
			Velocity = initialVelocity;
		}

		public Vector Position { get => position; set => position = value; }
		public Vector Velocity { get => velocity; set => velocity = value; }
		IVector IBall.Velocity { get => Velocity; set => throw new NotImplementedException(); }

		public void SetVelocity(double x, double y)
		{
			velocity = new Vector(x, y);
		}

		public event EventHandler<IVector> NewPositionNotification;

		private void RaiseNewPositionChangeNotification()
		{
			NewPositionNotification?.Invoke(this, Position);
		}

		public void Move(double canvasWidth, double canvasHeight)
		{
			Position = new Vector(Position.x + velocity.x, Position.y + velocity.y);
			if (Position.x + 10 > canvasWidth - 18)
			{
				Position = new Vector((canvasWidth - 28) - (Position.x - (canvasWidth - 28)), Position.y);
				Velocity = new Vector(Velocity.x * -1, Velocity.y);
			}
			if (Position.y + 10 > canvasHeight - 18)
			{
				Position = new Vector(Position.x, (canvasHeight - 28) - (Position.y - (canvasHeight - 28)));
				Velocity = new Vector(Velocity.x, Velocity.y * -1);
			}
			if (Position.x < 0)
			{
				Position = new Vector(Position.x * -1, Position.y);
				Velocity = new Vector(Velocity.x * -1, Velocity.y);
			}
			if (Position.y < 0)
			{
				Position = new Vector(Position.x, Position.y * -1);
				Velocity = new Vector(Velocity.x, Velocity.y * -1);
			}
			RaiseNewPositionChangeNotification();
		}
	}
}
