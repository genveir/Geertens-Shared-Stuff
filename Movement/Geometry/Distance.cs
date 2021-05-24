using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geerten.Movement.Geometry
{
    public class Distance : IComparable
    {
        public static Distance Zero = new Distance(0.0d);

        public double Value { get; set; }

        public Distance(double value)
        {
            Value = value;
        }

        public static Distance Calculate(ILocation locationOne, ILocation locationTwo)
        {
            long XLeg = locationOne.X - locationTwo.X;

            long YLeg = locationOne.Y - locationTwo.Y;

            if (XLeg == 0)
                if (YLeg != 0) return new Distance(Math.Abs(YLeg));
                else return new Distance(0);

            var ratio = (double)YLeg / (double)XLeg;
            var hypot = Math.Abs(XLeg) * Math.Sqrt(1 + ratio * ratio);

            return new Distance(hypot);
        }

        public static Distance operator -(Distance distance)
        {
            return new Distance(-distance.Value);
        }

        public static Distance operator +(Distance first, Distance second)
        {
            return new Distance(first.Value + second.Value);
        }

        public static Distance operator -(Distance first, Distance second)
        {
            return new Distance(first.Value - second.Value);
        }

        public static Distance operator *(Distance first, Distance second)
        {
            return new Distance(first.Value * second.Value);
        }

        public static Distance operator /(Distance first, Distance second)
        {
            return new Distance(first.Value / second.Value);
        }

        public static bool operator >(Distance first, Distance second)
        {
            return first.Value > second.Value;
        }

        public static bool operator <(Distance first, Distance second)
        {
            return first.Value < second.Value;
        }

        public static bool operator <=(Distance first, Distance second)
        {
            return first.Value <= second.Value;
        }

        public static bool operator >=(Distance first, Distance second)
        {
            return first.Value >= second.Value;
        }

        public static bool operator ==(Distance? first, Distance? second)
        {
            var firstIsNull = ReferenceEquals(first, null);
            var secondIsNull = ReferenceEquals(second, null);

#nullable disable // linter is wrong, first and second cannot be null on line 74
            if (firstIsNull && secondIsNull) return true;
            else if (firstIsNull || secondIsNull) return false;
            else return first.Value == second.Value;
#nullable enable
        }

        public static bool operator !=(Distance? first, Distance? second)
        {
            return !(first == second);
        }

        public override string ToString()
        {
            return string.Format("Distance: {0}", Value);
        }

        public override int GetHashCode()
        {
            return (int)Value;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
#nullable disable
            var other = obj as Distance;

            if (other == null) return false;
#nullable enable
            return other.Value == this.Value;
        }

        public bool Equals(Distance other, int precision)
        {
            var roundedValue = Math.Round(Value, precision);
            var otherValue = Math.Round(other.Value, precision);

            return roundedValue == otherValue;
        }

        public int CompareTo(object? obj)
        {
            if (obj is Distance)
            {
                var otherDistance = (Distance)obj;

                return Value.CompareTo(otherDistance.Value);
            }
            else
                throw new ArgumentException("object is not a distance");
        }
    }
}
