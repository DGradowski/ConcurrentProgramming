using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnderneathLayerAPI = Data.DataAbstractAPI;

namespace Logic
{
	public class LogicImplementation : LogicAbstractAPI
	{
		private bool Disposed = false;
		private readonly UnderneathLayerAPI layerBellow;

		public LogicImplementation() : this(null) { }

		public LogicImplementation(UnderneathLayerAPI? underneathLayer)
		{
			layerBellow = underneathLayer == null ? UnderneathLayerAPI.GetDataLayer() : underneathLayer;
		}


		public override void Dispose()
		{
			if (Disposed)
				throw new ObjectDisposedException(nameof(LogicImplementation));
			layerBellow.Dispose();
			Disposed = true;
		}

		public override void Start(int numberOfBalls, Action<IPosition, IBall> upperLayerHandler)
		{
			if (Disposed)
				throw new ObjectDisposedException(nameof(LogicImplementation));
			if (upperLayerHandler == null)
				throw new ArgumentNullException(nameof(upperLayerHandler));
			layerBellow.Start(
				numberOfBalls, 
				(startingPosition, databall) => 
					upperLayerHandler(new Position(startingPosition.x, startingPosition.y), new Ball(databall))
				);
		}

		public override void Pause()
		{
			UnderneathLayerAPI.GetDataLayer().Pause();
		}

		public override void Continue()
		{
			UnderneathLayerAPI.GetDataLayer().Continue();
		}
	}
}
