using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnToAngle
{
    class MotorSimulation
    {
        public static double ReferenceAngle = 100.3;
        public static void Move(double delta)
        {
            ReferenceAngle += delta;
        }
    }
}
