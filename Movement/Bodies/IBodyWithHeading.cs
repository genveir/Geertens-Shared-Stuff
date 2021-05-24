using Geerten.Movement.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geerten.Movement.Bodies
{
    public interface IBodyWithHeading : IBody
    {
        /// <summary>
        /// The direction in which the nose of the body is pointed
        /// </summary>
        Direction Heading { get; }
    }
}
