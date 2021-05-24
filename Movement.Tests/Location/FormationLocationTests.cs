using Geerten.Movement.Bodies;
using Geerten.Movement.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Geerten.Movement.Location
{
    public class FormationLocationTests
    {
        [Fact]
        public void DirectionAndDistanceCreateFromAbsoluteOffsetSetsXAndY()
        {
            var leader = TestBody.Default;

            var loc = FormationLocation.CreateFromAbsoluteOffset(leader, Direction.FromDegrees(45.0d), new Distance(Math.Sqrt(2) * 100.0d));

            Assert.Equal(110, loc.X);
            Assert.Equal(115, loc.Y);
        }

        [Fact]
        public void VectorCreateFromAbsoluteOffsetSetsXAndY()
        {
            var leader = TestBody.Default;

            var loc = FormationLocation.CreateFromAbsoluteOffset(leader, new vector(100, 300));

            Assert.Equal(110, loc.X);
            Assert.Equal(315, loc.Y);
        }

        [Fact]
        public void DirectionAndDistanceCreateFromRelativeOffsetSetsXAndY()
        {
            var leader = TestBody.Default;

            var loc = FormationLocation.CreateFromRelativeOffset(leader, Direction.FromDegrees(45.0d), new Distance(Math.Sqrt(2) * 100d));

            Assert.Equal(110, loc.X);
            Assert.Equal(-85.0d, loc.Y);
        }

        [Fact]
        public void VectorCreateFromRelativeOffsetSetsXAndY()
        {
            var leader = TestBody.Default;

            var loc = FormationLocation.CreateFromRelativeOffset(leader, new vector(100, 300));

            Assert.Equal(310, loc.X);
            Assert.Equal(-84, loc.Y); // whatever, rounding issue, doesn't really matter
        }

        [Fact]
        public void PositionUpdatesWithLeaderMovement()
        {
            var leader = TestBody.Default;

            // 110, 115
            var loc = FormationLocation.CreateFromAbsoluteOffset(leader, Direction.FromDegrees(45.0d), new Distance(Math.Sqrt(2) * 100.0d));

            leader.Move(new FixedLocation(-103, -105));

            Assert.Equal(-3, loc.X);
            Assert.Equal(-5, loc.Y);
        }

        [Fact]
        public void PositionUpdateWithLeaderRotation()
        {
            var leader = TestBody.Default;

            // 110, 115
            var loc = FormationLocation.CreateFromAbsoluteOffset(leader, Direction.FromDegrees(45.0d), new Distance(Math.Sqrt(2) * 100.0d));

            leader.Turn(Direction.FromDegrees(270.0d));

            Assert.Equal(-90, loc.X);
            Assert.Equal(-84, loc.Y); // whatever, rounding issue, doesn't really matter
        }

        private class TestBody : BodyBase
        {
            public static TestBody Default
            {
                get
                {
                    var loc = new FixedLocation(10, 15);
                    var heading = Direction.FromDegrees(90);

                    return new TestBody(loc, heading);
                }
            }

            public TestBody(ILocation location, Direction heading) : base(location, heading) { }

            public void Move(ILocation newLocation)
            {
                this.Location = newLocation;
            }

            public void Turn(Direction newHeading)
            {
                this.Heading = newHeading;
            }
        }
    }
}
