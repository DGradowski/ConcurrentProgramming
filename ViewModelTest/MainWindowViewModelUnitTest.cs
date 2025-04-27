//__________________________________________________________________________________________
//
//  Copyright 2024 Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and to get started
//  comment using the discussion panel at
//  https://github.com/mpostol/TP/discussions/182
//__________________________________________________________________________________________

using System;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;
using ViewModel;
using Model;
using ModelIBall = Model.IBall;

namespace ViewModelTest
{
  public class MainWindowViewModelUnitTest
  {
    [Test]
    public void ConstructorTest()
    {
      ModelNullFixture nullModelFixture = new();
      Assert.AreEqual(0, nullModelFixture.Disposed);
      Assert.AreEqual(0, nullModelFixture.Started);
      Assert.AreEqual(0, nullModelFixture.Subscribed);
      using (MainWindowViewModel viewModel = new(nullModelFixture))
      {
        Random random = new Random();
        int numberOfBalls = random.Next(1, 10);
        viewModel.Start(numberOfBalls);
        Assert.IsNotNull(viewModel.Balls);
        Assert.AreEqual(0, nullModelFixture.Disposed);
        Assert.AreEqual(numberOfBalls, nullModelFixture.Started);
        Assert.AreEqual(1, nullModelFixture.Subscribed);
      }
      Assert.AreEqual(1, nullModelFixture.Disposed);
    }

    [Test]
    public void BehaviorTestMethod()
    {
      ModelSimulatorFixture modelSimulator = new();
      MainWindowViewModel viewModel = new(modelSimulator);
      Assert.IsNotNull(viewModel.Balls);
      Assert.AreEqual(0, viewModel.Balls.Count);
      Random random = new Random();
      int numberOfBalls = random.Next(1, 10);
      viewModel.Start(numberOfBalls);
      Assert.AreEqual(numberOfBalls, viewModel.Balls.Count);
      viewModel.Dispose();
      Assert.IsTrue(modelSimulator.Disposed);
      Assert.AreEqual(0, viewModel.Balls.Count);
    }

    #region testing infrastructure

    private class ModelNullFixture : ModelAbstractAPI
    {
      #region Test

      internal int Disposed = 0;
      internal int Started = 0;
      internal int Subscribed = 0;

			public override void Continue()
			{
				throw new NotImplementedException();
			}

			#endregion Test

			#region ModelAbstractApi

			public override void Dispose()
      {
        Disposed++;
      }

			public override void Pause()
			{
				throw new NotImplementedException();
			}

			public override void Start(int numberOfBalls)
      {
        Started = numberOfBalls;
      }

      public override IDisposable Subscribe(IObserver<ModelIBall> observer)
      {
        Subscribed++;
        return new NullDisposable();
      }

      #endregion ModelAbstractApi

      #region private

      private class NullDisposable : IDisposable
      {
        public void Dispose()
        { }
      }

      #endregion private
    }

    private class ModelSimulatorFixture : ModelAbstractAPI
    {
      #region Testing indicators

      internal bool Disposed = false;

      #endregion Testing indicators

      #region ctor

      public ModelSimulatorFixture()
      {
        eventObservable = Observable.FromEventPattern<BallChaneEventArgs>(this, "BallChanged");
      }

      #endregion ctor

      #region ModelAbstractApi fixture

      public override IDisposable? Subscribe(IObserver<ModelIBall> observer)
      {
        return eventObservable?.Subscribe(x => observer.OnNext(x.EventArgs.Ball), ex => observer.OnError(ex), () => observer.OnCompleted());
      }

      public override void Start(int numberOfBalls)
      {
        for (int i = 0; i < numberOfBalls; i++)
        {
          ModelBall newBall = new ModelBall(0, 0) { };
          BallChanged?.Invoke(this, new BallChaneEventArgs() { Ball = newBall });
        }
      }

      public override void Dispose()
      {
        Disposed = true;
      }

			public override void Pause()
			{
				throw new NotImplementedException();
			}

			public override void Continue()
			{
				throw new NotImplementedException();
			}

			#endregion ModelAbstractApi

			#region API

			public event EventHandler<BallChaneEventArgs> BallChanged;

      #endregion API

      #region private

      private IObservable<EventPattern<BallChaneEventArgs>>? eventObservable = null;

      private class ModelBall : ModelIBall
      {
        public ModelBall(double top, double left)
        { }

        #region IBall

        public double Diameter => throw new NotImplementedException();

        public double Top => throw new NotImplementedException();

        public double Left => throw new NotImplementedException();

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion INotifyPropertyChanged

        #endregion IBall
      }

      #endregion private
    }

    #endregion testing infrastructure
  }
}