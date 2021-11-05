using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyQCodingRobot.Worlds;

namespace MyQCodingRobot.Robots
{
    public class Robot
    {
        public Direction Facing { get; private set; }

        public int Battery { get; private set; }

        public (int X, int Y) Position { get; private set; }

        public Robot(Direction facing, int battery)
        {
            Facing = facing;
            Battery = battery;
        }

        private bool Consume(int consumption)
        {
            if (Battery < consumption)
            {
                return false;
            }
            Battery -= consumption;
            return true;
        }

        public bool ProvideOperation(RobotMoveConfiguration robotMoveConfiguration, World world)
        {
            if (!Consume(robotMoveConfiguration.Cost))
            {
                return false;
            }
            return robotMoveConfiguration.Function(this, world);
        }

        public bool TurnLeft()
        {
            Facing = Facing switch
            {
                Direction.North => Direction.West,
                Direction.East => Direction.North,
                Direction.South => Direction.East,
                Direction.West => Direction.South,
                _ => throw new NotImplementedException(),
            };
            return true;
        }

        public bool TurnRight()
        {
            Facing = Facing switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => throw new NotImplementedException(),
            };
            return true;
        }

        public bool Advance(World world)
        {
            (int X, int Y) newPosition = Facing switch
            {
                Direction.North => (Position.X, Position.Y - 1),
                Direction.East => (Position.X + 1, Position.Y),
                Direction.South => (Position.X, Position.Y + 1),
                Direction.West => (Position.X - 1, Position.Y),
                _ => throw new NotImplementedException(),
            };

            if (world.CanMove(newPosition))
            {
                Position = newPosition;
                return true;
            }
            return false;
        }

        public bool Back(World world)
        {
            (int X, int Y) newPosition = Facing switch
            {
                Direction.North => (Position.X, Position.Y + 1),
                Direction.East => (Position.X - 1, Position.Y),
                Direction.South => (Position.X, Position.Y - 1),
                Direction.West => (Position.X + 1, Position.Y),
                _ => throw new NotImplementedException(),
            };

            if (world.CanMove(newPosition))
            {
                Position = newPosition;
                return true;
            }
            return false;
        }

        public bool Clean(World world)
        {
            world.Clean(Position);
            return true;
        }
    }
}
