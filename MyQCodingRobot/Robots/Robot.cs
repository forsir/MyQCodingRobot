
using MyQCodingRobot.Worlds;

namespace MyQCodingRobot.Robots
{
	public class Robot
	{
		public Direction Facing { get; private set; }

		public int Battery { get; private set; }

		public Position Position { get; private set; }

		public RobotStrategy RobotStrategy { get; private set; }

		public Robot(Position position, Direction facing, int battery, IList<RobotMoveConfiguration> commands)
		{
			Facing = facing;
			Battery = battery;
			Position = position;
			RobotStrategy = new RobotStrategy(commands);
		}

		public bool DoCommand(Room room)
		{
			RobotMoveConfiguration? command = RobotStrategy.GetNextStep();

			if (command == null)
			{
				return false;
			}

			bool result = ProvideOperation(command, room);
			return RobotStrategy.SetByResult(result);
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

		public bool ProvideOperation(RobotMoveConfiguration robotMoveConfiguration, Room room)
		{
			robotMoveConfiguration = robotMoveConfiguration ?? throw new ArgumentNullException(nameof(robotMoveConfiguration));
			room = room ?? throw new ArgumentNullException(nameof(room));

			if (!Consume(robotMoveConfiguration.Cost))
			{
				return false;
			}
			bool result = robotMoveConfiguration.Function(this, room);
			room.VisitCell(Position);
			return result;
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

		public bool Advance(Room room)
		{
			room = room ?? throw new ArgumentNullException(nameof(room));

			Position newPosition = Facing switch
			{
				Direction.North => new Position(Position.X, Position.Y - 1),
				Direction.East => new Position(Position.X + 1, Position.Y),
				Direction.South => new Position(Position.X, Position.Y + 1),
				Direction.West => new Position(Position.X - 1, Position.Y),
				_ => throw new NotImplementedException(),
			};

			if (room.CanMoveTo(newPosition))
			{
				Position = newPosition;
				return true;
			}
			return false;
		}

		public bool Back(Room room)
		{
			room = room ?? throw new ArgumentNullException(nameof(room));

			Position newPosition = Facing switch
			{
				Direction.North => new Position(Position.X, Position.Y + 1),
				Direction.East => new Position(Position.X - 1, Position.Y),
				Direction.South => new Position(Position.X, Position.Y - 1),
				Direction.West => new Position(Position.X + 1, Position.Y),
				_ => throw new NotImplementedException(),
			};

			if (room.CanMoveTo(newPosition))
			{
				Position = newPosition;
				return true;
			}
			return false;
		}

		public bool Clean(Room room)
		{
			room = room ?? throw new ArgumentNullException(nameof(room));

			room.CleanCell(Position);
			return true;
		}

		public override string ToString()
		{
			return $"R: {(char)Facing} ({Position.X},{Position.Y}): {Battery}\n{RobotStrategy}";
		}

		public string ToMark()
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
