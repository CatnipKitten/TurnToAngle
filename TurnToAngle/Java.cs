using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace TurnToAngle
{
    class Java
    {
        //List that holds all angles.
        static List<double> angles = new List<double>();

        /// <summary>
        /// Determines whether or not the angles have been calculated.
        /// </summary>
        static bool init = false;
        /// <summary>
        /// Number of segments for the circle. Supports any number.
        /// </summary>
        static double segmentCount = 4;
        /// <summary>
        /// The range between each number.
        /// </summary>
        static double divisorAmount;

        /// <summary>
        /// Initializes the program by adding the number of 
        /// </summary>
        /// <param name="segments">Number of times to segment the circle.</param>
        public static void Initialize(double segments)
        {
            Console.WriteLine("Initializing.\nSegments: {0}", segments);
            //Number of degrees in a circle. Straight forward. If you want, you can change how many degrees there are in a circle.
            int deg = 360;
            double currentDeg = 0;
            angles.Add(currentDeg);
            while (currentDeg < deg)
            {
                currentDeg += (deg / segments);
                angles.Add(currentDeg);
            }
            divisorAmount = deg / segments;
            foreach (double x in angles)
                Console.Write(x + " ");
            Console.Write("\n");
        }

        /// <summary>
        /// Provides whether to move left or right. Used for determining floor or ceiling rounding.
        /// </summary>
        public enum Direction { Left, Right }
        /// <summary>
        /// Moves to a specified angle.
        /// </summary>
        /// <param name="direction">Direction to move according to floor or ceiling.</param>
        public static void MoveToAngle(Direction direction)
        {
            //Check whether or not we have initialized the segments of the circle. If not, we NEED to! >:(
            if (!init)
                Initialize(segmentCount);

            //Desired angle to turn to.
            double desiredAngle;
            //Finds the desired angle by rounding down.
            if (direction == Direction.Left)
                desiredAngle = angles.ElementAt((int)Math.Floor(MotorSimulation.ReferenceAngle / divisorAmount));
            //Finds the desired angle by rounding up.
            else
                desiredAngle = angles.ElementAt((int)Math.Ceiling(MotorSimulation.ReferenceAngle / divisorAmount));

            //Here's where we start moving motors.
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //Move while we're not the desired angle.
            while((MotorSimulation.ReferenceAngle != desiredAngle) && (stopwatch.ElapsedMilliseconds <= 1000))
            {
                Console.WriteLine("Reference Angle: {0}", MotorSimulation.ReferenceAngle);
                if (MotorSimulation.ReferenceAngle > desiredAngle)
                {
                    MotorSimulation.Move(-0.25);
                    Console.WriteLine("Less!");
                }
                else
                {
                    MotorSimulation.Move(0.25);
                    Console.WriteLine("Greater!");
                }
            }
            Console.WriteLine("Met the desired angle of {0}, or stopped due to the timer.", desiredAngle);
        }
    }
}
