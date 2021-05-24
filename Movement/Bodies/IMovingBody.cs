using Geerten.Movement.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geerten.Movement.Bodies
{
    public interface IMovingBody : IBody
    {
        vector Movement { get; }
    }
}
