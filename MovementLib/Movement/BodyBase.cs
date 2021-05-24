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
        /// <summary>
        /// A struct with some configuration for behavior
        /// </summary>
        protected BodyBaseBehavior bodyBehavior;

        private vector _movement;
        /// <summary>
        /// A vector that signifies the shift of the body's location on an update.
        /// </summary>
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

        /// <summary>
        /// The location of the body
        /// </summary>
        public virtual ILocation Location { get; protected set; }
        
        /// <summary>
        /// The direction the body is facing
        /// </summary>
        public virtual Direction Heading { get; protected set; }

        /// <summary>
        /// The direction the body is moving
        /// </summary>
        public virtual Direction Bearing => Movement.Direction;

        /// <summary>
        /// Creates a new body-base
        /// </summary>
        /// <param name="location">The initial location of the body</param>
        /// <param name="behavior">Parameters for the body's behavior</param>
        /// <param name="heading">The direction the body is facing</param>
        /// <param name="movement">The direction and velocity in which the body is moving</param>
        public BodyBase(ILocation location, BodyBaseBehavior behavior, Direction heading, vector movement)
        {
            this.Location = location;
            this.bodyBehavior = behavior;
            this.Heading = heading;
            this.Movement = movement;
        }

        /// <summary>
        /// Creates a new body-base
        /// </summary>
        /// <param name="location">The initial location of the body</param>
        public BodyBase(ILocation location) : this(location, BodyBaseBehavior.Default, Direction.Zero, vector.Zero) { }
        /// <summary>
        /// Creates a new body-base
        /// </summary>
        /// <param name="location">The initial location of the body</param>
        /// <param name="behavior">Parameters for the body's behavior</param>
        public BodyBase(ILocation location, BodyBaseBehavior behavior) : this(location, behavior, Direction.Zero, vector.Zero) { }
        /// <summary>
        /// Creates a new body-base
        /// </summary>
        /// <param name="location">The initial location of the body</param>
        /// <param name="behavior">Parameters for the body's behavior</param>
        /// <param name="heading">The direction the body is facing</param>
        public BodyBase(ILocation location, BodyBaseBehavior behavior, Direction heading) : this(location, behavior, heading, vector.Zero) { }
        /// <summary>
        /// Creates a new body-base
        /// </summary>
        /// <param name="location">The initial location of the body</param>
        /// <param name="behavior">Parameters for the body's behavior</param>
        /// <param name="movement">The direction and velocity in which the body is moving</param>
        public BodyBase(ILocation location, BodyBaseBehavior behavior, vector movement) : this(location, behavior, Direction.Zero, movement) { }
        /// <summary>
        /// Creates a new body-base
        /// </summary>
        /// <param name="location">The initial location of the body</param>
        /// <param name="behavior">Parameters for the body's behavior</param>
        /// <param name="bearing">The direction in which the body is moving</param>
        /// <param name="speed">The speed the body is moving at</param>
        public BodyBase(ILocation location, BodyBaseBehavior behavior, Direction bearing, Distance speed) : this(location, behavior, bearing, new vector(bearing, speed)) { }
        /// <summary>
        /// Creates a new body-base
        /// </summary>
        /// <param name="location">The initial location of the body</param>
        /// <param name="behavior">Parameters for the body's behavior</param>
        /// <param name="heading">The direction the body is facing</param>
        /// <param name="bearing">The direction in which the body is moving</param>
        /// <param name="speed">The speed the body is moving at</param>
        public BodyBase(ILocation location, BodyBaseBehavior behavior, Direction heading, Direction bearing, Distance speed) : this(location, behavior, heading, new vector(bearing, speed)) { }
        /// <summary>
        /// Creates a new body-base
        /// </summary>
        /// <param name="location">The initial location of the body</param>
        /// <param name="heading">The direction the body is facing</param>
        public BodyBase(ILocation location, Direction heading) : this(location, BodyBaseBehavior.Default, heading, vector.Zero) { }
        /// <summary>
        /// Creates a new body-base
        /// </summary>
        /// <param name="location">The initial location of the body</param>
        /// <param name="movement">The direction and velocity in which the body is moving</param>
        public BodyBase(ILocation location, vector movement) : this(location, BodyBaseBehavior.Default, Direction.Zero, movement) { }
        /// <summary>
        /// Creates a new body-base
        /// </summary>
        /// <param name="location">The initial location of the body</param>
        /// <param name="heading">The direction the body is facing</param>
        /// <param name="movement">The direction and velocity in which the body is moving</param>
        public BodyBase(ILocation location, Direction heading, vector movement) : this(location, BodyBaseBehavior.Default, heading, movement) { }
        /// <summary>
        /// Creates a new body-base
        /// </summary>
        /// <param name="location">The initial location of the body</param>
        /// <param name="bearing">The direction in which the body is moving</param>
        /// <param name="speed">The speed the body is moving at</param>
        public BodyBase(ILocation location, Direction bearing, Distance speed) : this(location, BodyBaseBehavior.Default, bearing, new vector(bearing, speed)) { }
        /// <summary>
        /// Creates a new body-base
        /// </summary>
        /// <param name="location">The initial location of the body</param>
        /// <param name="heading">The direction the body is facing</param>
        /// <param name="bearing">The direction in which the body is moving</param>
        /// <param name="speed">The speed the body is moving at</param>
        public BodyBase(ILocation location, Direction heading, Direction bearing, Distance speed) : this(location, BodyBaseBehavior.Default, heading, new vector(bearing, speed)) { }

        /// <summary>
        /// Accelerate the body
        /// </summary>
        /// <param name="accelerationVector">The direction and distance with which movement will be updated</param>
        public virtual void Accelerate(vector accelerationVector)
        {
            this.Movement = this.Movement + accelerationVector;
        }
        /// <summary>
        /// Accelerate the body
        /// </summary>
        /// <param name="direction">The direction in which the acceleration takes place</param>
        /// <param name="amount">The amount of acceleration</param>
        public virtual void Accelerate(Direction direction, Distance amount) => Accelerate(new vector(direction, amount));
        /// <summary>
        /// Accelerate the body in the direction it is moving
        /// </summary>
        /// <param name="amount">The amount of acceleration</param>
        public virtual void AccelerateAlongBearing(Distance amount) => Accelerate(Bearing, amount);
        /// <summary>
        /// Accelerate the body in the direction it is facing
        /// </summary>
        /// <param name="amount">The amount of acceleration</param>
        public virtual void AccelerateAlongHeading(Distance amount) => Accelerate(Heading, amount);

        /// <summary>
        /// Decelerate the body in the direction it is moving
        /// </summary>
        /// <param name="amount">The amount of deceleration</param>
        public virtual void DecelerateAlongBearing(Distance amount) => Accelerate(Bearing, -amount);
        /// <summary>
        /// Decelerate the body in the direction it is facing
        /// </summary>
        /// <param name="amount">The amount of deceleration</param>
        public virtual void DecelerateAlongHeading(Distance amount) => Accelerate(Heading, -amount);

        /// <summary>
        /// Turn the body to the left
        /// </summary>
        /// <param name="amount">How much the body should be turned</param>
        public virtual void TurnLeft(Direction amount) => this.Heading -= amount;
        /// <summary>
        /// Turn the body to the left
        /// </summary>
        /// <param name="amount">How much the body should be turned</param>
        public virtual void TurnLeft(radian amount) => TurnLeft(Direction.FromRadian(amount));

        /// <summary>
        /// Turn the body to the right
        /// </summary>
        /// <param name="amount">How much the body should be turned</param>
        public virtual void TurnRight(Direction amount) => this.Heading += amount;
        /// <summary>
        /// Turn the body to the right
        /// </summary>
        /// <param name="amount">How much the body should be turned</param>
        public virtual void TurnRight(radian amount) => TurnRight(Direction.FromRadian(amount));

        /// <summary>
        /// Sets a new Location based on an update function.
        /// </summary>
        /// <param name="updateFunction">Location will be set to the result of this function with Location and Movement as parameters</param>
        public void UpdateLocation(Func<ILocation, vector, ILocation> updateFunction)
        {
            this.Location = updateFunction(this.Location, this.Movement);
        }
        /// <summary>
        /// Sets a new FixedLocation based on current Location and Movement
        /// </summary>
        public void UpdateLocation() => UpdateLocation((loc, mov) => new FixedLocation(loc, mov));
    }
}
