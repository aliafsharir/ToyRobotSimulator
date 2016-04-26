using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToyRobotSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotSimulator.Tests
{
    [TestClass()]
    public class RobotTests
    {
        [TestMethod()]
        public void MoveTest_ShouldNotMoveBeforePlaced()
        {
            var robot = new Robot();
            robot.Move();

            if(robot.Report() != string.Empty)
                Assert.Fail("Robot should not move before its placed on a table.");
        }

        [TestMethod()]
        public void LeftTest_ShouldNotTurnBeforePlaced()
        {
            var robot = new Robot();
            robot.Left();

            if (robot.Report() != string.Empty)
                Assert.Fail("Robot should not turn left before its placed on a table.");
        }

        [TestMethod()]
        public void RightTest_ShouldNotTurnBeforePlaced()
        {
            var robot = new Robot();
            robot.Right();

            if (robot.Report() != string.Empty)
                Assert.Fail("Robot should not turn right before its placed on a table.");
        }

        [TestMethod()]
        public void MoveTest_MovingUp()
        {
            var robot = new Robot();
            robot.Place(0, 0, "NORTH");
            robot.Move();

            if (robot.Report() != "0,1,NORTH")
                Assert.Fail("Robot does not move up.");
        }

        [TestMethod()]
        public void MoveTest_ShouldNotFallFromTable()
        {
            var robot = new Robot();
            robot.Place(0, 0, "NORTH");
            robot.Move();
            robot.Move();
            robot.Move();
            robot.Move();
            robot.Move();
            robot.Move();
            robot.Move();

            if (robot.Report() != "0,4,NORTH")
                Assert.Fail("Robot falls from table.");
        }

        [TestMethod()]
        public void PlaceTest_ShouldPlaceOutsideTable()
        {
            var robot = new Robot();
            robot.Place(6, 5, "NORTH");         

            if (robot.Report() != string.Empty)
                Assert.Fail("Robot cannot be placed outside the table.");
        }

        [TestMethod()]
        public void RightTest_TurningRight()
        {
            var robot = new Robot();
            robot.Place(0, 0, "NORTH");
            robot.Right();

            if (robot.Report() != "0,0,EAST")
                Assert.Fail("Robot is not turning Right.");
        }

        [TestMethod()]
        public void LeftTest_TurningLeft()
        {
            var robot = new Robot();
            robot.Place(0, 0, "NORTH");
            robot.Left();

            if (robot.Report() != "0,0,WEST")
                Assert.Fail("Robot is not turning Right.");
        }
    }
}