using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class Ball : IBall
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private double TopBackingField;
		private double LeftBackingField;

		public Ball(double top, double left, Logic.IBall underneathBall)
		{
			TopBackingField = top;
			LeftBackingField = left;
			underneathBall.NewPositionNotification += NewPositionNotification;
		}

		public double Top
		{
			get { return TopBackingField; }
			set
			{
				if (TopBackingField == value)
					return;
				TopBackingField = value;
				RaisePropertyChanged();
			}
		}

		public double Left
		{
			get { return LeftBackingField; }
			set
			{
				if (LeftBackingField == value)
					return;
				LeftBackingField = value;
				RaisePropertyChanged();
			}
		}

		public double Diameter { get; init; } = 0;

		private void NewPositionNotification(object sender, IPosition e)
		{
			Top = e.y; Left = e.x;
		}

		private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
