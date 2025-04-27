using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public abstract class ModelAbstractAPI : IObservable<IBall>, IDisposable
	{
		public static ModelAbstractAPI CreateModel()
		{
			return modelInstance.Value;
		}

		public abstract void Start(int numberOfBalls);

		public abstract IDisposable Subscribe(IObserver<IBall> observer);

		public abstract void Dispose();

		public abstract void Pause();

		public abstract void Continue();

		private static Lazy<ModelAbstractAPI> modelInstance = new Lazy<ModelAbstractAPI>(() => new ModelImplementation());
	}

	public interface IBall : INotifyPropertyChanged
	{
		double Top { get; }
		double Left { get; }
		double Diameter { get; }
	}

	public class BallChaneEventArgs : EventArgs
	{
		public IBall Ball { get; init; }
	}
}
