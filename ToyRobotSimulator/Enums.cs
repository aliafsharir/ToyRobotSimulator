using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotSimulator
{
    public enum Command
    {
        PLACE,
        MOVE, 
        LEFT,
        RIGHT,
        REPORT
    }

    public enum RotateTo
    {
        RIGHT,
        LEFT
    }

    public enum Direction
    {
        NORTH = 0,
        EAST = 90,
        SOUTH = 180,
        WEST = 270
    }
}
