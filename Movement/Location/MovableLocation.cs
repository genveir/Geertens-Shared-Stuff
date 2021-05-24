using Geerten.Movement.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geerten.Movement.Location
{
    public class MovableLocation : ILocation
    {
        public static MovableLocation Zero = new MovableLocation(0, 0);

        public long X { get; set; }
        public long Y { get; set; }

        public MovableLocation(long X, long Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public MovableLocation(ILocation otherLocation) : this(otherLocation.X, otherLocation.Y) { }
        public MovableLocation(ILocation startingPoint, vector difference) : this(
            startingPoint.X + difference.XOffset,
            startingPoint.Y + difference.YOffset
        )
        { }
        public MovableLocation(ILocation location, Direction direction, Distance distance) : this(location, new vector(direction, distance)) { }

        public void Move(vector difference)
        {
            this.X += difference.XOffset;
            this.Y += difference.YOffset;
        }
        public void Move(Direction direction, Distance distance) => Move(new vector(direction, distance));

        public override string ToString()
        {
            return string.Format("MovableLocation: ({0}, {1})", X, Y);
        }
    }
}
