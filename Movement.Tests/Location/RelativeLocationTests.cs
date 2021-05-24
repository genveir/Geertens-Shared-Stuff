using Geerten.Movement.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Geerten.Movement.Location
{
    public class RelativeLocationTests
    {
        [Fact]
        public void VectorConstructorIsCorrectInitially()
        {
            var otherLoc = new MovableLocation(10, 5);

            var loc = new RelativeLocation(otherLoc, new vector(10, 20));

            Assert.Equal(20, loc.X);
            Assert.Equal(25, loc.Y);
        }

        [Fact]
        public void OffsetConstructorIsCorrectInitially()
        {
            var otherLoc = new MovableLocation(10, 5);

            var loc = new RelativeLocation(otherLoc, 10, 20);

            Assert.Equal(20, loc.X);
            Assert.Equal(25, loc.Y);
        }

        [Fact]
        public void DirectionAndDistanceConstructorIsCorrectInitially()
        {
            var otherLoc = new MovableLocation(10, 5);

            var loc = new RelativeLocation(otherLoc, Direction.FromDegrees(45), new Distance(Math.Sqrt(2) * 10));

            Assert.Equal(20, loc.X);
            Assert.Equal(15, loc.Y);
        }

        [Fact]
        public void RelativeLocationFollowsOrigin()
        {
            var otherLoc = new MovableLocation(10, 5);

            var loc = new RelativeLocation(otherLoc, 10, 20);

            otherLoc.X = 20;
            otherLoc.Y = 15;

            Assert.Equal(30, loc.X);
            Assert.Equal(35, loc.Y);
        }
    }
}
