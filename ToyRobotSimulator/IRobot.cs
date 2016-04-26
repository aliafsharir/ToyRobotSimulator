using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotSimulator
{
    public interface IRobot
    {
        void Place(int x, int y, string strDirection);
        void Move();
        void Left();
        void Right();
        string Report();
    }
}
