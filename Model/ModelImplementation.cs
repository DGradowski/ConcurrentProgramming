using System;
using System.Reactive;
using System.Reactive.Linq;

using UnderneathLayerAPI = Logic.LogicAbstractAPI;

namespace Model
{
	internal class ModelImplementation : ModelAbstractAPI
	{
		private bool Disposed = false;
		private readonly IObservable<EventPattern<BallChaneEventArgs>> eventObservable = null;
		private readonly UnderneathLayerAPI layerBellow = null;

		internal ModelImplementation() : this(null)
		{ }

		internal ModelImplementation(UnderneathLayerAPI underneathLayer)
		{
			layerBellow = underneathLayer == null ? UnderneathLayerAPI.GetLogicLayer() : underneathLayer;
			eventObservable = Observable.FromEventPattern<BallChaneEventArgs>(this, "BallChanged");
		}


		public override void Dispose()
		{
			if (Disposed)
				throw new ObjectDisposedException(nameof(Model));
			layerBellow.Dispose();
			Disposed = true;
		}

		public override IDisposable Subscribe(IObserver<Model.IBall> observer)
		{
			return eventObservable.Subscribe(x => observer.OnNext(x.EventArgs.Ball), ex => observer.OnError(ex), () => observer.OnCompleted());
		}

		public override void Start(int numberOfBalls)
		{
			layerBellow.Start(numberOfBalls, StartHandler);
		}

		public event EventHandler<BallChaneEventArgs> BallChanged;

		private void StartHandler(Logic.IPosition position, Logic.IBall ball)
		{
			Ball newBall = new Ball(position.x, position.y, ball) { Diameter = 20.0 };
			BallChanged.Invoke(this, new BallChaneEventArgs() { Ball = newBall });
		}
	}
}
