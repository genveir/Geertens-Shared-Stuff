﻿using Geerten.MovementLib.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geerten.MovementLib.Movement
{
    public abstract class BodyBase : IBodyWithHeading, IMovingBody
    {
        protected BodyBaseBehavior bodyBehavior;

        private vector _movement;
        public virtual vector Movement
        {
            get => _movement;
            protected set
            {
                _movement = value;
                if (bodyBehavior.MaximumSpeed != null)
                {
                    if (_movement.Distance > bodyBehavior.MaximumSpeed) _movement = 
                            new vector(_movement.Direction, bodyBehavior.MaximumSpeed);
                }
            }
        }

        public virtual ILocation Location { get; protected set; }

        public virtual Direction Heading { get; protected set; }

        public virtual Direction Bearing => Movement.Direction;

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

        public virtual void Accelerate(vector accelerationVector)
        {
            this.Movement = this.Movement + accelerationVector;
        }
        public virtual void Accelerate(Direction direction, Distance amount) => Accelerate(new vector(direction, amount));
        public virtual void AccelerateAlongBearing(Distance amount) => Accelerate(Bearing, amount);
        public virtual void AccelerateAlongHeading(Distance amount) => Accelerate(Heading, amount);

        public virtual void DecelerateAlongBearing(Distance amount) => Accelerate(Bearing, -amount);
        public virtual void DecelerateAlongHeading(Distance amount) => Accelerate(Heading, -amount);

        public virtual void TurnLeft(Direction amount) => this.Heading -= amount;
        public virtual void TurnLeft(radian amount) => TurnLeft(Direction.FromRadian(amount));

        public virtual void TurnRight(Direction amount) => this.Heading += amount;
        public virtual void TurnRight(radian amount) => TurnRight(Direction.FromRadian(amount));
    }
}