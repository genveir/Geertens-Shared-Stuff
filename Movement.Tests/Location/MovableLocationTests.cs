using Geerten.Movement.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Geerten.Movement.Location
{
    public class MovableLocationTests
    {
        [Fact]
        public void ZeroConstructorSetsZeroXAndY()
        {
            var loc = MovableLocation.Zero;

            Assert.Equal(0, loc.X);
            Assert.Equal(0, loc.Y);
        }

        [Fact]
        public void XYConstructorSetsXAndY()
        {
            var loc = new MovableLocation(5, 7);

            Assert.Equal(5, loc.X);
            Assert.Equal(7, loc.Y);
        }

        [Fact]
        public void LocationConstructorSetsXAndY()
        {
            var otherLoc = new MovableLocation(5, 7);
            var loc = new MovableLocation(otherLoc);

            Assert.Equal(5, loc.X);
            Assert.Equal(7, loc.Y);
        }

        [Fact]
        public void VectorConstructorOffsetsOffLocation()
        {
            var otherLoc = new MovableLocation(5, 7);
            var vector = new vector(10, 20);

            var loc = new MovableLocation(otherLoc, vector);

            Assert.Equal(15, loc.X);
            Assert.Equal(27, loc.Y);
        }

        [Fact]
        public void DirectionAndDistanceConstructorOffsetsOffLocation()
        {
            var otherLoc = new MovableLocation(5, 7);

            var loc = new MovableLocation(otherLoc, Direction.FromDegrees(45), new Distance(Math.Sqrt(2) * 10.0d));

            Assert.Equal(15, loc.X);
            Assert.Equal(17, loc.Y);
        }

        [Fact]
        public void VectorMoveUpdatesXAndYWithVector()
        {
            var loc = new MovableLocation(5, 7);

            loc.Move(new vector(10, 12));

            Assert.Equal(15, loc.X);
            Assert.Equal(19, loc.Y);
        }

        [Fact]
        public void DirectionAndDistanceMoveUpdatesXAndY()
        {
            var loc = new MovableLocation(5, 7);

            loc.Move(Direction.FromDegrees(45), new Distance(Math.Sqrt(2) * 10));

            Assert.Equal(15, loc.X);
            Assert.Equal(17, loc.Y);
        }
    }
}
