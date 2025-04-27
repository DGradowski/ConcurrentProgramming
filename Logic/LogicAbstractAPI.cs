using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
	public abstract class LogicAbstractAPI : IDisposable
	{
		public static LogicAbstractAPI GetLogicLayer()
		{
			return modelInstance.Value;
		}


		public static readonly Dimensions GetDimensions = new(10.0, 10.0, 10.0);

		public abstract void Start(int numberOfBalls, Action<IPosition, IBall> upperLayerHandler);

		public abstract void Dispose();

		private static Lazy<LogicAbstractAPI> modelInstance = new Lazy<LogicAbstractAPI>(() => new LogicImplementation());

		public abstract void Pause();

		public abstract void Continue();
	}

	public record Dimensions(double BallDimension, double TableHeight, double TableWidth);

	public interface IPosition
	{
		double x { get; init; }
		double y { get; init; }
	}

	public interface IBall
	{
		event EventHandler<IPosition> NewPositionNotification;
	}
}
