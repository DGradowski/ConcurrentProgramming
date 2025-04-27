using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	public class DataImplementation : DataAbstractAPI
	{
		private bool Disposed = false;

		private readonly Timer MoveTimer;
		private Random RandomGenerator = new();
		private List<Ball> BallsList = [];

		public DataImplementation()
		{
			MoveTimer = new Timer(Move, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(100));
		}

		public override void Start(int numberOfBalls, Action<IVector, IBall> upperLayerHandler)
		{
			if (Disposed)
				throw new ObjectDisposedException(nameof(DataImplementation));
			if (upperLayerHandler == null)
				throw new ArgumentNullException(nameof(upperLayerHandler));
			Random random = new Random();
			for (int i = 0; i < numberOfBalls; i++)
			{
				Vector startingPosition = new Vector(random.Next(100, 400 - 100), random.Next(100, 400 - 100));
				Ball newBall = new Ball(startingPosition, startingPosition);
				upperLayerHandler(startingPosition, newBall);
				BallsList.Add(newBall);
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!Disposed)
			{
				if (disposing)
				{
					MoveTimer.Dispose();
					BallsList.Clear();
				}
				Disposed = true;
			}
			else
				throw new ObjectDisposedException(nameof(DataImplementation));
		}

		public override void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void Move(object? x)
		{
			foreach (Ball item in BallsList)
				item.Move(item.Velocity);
		}
	}
}
