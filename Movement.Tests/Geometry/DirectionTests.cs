using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Geerten.Movement.Geometry
{
    public class DirectionTests
    {
        [Fact]
        public void ZeroDirectionIsZeroRadians()
        {
            Assert.Equal(new radian(0.0d), Direction.Zero.InRadians);
        }

        [Fact]
        public void ZeroDirectionIsZeroDegrees()
        {
            Assert.Equal(0.0d, Direction.Zero.InDegrees);
        }

        [Theory]
        [InlineData(0.1d)]
        [InlineData(0.5d * Math.PI)]
        [InlineData(10.0d)]
        public void RadianInputAndOutputMatch(double input)
        {
            var radian = new radian(input);

            var direction = Direction.FromRadian(radian);

            Assert.Equal(radian, direction.InRadians);
        }

        [Theory]
        [InlineData(Math.PI, 180.0d)]
        [InlineData((1.0d/6.0d) * Math.PI, 30.0d)]
        [InlineData((3.0d/2.0d) * Math.PI, 270.0d)]
        public void RadianInputTranslatesToDegrees(double input, double degrees)
        {
            var radian = new radian(input);

            var direction = Direction.FromRadian(radian);

            Assert.Equal(degrees, direction.InDegrees, 6);
        }

        [Theory]
        [InlineData(100.0d)]
        [InlineData(159.5d)]
        [InlineData(5.0d)]
        public void DegreesInputAndOutputMatch(double input)
        {
            var direction = Direction.FromDegrees(input);

            Assert.Equal(input, direction.InDegrees, 6);
        }

        [Fact]
        public void NegativeDegreeInputWraps()
        {
            var direction = Direction.FromDegrees(-90.0d);

            Assert.Equal(270.0d, direction.InDegrees, 6);
        }

        [Fact]
        public void LargeDegreeInputWraps()
        {
            var direction = Direction.FromDegrees(400.0d);

            Assert.Equal(40.0d, direction.InDegrees, 6);
        }

        [Fact]
        public void CanGetAbsoluteDirection()
        {
            var val = new radian(1.5 * Math.PI);

            var direction = Direction.FromRadian(val);

            Assert.Equal(direction.Absolute().InRadians, val.Absolute());
        }

        [Fact]
        public void ZeroDirectionIsStraightUpAlongTheYAxis()
        {
            var pos1 = new FixedLocation(0, 0);
            var pos2 = new FixedLocation(0, 1);

            var direction = Direction.Calculate(pos1, pos2);

            Assert.Equal(new radian(0.0d), direction.InRadians.Absolute());
        }

        [Fact]
        public void DirectionCircleGoesClockwise()
        {
            var pos1 = new FixedLocation(0, 0);
            var pos2 = new FixedLocation(1, 0);

            var direction = Direction.Calculate(pos1, pos2);

            Assert.Equal(new radian(0.5 * Math.PI), direction.InRadians);
        }

        [Theory]
        [InlineData(1, 1, 0.25 * Math.PI)]
        [InlineData(1, -1, 0.75 * Math.PI)]
        [InlineData(-1, -1, 1.25 * Math.PI)]
        [InlineData(-1, 1, 1.75 * Math.PI)]
        public void CalculationIsCorrectForEachQuadrant(long xVar, long yVar, double expected)
        {
            var pos1 = new FixedLocation(0, 0);
            var pos2 = new FixedLocation(xVar, yVar);

            var direction = Direction.Calculate(pos1, pos2);

            var expectedRadian = new radian(expected);

            Assert.Equal(expectedRadian, direction.InRadians);
        }

        [Theory]
        [InlineData(0.0d, Math.PI)]
        [InlineData(1.543d, Math.PI + 1.543d)]
        public void NegatedDirectionIsTheOppositeDirection(double input, double expected)
        {
            var direction = Direction.FromRadian(new radian(input));

            var inverse = -direction;

            var expectedDirection = Direction.FromRadian(new radian(expected));

            Assert.Equal(expectedDirection, inverse);
        }

        [Fact]
        public void SumOfTwoDirectionsIsTheSumOfTheValues()
        {
            var direction = Direction.FromRadian(new radian(0.4d)) + Direction.FromRadian(new radian(0.6d));

            Assert.Equal(Direction.FromRadian(new radian(1.0d)), direction);
        }

        [Fact]
        public void DifferenceBetweenTwoDirectionsIsTheDifferenceOfTheValues()
        {
            var direction = Direction.FromRadian(new radian(1.0d)) - Direction.FromRadian(new radian(0.4d));

            Assert.Equal(Direction.FromRadian(new radian(0.6d)), direction);
        }

        [Fact]
        public void TwoDifferentDirectionsWithTheSameValueAreEqual()
        {
            var direction = Direction.FromRadian(new radian(1.0d));
            var direction2 = Direction.FromRadian(new radian(1.0d));

            Assert.True(direction == direction2);
            Assert.True(direction.Equals(direction2));
            Assert.False(direction != direction2);
        }

        [Fact]
        public void TwoDifferentDirectionsWithDifferentValuesAreNotEqual()
        {
            var direction = Direction.FromRadian(new radian(1.1d));
            var direction2 = Direction.FromRadian(new radian(1.0d));

            Assert.False(direction == direction2);
            Assert.False(direction.Equals(direction2));
            Assert.True(direction != direction2);
        }

        [Fact]
        public void DifferenceLessThanReturnsTrueIfDifferenceIsSmaller()
        
        {
            var direction = Direction.FromRadian(new radian(1.0d));
            var other = Direction.FromRadian(new radian(1.1d));

            Assert.True(direction.DifferenceLessThan(other, new radian(0.2d)));
        }

        [Fact]
        public void DifferenceLessThanReturnsFalseIfDifferenceIsGreater()
        {
            var direction = Direction.FromRadian(new radian(1.0d));
            var other = Direction.FromRadian(new radian(1.3d));

            Assert.False(direction.DifferenceLessThan(other, new radian(0.2d)));
        }

        [Fact]
        public void DifferenceLessThanWorksAcrossZeroBoundary()
        {
            var direction = Direction.FromRadian(new radian(-0.1d));
            var other = Direction.FromRadian(new radian(0.1d));

            Assert.True(direction.DifferenceLessThan(other, new radian(0.3d)));
        }

        [Fact]
        public void DegreeDifferenceLessThanReturnsTrueIfDifferenceIsSmaller()

        {
            var direction = Direction.FromDegrees(0.0d);
            var other = Direction.FromDegrees(10.0d);

            Assert.True(direction.DifferenceLessThan(other, 15.0d));
        }

        [Fact]
        public void DegreeDifferenceLessThanReturnsFalseIfDifferenceIsGreater()
        {
            var direction = Direction.FromDegrees(0.0d);
            var other = Direction.FromDegrees(30.0d);

            Assert.False(direction.DifferenceLessThan(other, 15.0d));
        }

        [Fact]
        public void DegreeDifferenceLessThanWorksAcrossZeroBoundary()
        {
            var direction = Direction.FromDegrees(330.0d);
            var other = Direction.FromDegrees(30.0d);

            Assert.True(direction.DifferenceLessThan(other, 65.0d));
        }
    }
}
