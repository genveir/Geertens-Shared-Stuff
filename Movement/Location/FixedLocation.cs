using Geerten.Movement.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geerten.Movement.Location
{
    public class FixedLocation : ILocation
    {
        public static FixedLocation Zero = new FixedLocation(0, 0);

        public long X { get; }
        public long Y { get; }

        public FixedLocation(long X, long Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public FixedLocation(ILocation otherLocation) : this(otherLocation.X, otherLocation.Y) { }
        public FixedLocation(ILocation startingPoint, vector difference) : this(
            startingPoint.X + difference.XOffset,
            startingPoint.Y + difference.YOffset
        ) { }
        public FixedLocation(ILocation location, Direction direction, Distance distance) : this(location, new vector(direction, distance)) { }

        public override string ToString()
        {
            return string.Format("FixedLocation: ({0}, {1})", X, Y);
        }
    }
}
