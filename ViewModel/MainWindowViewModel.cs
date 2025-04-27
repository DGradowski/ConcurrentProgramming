using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Model;

namespace ViewModel
{
	public class MainWindowViewModel : ViewModelBase, IDisposable
	{
		private IDisposable Observer = null;
		private ModelAbstractAPI ModelLayer;
		private bool Disposed = false;

		public ICommand StartOnLoadedCommand { get; }
		public ICommand PauseCommand { get; }
		public ICommand ContinueCommand { get; }

		public ObservableCollection<Model.IBall> Balls { get; } = new ObservableCollection<Model.IBall>();
	
		public MainWindowViewModel() : this(null)
		{
			StartOnLoadedCommand = new RelayCommand(() => {
				Start(5);
			}); // Automatyczny start po załadowaniu
			PauseCommand = new RelayCommand(PauseBalls);

			ContinueCommand = new RelayCommand(ContinueBalls);
		}

		public MainWindowViewModel(ModelAbstractAPI modelLayerAPI)
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

		private void PauseBalls()
		{
			ModelLayer.Pause();
		}

		private void ContinueBalls()
		{
			ModelLayer.Continue();
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
