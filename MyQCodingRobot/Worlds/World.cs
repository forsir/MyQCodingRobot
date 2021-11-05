using MyQCodingRobot.DataStructures;
using MyQCodingRobot.Robots;

namespace MyQCodingRobot.Worlds
{
	public class World
	{
		private Room Room { get; init; }

		private Robot Robot { get; init; }

		public World(InputConfiguration configuration)
		{
			configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

			var robotMoveConfigurationConverter = new RobotMoveConfigurationConverter();
			IList<RobotMoveConfiguration> commands = robotMoveConfigurationConverter.GetByCode(configuration.Commands!);
			Robot = new Robot(new Position(configuration.Start!.X, configuration.Start.Y), DirectionConverter.ConvertToDirection(configuration.Start!.Facing), configuration.Battery!.Value, commands);

			var cellConfigurationConverter = new CellConfigurationConverter();
			Cell[][] worldCells = configuration.Map!.Select((m, y) => m.Select((c, x) => new Cell(x, y, cellConfigurationConverter.GetByCode(c))).ToArray()).ToArray();
			Room = new Room(worldCells);
		}

		public void Solve()
		{
			WriteToConsole();

			Room.VisitCell(Robot.Position);

			bool result;
			do
			{
				result = Robot.DoCommand(Room);
				WriteToConsole();
			} while (result);
		}

		public void WriteToConsole()
		{
			Console.WriteLine(Room.ToString(Robot));
			Console.WriteLine(Robot.ToString());
			Console.WriteLine();
		}

		public OutputStructure GetOutputStructure()
		{
			return new OutputStructure(Room, Robot);
		}
	}
}
