using Geerten.Movement.Bodies;
using Geerten.Movement.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geerten.Movement.Location
{
    public class FormationLocation : ILocation
    {
        private Direction lastHeading = Direction.Zero;
        private vector? lastAbsoluteVector = null;

        private vector CalculateAbsoluteVector()
        {
            var leaderHeading = _formationLeader.Heading;
            if (lastHeading != leaderHeading || lastAbsoluteVector == null)
            {
                var absoluteDirection = leaderHeading + _relativeDirection;

                lastAbsoluteVector = new vector(absoluteDirection, _distance);
            }

            return lastAbsoluteVector.Value;
        }

        public long X 
        { 
            get
            {
                var vector = CalculateAbsoluteVector();

                return _formationLeader.Location.X + vector.XOffset;
            }
        }

        public long Y
        {
            get
            {
                var vector = CalculateAbsoluteVector();

                return _formationLeader.Location.Y + vector.YOffset;
            }
        }

        private IBodyWithHeading _formationLeader;
        private Direction _relativeDirection;
        private Distance _distance;

        private FormationLocation(IBodyWithHeading formationLeader, Direction relativeDirection, Distance distance)
        {
            _formationLeader = formationLeader;
            _relativeDirection = relativeDirection;
            _distance = distance;
        }

        public static FormationLocation CreateFromAbsoluteOffset(IBodyWithHeading formationLeader, Direction absoluteDirection, Distance distance)
        {
            var relativeDirection = absoluteDirection - formationLeader.Heading;

            return new FormationLocation(formationLeader, relativeDirection, distance);
        }
        public static FormationLocation CreateFromAbsoluteOffset(IBodyWithHeading formationLoader, vector offset) =>
            CreateFromAbsoluteOffset(formationLoader, offset.Direction, offset.Distance);

        public static FormationLocation CreateFromRelativeOffset(IBodyWithHeading formationLeader, Direction relativeDirection, Distance distance)
        {
            return new FormationLocation(formationLeader, relativeDirection, distance);
        }
        public static FormationLocation CreateFromRelativeOffset(IBodyWithHeading formationLeader, vector offset) =>
            CreateFromRelativeOffset(formationLeader, offset.Direction, offset.Distance);
    }
}
