using Geerten.Movement.Bodies;
using Geerten.Movement.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geerten.Movement.Geometry
{
    public class Direction
    {
        public static readonly Direction Zero = new Direction(0.0d);

        private radian _inRadians;

        public radian InRadians
        {
            get { return _inRadians; }
            private set { _inRadians = value; }
        }

        public double InDegrees
        {
            get { return _inRadians.toDouble() / Math.PI * 180.0d; }
        }

        private Direction(radian inRadians)
        {
            this.InRadians = inRadians;
        }
        private Direction(double radians) :this(new radian(radians)) { }

        public Direction Absolute()
        {
            return new Direction(this.InRadians.Absolute());
        }

        public static Direction FromRadian(radian value)
        {
            return new Direction(value);
        }

        public static Direction FromDegrees(double degrees)
        {
            return new Direction(new radian(degrees / 180.0d * Math.PI));
        }

        // 0 should be straight up, so positive Y
        public static Direction Calculate(ILocation from, ILocation to)
        {
            double deltaX = to.X - from.X;
            double deltaY = to.Y - from.Y;

            return Direction.FromRadian(new radian(Math.Atan2(deltaX, deltaY)));
        }

        public static Direction operator -(Direction direction)
        {
            return Direction.FromRadian(-direction.InRadians);
        }

        public static Direction operator +(Direction directionOne, Direction directionTwo)
        {
            return Direction.FromRadian(directionOne.InRadians + directionTwo.InRadians);
        }

        public static Direction operator -(Direction directionOne, Direction directionTwo)
        {
            return Direction.FromRadian(directionOne.InRadians - directionTwo.InRadians);
        }

        public static bool operator ==(Direction? directionOne, Direction? directionTwo)
        {
            var oneIsNull = ReferenceEquals(directionOne, null);
            var twoIsNull = ReferenceEquals(directionTwo, null);

#nullable disable // linter is wrong, one and two cannot be null on line 89
            if (oneIsNull && twoIsNull) return true;
            else if (oneIsNull || twoIsNull) return false;
            else return directionOne._inRadians == directionTwo._inRadians;
#nullable enable
        }

        public static bool operator !=(Direction? directionOne, Direction? directionTwo)
        {
            return !(directionOne == directionTwo);
        }

        public override int GetHashCode()
        {
            return _inRadians.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;

            var other = obj as Direction;
            if (other == null) return false;

            return other._inRadians == this._inRadians;
        }

        public bool DifferenceLessThan(Direction other, radian allowedDifference)
        {
            var difference = (this - other).Absolute();

            return difference.InRadians < allowedDifference;
        }

        public bool DifferenceLessThan(Direction other, double degreesDifferenceAllowed)
        {
            var difference = (this - other).Absolute();

            return difference.InDegrees < degreesDifferenceAllowed;
        }

        public override string ToString()
        {
            return string.Format("Direction: {0}", InRadians);
        }
    }
}
