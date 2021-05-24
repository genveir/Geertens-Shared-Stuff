using Geerten.Movement.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geerten.Movement.Bodies
{
    public interface IBody
    {
        /// <summary>
        /// The location of a body
        /// </summary>
        ILocation Location { get; }
    }
}
