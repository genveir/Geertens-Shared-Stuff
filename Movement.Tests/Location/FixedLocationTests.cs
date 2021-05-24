using Geerten.Movement.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Geerten.Movement.Location
{
    public class FixedLocationTests
    {
        [Fact]
        public void ZeroConstructorSetsZeroXAndY()
        {
            var loc = FixedLocation.Zero;

            Assert.Equal(0, loc.X);
            Assert.Equal(0, loc.Y);
        }

        [Fact]
        public void XYConstructorSetsXAndY()
        {
            var loc = new FixedLocation(5, 7);

            Assert.Equal(5, loc.X);
            Assert.Equal(7, loc.Y);
        }

        [Fact]
        public void LocationConstructorSetsXAndY()
        {
            var otherLoc = new FixedLocation(5, 7);
            var loc = new FixedLocation(otherLoc);

            Assert.Equal(5, loc.X);
            Assert.Equal(7, loc.Y);
        }

        [Fact]
        public void VectorConstructorOffsetsOffLocation()
        {
            var otherLoc = new FixedLocation(5, 7);
            var vector = new vector(10, 20);

            var loc = new FixedLocation(otherLoc, vector);

            Assert.Equal(15, loc.X);
            Assert.Equal(27, loc.Y);
        }

        [Fact]
        public void DirectionAndDistanceConstructorOffsetsOffLocation()
        {
            var otherLoc = new FixedLocation(5, 7);

            var loc = new FixedLocation(otherLoc, Direction.FromDegrees(45), new Distance(Math.Sqrt(2) * 10.0d));

            Assert.Equal(15, loc.X);
            Assert.Equal(17, loc.Y);
        }
    }
}
