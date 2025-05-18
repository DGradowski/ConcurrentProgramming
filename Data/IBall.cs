using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	public interface IBall : INotifyPropertyChanged
	{
		void Move();
		Vector Position { get; }
		int Diameter { get; }
		Vector Velocity { get; set; }
	}
}
