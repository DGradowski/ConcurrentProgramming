using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
	public class Ball : IBall
	{
		public Ball(Data.IBall ball)
		{
			ball.NewPositionNotification += RaisePositionChangeEvent;
		}

		public event EventHandler<IPosition>? NewPositionNotification;

		private void RaisePositionChangeEvent(object? sender, Data.IVector e)
		{
			NewPositionNotification?.Invoke(this, new Position(e.x, e.y));
		}
	}
}
