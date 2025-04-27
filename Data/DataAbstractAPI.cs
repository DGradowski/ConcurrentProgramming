using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	public abstract class DataAbstractAPI : IDisposable
	{
		public static DataAbstractAPI GetDataLayer()
		{
			return instance.Value;
		}

		public abstract void Start(int numberOfBalls, Action<IVector, IBall> upperLayerHandler);

		public abstract void Dispose();


		private static Lazy<DataAbstractAPI> instance = new Lazy<DataAbstractAPI>(() => new DataImplementation());
	}

	public interface IVector
	{
		double x { get; init; }

		double y { get; init; }
	}

	public interface IBall
	{
		event EventHandler<IVector> NewPositionNotification;

		IVector Velocity { get; set; }
	}
}
