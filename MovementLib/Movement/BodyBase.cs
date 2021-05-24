using Geerten.MovementLib.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geerten.MovementLib.Movement
{
    public abstract class BodyBase : IBodyWithHeading, IMovingBody
    {
        private BodyBaseBehavior bodyBehavior;

        private vector _movement;
        public vector Movement
        {
            get => _movement;
            private set
            {
                _movement = value;
                if (bodyBehavior.MaximumSpeed != null)
                {
                    var overspeed = _movement.Distance - bodyBehavior.MaximumSpeed;
                    if (overspeed > Distance.Zero) _movement += new vector(_movement.Direction, -_movement.Distance);
                }
            }
        }

        public ILocation Location { get; private set; }

        public Direction Heading { get; private set; }

        public Direction Bearing => Movement.Direction;

        public BodyBase(ILocation location, BodyBaseBehavior behavior, Direction heading, vector movement)
        {
            this.Location = location;
            this.bodyBehavior = behavior;
            this.Heading = heading;
            this.Movement = movement;
        }

        public BodyBase(ILocation location) : this(location, BodyBaseBehavior.Default, Direction.Zero, vector.Zero) { }
        public BodyBase(ILocation location, BodyBaseBehavior behavior) : this(location, behavior, Direction.Zero, vector.Zero) { }
        public BodyBase(ILocation location, BodyBaseBehavior behavior, Direction heading) : this(location, behavior, heading, vector.Zero) { }
        public BodyBase(ILocation location, BodyBaseBehavior behavior, vector movement) : this(location, behavior, Direction.Zero, movement) { }
        public BodyBase(ILocation location, BodyBaseBehavior behavior, Direction bearing, Distance speed) : this(location, behavior, bearing, new vector(bearing, speed)) { }
        public BodyBase(ILocation location, BodyBaseBehavior behavior, Direction heading, Direction bearing, Distance speed) : this(location, behavior, heading, new vector(bearing, speed)) { }
        public BodyBase(ILocation location, Direction heading) : this(location, BodyBaseBehavior.Default, heading, vector.Zero) { }
        public BodyBase(ILocation location, vector movement) : this(location, BodyBaseBehavior.Default, Direction.Zero, movement) { }
        public BodyBase(ILocation location, Direction heading, vector movement) : this(location, BodyBaseBehavior.Default, heading, movement) { }
        public BodyBase(ILocation location, Direction bearing, Distance speed) : this(location, BodyBaseBehavior.Default, bearing, new vector(bearing, speed)) { }
        public BodyBase(ILocation location, Direction heading, Direction bearing, Distance speed) : this(location, BodyBaseBehavior.Default, heading, new vector(bearing, speed)) { }

        public void Accelerate(vector accelerationVector)
        {
            this.Movement = this.Movement + accelerationVector;
        }
        public void Accelerate(Direction direction, Distance amount) => Accelerate(new vector(direction, amount));
        public void AccelerateAlongBearing(Distance amount) => Accelerate(Bearing, amount);
        public void AccelerateAlongHeading(Distance amount) => Accelerate(Heading, amount);

        public void DecelerateAlongBearing(Distance amount) => Accelerate(Bearing, -amount);
        public void DecelerateAlongHeading(Distance amount) => Accelerate(Heading, -amount);

        public void TurnLeft(Direction amount) => this.Heading -= amount;
        public void TurnLeft(radian amount) => TurnLeft(Direction.FromRadian(amount));

        public void TurnRight(Direction amount) => this.Heading += amount;
        public void TurnRight(radian amount) => TurnRight(Direction.FromRadian(amount));
    }
}
