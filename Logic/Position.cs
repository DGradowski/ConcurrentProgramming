using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
	public record Position : IPosition
	{
		public double x { get; init; }
		public double y { get; init; }

		public Position(double x, double y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
