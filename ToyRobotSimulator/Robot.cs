using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotSimulator
{
    public class Robot : IRobot
    {
        private Table _table;
        private Position _position;
        private bool _isOnTable;

        public Robot()
        {
            _table = new Table(5, 5);
            _isOnTable = false;
        }

        public Robot(Table table)//giving the client to define the size of the table
        {
            _table = table;
            _isOnTable = false;
        }

        public void Place(int x, int y, string strDirection)
        {
            Direction direction;

            if (Enum.TryParse(strDirection.ToUpper(), out direction) == false)//check if given direction is valid
                throw new Exception("Invalid direction!");

            var position = new Position(x, y, direction);

            if (IsSafe(position))
            {
                _position = position;
                _isOnTable = true;
            }
        }

        public void Move()
        {
            if (_isOnTable == false)//dont move if you are not placed on the table
                return;

            var newPosition = getNewPosition(_position);

            if (IsSafe(newPosition))//check if its safe to move to the given new position
            {
                _position = newPosition;
            }
        }

        public void Left()
        {
            if (_isOnTable == false)//dont move if you are not placed on the table
                return;

            _position.Face = Rotate(RotateTo.LEFT, 90, _position.Face);
        }

        public void Right()
        {
            if (_isOnTable == false)//dont move if you are not placed on the table
                return;

            _position.Face = Rotate(RotateTo.RIGHT, 90, _position.Face);
        }

        //by receiving degree in the future we can turn the robot more flexibly e.g. 45 degree
        private Direction Rotate(RotateTo rotDir, int degree, Direction currentDirection)
        {
            var variant = (rotDir == RotateTo.LEFT ? -1 : 1) * degree;//depending on turning left or right the value of degree should be added or subtracted            
            var newFace = (int)currentDirection + variant;
            newFace = newFace < 0 ? newFace + 360 : newFace;//normalizing negative values to their equivalent positive values e.g. -90 to 270
            newFace = newFace == 360 ? 0 : newFace;//normalizing 360 degree to its 0 degree eequivalent
            return (Direction)newFace;
        }

        public string Report()
        {
            return _isOnTable ? string.Format("{0},{1},{2}", _position.X, _position.Y, _position.Face) : string.Empty;
        }

        public string CommandByText(string commandStr)
        {
            var res = string.Empty;
            var commandParts = commandStr.Split(' ');
            Command command;

            if (Enum.TryParse(commandParts[0].ToUpper(), out command) == false)//check if given command is valid
                throw new Exception("Invalid command!");

            switch (command)
            {
                case Command.PLACE:
                    var commandParams = commandParts[1].Split(',');
                    Place(int.Parse(commandParams[0]), int.Parse(commandParams[1]), commandParams[2]);
                    break;

                case Command.MOVE:
                    Move();
                    break;

                case Command.LEFT:
                    Left();
                    break;

                case Command.RIGHT:
                    Right();
                    break;

                case Command.REPORT:
                    res = Report();
                    break;
            }

            return res;
        }

        private bool IsSafe(Position newPosition)
        {
            if (newPosition.X < 0 || newPosition.X > _table.Height - 1)//don't fall from table when going left or right
                return false;

            if (newPosition.Y < 0 || newPosition.Y > _table.Width - 1)//don't fall from table when going up or down
                return false;

            return true;
        }

        private Position getNewPosition(Position currenctPosition)
        {
            return new Position
            (
                getNextX(currenctPosition.X, currenctPosition.Face),
                getNextY(currenctPosition.Y, currenctPosition.Face),
                currenctPosition.Face
            );
        }

        private int getNextX(int currentX, Direction direction)
        {
            switch (direction)
            {
                case Direction.EAST:
                    return currentX + 1;

                case Direction.WEST:
                    return currentX - 1;

                default:
                    return currentX;
            }
        }

        private int getNextY(int currentY, Direction direction)
        {
            switch (direction)
            {
                case Direction.NORTH:
                    return currentY + 1;

                case Direction.SOUTH:
                    return currentY - 1;

                default:
                    return currentY;
            }
        }
    }

}
