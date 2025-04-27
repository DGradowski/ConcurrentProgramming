using Data;
using System.Numerics;

namespace Data
{
	internal class Ball : IBall
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

		public event EventHandler<IVector> NewPositionNotification;

		private void RaiseNewPositionChangeNotification()
		{
			NewPositionNotification?.Invoke(this, Position);
		}

		public void Move(Vector velocity)
		{
			Position = new Vector(Position.x + velocity.x, Position.y + velocity.y);
			RaiseNewPositionChangeNotification();
		}
	}
}
