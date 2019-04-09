using System;
using System.Collections.Generic;
using System.Text;
using Turtle.Domain.Models.Enums;

namespace Turtle.Domain.Models.Entities
{
    /// <summary>
    /// Turtle Pointer class which will provide the simulation of a Turtle where it is and in which direction it is pointing
    /// </summary>
    public class TurtlePointer
    {
        public Directions Direction { get; set; }
        public int XVal { get; set; }
        public int YVal { get; set; }

        public TurtlePointer(Directions direction, int xVal, int yVal)
        {
            Direction = direction;
            XVal = xVal;
            YVal = yVal;
        }
    }
}
