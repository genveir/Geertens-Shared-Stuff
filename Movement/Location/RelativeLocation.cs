using Geerten.Movement.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geerten.Movement.Location
{
    public class RelativeLocation : ILocation
    {
        ILocation _originLocation;
        vector _offset;

        public RelativeLocation(ILocation originLocation, vector offset)
        {
            _originLocation = originLocation;
            _offset = offset;
        }
        public RelativeLocation(ILocation originLocation, long XOffset, long YOffset) 
            : this(originLocation, new vector(XOffset, YOffset)) { }
        public RelativeLocation(ILocation originLocation, Direction direction, Distance distance) 
            : this(originLocation, new vector(direction, distance)) { }

        public long X
        {
            get { return _originLocation.X + _offset.XOffset; }
        }

        public long Y
        {
            get { return _originLocation.Y + _offset.YOffset; }
        }
    }
}
