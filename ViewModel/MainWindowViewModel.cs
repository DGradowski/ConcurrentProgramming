using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ModelIBall = Model.IBall;

namespace ViewModel
{
	internal class MainWindowViewModel : ViewModelBase, IDisposable
	{
		private IDisposable Observer = null;
		private ModelAbstractAPI ModelLayer;
		private bool Disposed = false;

		public ObservableCollection<Model.IBall> Balls { get; } = new ObservableCollection<Model.IBall>();

		public MainWindowViewModel() : this(null)
		{ }

		internal MainWindowViewModel(ModelAbstractAPI modelLayerAPI)
		{
			ModelLayer = modelLayerAPI == null ? ModelAbstractAPI.CreateModel() : modelLayerAPI;
			Observer = ModelLayer.Subscribe<Model.IBall>(x => Balls.Add(x));
		}

		public void Start(int numberOfBalls)
		{
			if (Disposed)
				throw new ObjectDisposedException(nameof(MainWindowViewModel));
			ModelLayer.Start(numberOfBalls);
			Observer.Dispose();
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!Disposed)
			{
				if (disposing)
				{
					Balls.Clear();
					Observer.Dispose();
					ModelLayer.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				Disposed = true;
			}
		}

		public void Dispose()
		{
			if (Disposed)
				throw new ObjectDisposedException(nameof(MainWindowViewModel));
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
