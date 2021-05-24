using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Geerten.MovementLib.Geometry
{
    public class DistanceTests
    {
        [Fact]
        public void ZeroDistanceHasValueZero()
        {
            Assert.Equal(0.0d, Distance.Zero.Value);
        }

        [Fact]
        public void NonZeroDistanceHasCorrectValue()
        {
            Assert.Equal(500.0d, new Distance(500).Value);
        }

        [Theory]
        [InlineData(10, 0)]
        [InlineData(0, 10)]
        [InlineData(-10, 0)]
        [InlineData(0, -10)]
        public void CanCalculateZeroDistance(long xVal, long yVal)
        {
            var loc1 = new FixedLocation(xVal, yVal);
            var loc2 = new FixedLocation(xVal, yVal);

            var result = Distance.Calculate(loc1, loc2);

            Assert.Equal(0.0d, result, 6);
        }

        [Theory]
        [InlineData(10, 0, 10, 238903)]
        [InlineData(23989, 0, 0, 0)]
        [InlineData(-2309, 0, -2309, 10)]
        [InlineData(1298921, -10, -12982, -10)]
        public void CanCalculateDistanceOnLine(long xVal1, long yVal1, long xVal2, long yVal2)
        {
            var loc1 = new FixedLocation(xVal1, yVal1);
            var loc2 = new FixedLocation(xVal2, yVal2);

            var result = Distance.Calculate(loc1, loc2);

            var distance = CalculatePythagoras(xVal1, yVal1, xVal2, yVal2);

            Assert.Equal(distance, result, 6);
        }

        [Theory]
        [InlineData(10, 25, -60, 20)]
        [InlineData(1023, 2387, 3498, -1297)]
        [InlineData(0, 0, -1298, -3489)]
        [InlineData(2093, -2390823, 0, 1)]
        public void CanCalculateTriangleDistance(long xVal1, long yVal1, long xVal2, long yVal2)
        {
            var loc1 = new FixedLocation(xVal1, yVal1);
            var loc2 = new FixedLocation(xVal2, yVal2);

            var result = Distance.Calculate(loc1, loc2);

            var distance = CalculatePythagoras(xVal1, yVal1, xVal2, yVal2);

            Assert.Equal(distance, result, 6);
        }

        [Fact]
        public void CanCalculateHugeDistancesWithoutOverflow()
        {
            var loc1 = new FixedLocation(long.MaxValue / 2, 0);
            var loc2 = new FixedLocation(0, long.MaxValue / 2);

            var result = Distance.Calculate(loc1, loc2);

            var correctAnswer = Math.Sqrt(2) * long.MaxValue / 2;

            Assert.Equal(correctAnswer, result, 6);
        }

        [Fact]
        public void NegativeDistanceIsDistanceWithNegativeValue()
        {
            var distance = new Distance(500);

            var negated = -distance;

            Assert.Equal(-500.0d, negated.Value);
        }

        [Fact]
        public void SummedDistanceHasCorrectValue()
        {
            var sum = new Distance(100) + new Distance(200);

            Assert.Equal(300.0d, sum.Value);
        }

        [Fact]
        public void SubtractedDistanceHasCorrectValue()
        {
            var difference = new Distance(1000.0d) - new Distance(333.3d);

            Assert.Equal(666.7d, difference.Value);
        }

        [Fact]
        public void MultipliedDistanceHasCorrectValue()
        {
            var product = new Distance(1000.0d) * new Distance(123.456d);

            Assert.Equal(123456.0d, product);
        }

        [Fact]
        public void DividedDistanceHasCorrectValue()
        {
            var quotient = new Distance(654321.0d) / new Distance(1000.0d);

            Assert.Equal(654.321d, quotient);
        }

        [Fact]
        public void DistanceIsEqualToDifferentDistanceWithSameValue()
        {
            var d1 = new Distance(1000.0d);
            var d2 = new Distance(1000.0d);

            Assert.True(d1 == d2);
            Assert.True(d1.Equals(d2));
            Assert.False(d1 != d2);
        }

        [Fact]
        public void DistanceIsNotEqualToDifferentDistanceWithDifferentValue()
        {
            var d1 = new Distance(1000.0d);
            var d2 = new Distance(-1000.0d);

            Assert.False(d1 == d2);
            Assert.False(d1.Equals(d2));
            Assert.True(d1 != d2);
        }

        [Fact]
        public void EqualsWithPrecisionIsEqualForSameValue()
        {
            var d1 = new Distance(1000.0d);
            var d2 = new Distance(1000.0d);

            Assert.True(d1.Equals(d2, 15));
        }

        [Fact]
        public void EqualsWithPrecisionIsNotEqualForDifferentValue()
        {
            var d1 = new Distance(1000.0d);
            var d2 = new Distance(-1000.0d);

            Assert.False(d1.Equals(d2, 15));
        }

        [Fact]
        public void EqualsWithPrecisionDoesNotIgnoreSignificantDigits()
        {
            var d1 = new Distance(1000.0001d);
            var d2 = new Distance(1000.002d);

            Assert.False(d1.Equals(d2, 3));
        }

        [Fact]
        public void EqualseWithPrecisionIgnoresInsignificantDigits()
        {
            var d1 = new Distance(1000.0001d);
            var d2 = new Distance(1000.002d);

            Assert.True(d1.Equals(d2, 2));
        }

        [Fact]
        public void CanCompareDistances()
        {
            var d1 = new Distance(10.0d);
            var d2 = new Distance(11.0d);

            Assert.True(d1 < d2);
            Assert.True(d2 > d1);

            var distances = new List<Distance> { d2, d1 };
            distances.Sort();

            Assert.Equal(d1, distances[0]);
            Assert.Equal(d2, distances[1]);
        }

        private double CalculatePythagoras(long xVal1, long yVal1, long xVal2, long yVal2)
        {
            var xDist = Math.Abs(xVal1 - xVal2);
            var yDist = Math.Abs(yVal1 - yVal2);

            var squareSum = xDist * xDist + yDist * yDist;

            return Math.Sqrt(squareSum);
        }
    }
}
