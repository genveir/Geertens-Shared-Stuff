using Geerten.MovementLib.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geerten.MovementLib.Movement
{
    public struct BodyBaseBehavior
    {
        /// <summary>
        /// Movement vector distance will be limited to this speed
        /// </summary>
        public Distance MaximumSpeed;

        public static BodyBaseBehavior Default
        {
            get
            {
                return new BodyBaseBehavior(null);
            }
        }

        public BodyBaseBehavior(Distance maximumSpeed)
        {
            this.MaximumSpeed = maximumSpeed;
        }
    }
}
