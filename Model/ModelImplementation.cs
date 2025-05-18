using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;

using UnderneathLayerAPI = Logic.LogicAbstractAPI;

namespace Model
{
	public class ModelImplementation : ModelAbstractAPI
	{
		private bool Disposed = false;
		private readonly IObservable<EventPattern<BallChaneEventArgs>> eventObservable = null;
		private readonly UnderneathLayerAPI layerBellow = null;

		public ModelImplementation() : this(null)
		{ }

		public ModelImplementation(UnderneathLayerAPI underneathLayer)
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

		private void StartHandler(Logic.Ball ball)
		{
			Ball newBall = new Ball(ball.Position.X, ball.Position.Y, ball) { Diameter = 20.0 };
			BallChanged.Invoke(this, new BallChaneEventArgs() { Ball = newBall });
		}

		public override void Pause()
		{
			UnderneathLayerAPI.GetLogicLayer().Pause();
		}

		public override void Continue()
		{
			UnderneathLayerAPI.GetLogicLayer().Continue();
		}

		[Conditional("DEBUG")]
		public void CheckObjectDisposed(Action<bool> returnInstanceDisposed)
		{
			returnInstanceDisposed(Disposed);
		}

		[Conditional("DEBUG")]
		public void CheckUnderneathLayerAPI(Action<UnderneathLayerAPI> returnNumberOfBalls)
		{
			returnNumberOfBalls(layerBellow);
		}

		[Conditional("DEBUG")]
		public void CheckBallChangedEvent(Action<bool> returnBallChangedIsNull)
		{
			returnBallChangedIsNull(BallChanged == null);
		}
	}
}
