using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Geerten.Movement.Geometry
{
    public class VectorTests
    {
        [Fact]
        public void ZeroVectorHasZeroOffsets()
        {
            Assert.Equal(0, vector.Zero.XOffset);
            Assert.Equal(0, vector.Zero.YOffset);
        }

        [Fact]
        public void ZeroVectorDoesNotCrashOnDirectionQuery()
        {
            var direction = vector.Zero.Direction;

            Assert.NotNull(direction);
        }

        [Fact]
        public void ZeroVectorHasZeroDistance()
        {
            Assert.Equal(Distance.Zero, vector.Zero.Distance);
        }

        [Fact]
        public void VectorConstructedFromOffsetsIsFullyInitialized()
        {
            var vector = new vector(10, 0);

            var direction = Direction.FromRadian(new radian(0.5d * Math.PI));
            var distance = new Distance(10.0d);

            Assert.Equal(10, vector.XOffset);
            Assert.Equal(0, vector.YOffset);
            Assert.Equal(direction, vector.Direction);
            Assert.Equal(distance, vector.Distance);
        }

        [Fact]
        public void VectorConstructedFromDirectionAndDistanceHasOffsets()
        {
            var direction = Direction.FromRadian(new radian(0.5d * Math.PI));
            var distance = new Distance(10.0d);

            var vector = new vector(direction, distance);

            Assert.Equal(10, vector.XOffset);
            Assert.Equal(0, vector.YOffset);
            Assert.Equal(direction, vector.Direction);
            Assert.Equal(distance, vector.Distance);
        }

        [Fact]
        public void NegativeVectorIsVectorInOppositeDirectionWithSameDistance()
        {
            var vector = new vector(10, 0);

            var expected = new vector(-10, 0);

            Assert.Equal(expected, -vector);
        }

        [Fact]
        public void SumVectorSumsXandYValues()
        {
            var vector1 = new vector(10, 0);
            var vector2 = new vector(0, 10);

            var expected = new vector(10, 10);

            Assert.Equal(expected, vector1 + vector2);
        }

        [Fact]
        public void VectorDifferenceSubtractsXandY()
        {
            var vector1 = new vector(10, 10);
            var vector2 = new vector(3, 7);

            var expected = new vector(7, 3);

            Assert.Equal(expected, vector1 - vector2);
        }

        [Fact]
        public void VectorDivisionDividesBothXandY()
        {
            var vector = new vector(100, 50);

            var expected = new vector(10, 5);

            Assert.Equal(expected, vector / 10.0d);
        }

        [Fact]
        public void VectorMultiplicationMultipliesBothXAndY()
        {
            var vector = new vector(10, 5);

            var expected = new vector(100, 50);

            Assert.Equal(expected, 10 * vector);
            Assert.Equal(expected, vector * 10);
            Assert.Equal(expected, 10.0d * vector);
            Assert.Equal(expected, vector * 10.0d);
        }

        [Fact]
        public void VectorIsEqualToItself()
        {
            var vector = new vector(10, 10);

#pragma warning disable CS1718
            Assert.True(vector == vector);
            Assert.True(vector.Equals(vector));
            Assert.False(vector != vector);
#pragma warning restore CS1718
        }

        [Fact]
        public void DifferentVectorsWithSameValuesAreEqual()
        {
            var vector = new vector(10, 5);
            var vector2 = new vector(10, 5);

            Assert.True(vector == vector2);
            Assert.True(vector.Equals(vector2));
            Assert.False(vector != vector2);
        }

        [Fact]
        public void DifferentVectorsWithDifferentValuesAreNotEqual()
        {
            var vector = new vector(10, 5);
            var vector2 = new vector(5, 10);

            Assert.False(vector == vector2);
            Assert.False(vector.Equals(vector2));
            Assert.True(vector != vector2);
        }
    }
}
