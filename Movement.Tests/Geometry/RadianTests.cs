using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Geerten.Movement.Geometry
{
    public class RadianTests
    {
        private const double TAU = 2 * Math.PI;

        [Fact]
        public void RadianCreatedWithZeroIsZero()
        {
            var radian = new radian(0.0d);

            Assert.Equal(0.0d, radian.toDouble());
        }

        [Fact]
        public void RadianValueIsModulatedByTwoPi()
        {
            var radian = new radian(10001.23d);

            Assert.Equal(10001.23d % TAU, radian.toDouble());
        }

        [Fact]
        public void NegativeRadianIsModulatedToPositiveValue()
        {
            var radian = new radian(-1.0d);

            Assert.Equal(TAU - 1.0d, radian.toDouble());
        }

        [Theory]
        [InlineData(0.5d, 0.5d)]
        [InlineData(-0.5d, 0.5d)]
        [InlineData(Math.PI, Math.PI)]
        [InlineData(0.0d, 0.0d)]
        [InlineData(TAU - 0.5d, 0.5d)]
        public void AbsoluteGivesDistanceToZeroAsPositiveRadian(double value, double expected)
        {
            var radian = new radian(value);
            var expectedRadian = new radian(expected);

            Assert.Equal(expectedRadian, radian.Absolute());
        }

        [Fact]
        public void NegativeRadianGivesAngleComplement()
        {
            var radian = new radian(0.5d);
            var expected = new radian(Math.PI + 0.5d);

            Assert.Equal(expected, -radian);
        }

        [Fact]
        public void SummedRadianGivesRadianWithSumOfValues()
        {
            var radian = new radian(2.0d);
            var radian2 = new radian(2.0d);

            var expected = new radian(4.0d);

            Assert.Equal(expected, radian + radian2);
        }

        [Fact]
        public void RadianDifferenceGivesRadianWithDifferenceOfValues()
        {
            var radian = new radian(2.0d);
            var radian2 = new radian(4.0d);

            var expected = new radian(-2.0d);

            Assert.Equal(expected, radian - radian2);
        }

        [Fact]
        public void RadianMultiplicationGivesRadianWithMultipliedValue()
        {
            var radian = new radian(2.0d);

            var expected = new radian(20.0d);

            Assert.Equal(expected, 10.0d * radian);
            Assert.Equal(expected, radian * 10.0d);
        }

        [Fact]
        public void RadianDivisionGivesRadianWithDividedValue()
        {
            var radian = new radian(2.0d); // this test will not work with value >= TAU

            var expected = new radian(0.2d);

            Assert.Equal(expected, radian / 10.0d);
        }

        [Fact]
        public void RadianIsGreaterWhenItsValueIsCloserToTau()
        {
            var radian = new radian(Math.PI);
            var radian2 = new radian(TAU - 0.1d);

            Assert.True(radian < radian2);
            Assert.True(radian2 > radian);
        }

        [Fact]
        public void RadianIsEqualToItself()
        {
            var radian = new radian(2.0d);

#pragma warning disable CS1718
            Assert.True(radian == radian);
            Assert.True(radian.Equals(radian));
            Assert.False(radian != radian);
#pragma warning restore CS1718
        }

        [Fact]
        public void DifferentRadiansWithSameValueAreEqual()
        {
            var radian = new radian(0.2d);
            var radian2 = new radian(0.2d);

            Assert.True(radian == radian2);
            Assert.True(radian.Equals(radian2));
            Assert.False(radian != radian2);
        }

        [Fact]
        public void DifferentRadiansWithDifferentValuesAreNotEqual()
        {
            var radian = new radian(0.2d);
            var radian2 = new radian(0.4d);

            Assert.False(radian == radian2);
            Assert.False(radian.Equals(radian2));
            Assert.True(radian != radian2);
        }

    }
}
