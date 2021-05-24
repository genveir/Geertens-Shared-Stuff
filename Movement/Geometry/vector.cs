using Geerten.Movement.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geerten.Movement.Geometry
{
    public struct vector
    {
        public static vector Zero = new vector(0, 0);

        public readonly long XOffset { get; }
        public readonly long YOffset { get; }

        public readonly Direction Direction { get; }
        public readonly Distance Distance { get; }

        public vector(long XOffset, long YOffset)
        {
            this.XOffset = XOffset;
            this.YOffset = YOffset;

            var vectorLoc = new FixedLocation(XOffset, YOffset);

            this.Direction = Direction.Calculate(FixedLocation.Zero, vectorLoc);
            this.Distance = Distance.Calculate(FixedLocation.Zero, vectorLoc);
        }

        public vector(Direction direction, Distance distance)
        {
            this.Direction = direction;
            this.Distance = distance;

            this.XOffset = (long)(Math.Sin(direction.InRadians.toDouble()) * distance.Value);
            this.YOffset = (long)(Math.Cos(direction.InRadians.toDouble()) * distance.Value);
        }

        public static vector operator -(vector vector)
        {
            return new vector(-vector.XOffset, -vector.YOffset);
        }

        public static vector operator +(vector first, vector second)
        {
            return new vector(first.XOffset + second.XOffset, first.YOffset + second.YOffset);
        }

        public static vector operator -(vector first, vector second)
        {
            return new vector(first.XOffset - second.XOffset, first.YOffset - second.YOffset);
        }

        public static vector operator /(vector vector, double divider)
        {
            return new vector((long)(vector.XOffset / divider), (long)(vector.YOffset / divider));
        }

        public static vector operator *(vector vector, long multiplier)
        {
            return new vector(vector.XOffset * multiplier, vector.YOffset * multiplier);
        }
        public static vector operator *(long multiplier, vector vector) => vector * multiplier;
        public static vector operator *(vector vector, double multiplier) => vector * (long)multiplier;
        public static vector operator *(double multiplier, vector vector) => vector * (long)multiplier;

        public static bool operator ==(vector first, vector second)
        {
            return first.XOffset == second.XOffset &&
                first.YOffset == second.YOffset;
        }

        public static bool operator !=(vector first, vector second)
        {
            return !(first == second);
        }

        public static vector Calculate(ILocation from, ILocation to)
        {
            return new vector(to.X - from.X, to.Y - from.Y);
        }

        public override int GetHashCode()
        {
            return XOffset.GetHashCode() + YOffset.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, null)) return false;

            var other = (vector)obj;
            return this.XOffset == other.XOffset &&
                this.YOffset == other.YOffset;
        }

        public override string ToString()
        {
            return string.Format("vector ({0}, {1})", XOffset, YOffset);
        }
    }
}