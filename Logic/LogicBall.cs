using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
	internal class Ball : IBall
	{
		public Ball(Data.IBall ball)
		{
			ball.NewPositionNotification += RaisePositionChangeEvent;
		}

		#region IBall

		public event EventHandler<IPosition>? NewPositionNotification;

		#endregion IBall

		#region private

		private void RaisePositionChangeEvent(object? sender, Data.IVector e)
		{
			NewPositionNotification?.Invoke(this, new Position(e.x, e.y));
		}
	}
}
