
using MyQCodingRobot.Worlds;

namespace MyQCodingRobot.Robots
{
    public class Robot
    {
        public Direction Facing { get; private set; }

        public int Battery { get; private set; }

        public Position Position { get; private set; }

        public IList<RobotMoveConfiguration> Commands { get; private set; }

        public Robot(Position position, Direction facing, int battery, IList<RobotMoveConfiguration> commands)
        {
            Facing = facing;
            Battery = battery;
            Position = position;
            Commands = commands;
        }

        public bool DoCommand(World world)
        {
            if (Commands.Count == 0)
            {
                return false;
            }

            RobotMoveConfiguration? command = Commands.First();
            Commands = Commands.Skip(1).ToList();
            bool result = ProvideOperation(command, world);
            return result;
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
            Position newPosition = Facing switch
            {
                Direction.North => new Position(Position.X, Position.Y - 1),
                Direction.East => new Position(Position.X + 1, Position.Y),
                Direction.South => new Position(Position.X, Position.Y + 1),
                Direction.West => new Position(Position.X - 1, Position.Y),
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
            Position newPosition = Facing switch
            {
                Direction.North => new Position(Position.X, Position.Y + 1),
                Direction.East => new Position(Position.X - 1, Position.Y),
                Direction.South => new Position(Position.X, Position.Y - 1),
                Direction.West => new Position(Position.X + 1, Position.Y),
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

        public override string ToString()
        {
            return $"{(char)Facing}({Position.X},{Position.Y}):{Battery}\n{String.Join(", ", Commands)}";
        }

        public string ToShort()
        {
            return Facing switch
            {
                Direction.North => "^",
                Direction.East => ">",
                Direction.South => "v",
                Direction.West => "<",
                _ => throw new NotImplementedException(),
            };
        }
    }
}
