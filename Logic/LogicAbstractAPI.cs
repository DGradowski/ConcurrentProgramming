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

		public abstract void Start(int numberOfBalls, Action<Logic.Ball> upperLayerHandler);

		public abstract void Dispose();

		private static Lazy<LogicAbstractAPI> modelInstance = new Lazy<LogicAbstractAPI>(() => new LogicImplementation());

		public abstract void Stop();

		public abstract void Continue();

		public abstract void Pause();
	}
}
