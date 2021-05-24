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
        public Distance? MaximumSpeed;

        /// <summary>
        /// A bodybasebehavior with null as the value of all parameters
        /// </summary>
        public static BodyBaseBehavior Default
        {
            get
            {
                return new BodyBaseBehavior(null);
            }
        }

        /// <summary>
        /// Some parameters for the behavior of the body base
        /// </summary>
        /// <param name="maximumSpeed">Maximum speed, set to null to leave unlimited</param>
        public BodyBaseBehavior(Distance? maximumSpeed)
        {
            this.MaximumSpeed = maximumSpeed;
        }
    }
}
