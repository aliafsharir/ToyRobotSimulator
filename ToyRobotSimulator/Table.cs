using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotSimulator
{
    public class Table
    {
        public int Height { get; }
        public int Width { get; }

        public Table(int height, int width)
        {
            Height = height;
            Width = width;
        }
    }
}
