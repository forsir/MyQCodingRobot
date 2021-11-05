using MyQCodingRobot.Configurations;
using MyQCodingRobot.Robots;
using MyQCodingRobot.Worlds;
using Newtonsoft.Json;

namespace MyQCodingRobot
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			if (args == null || args.Length == 0)
			{
				Console.WriteLine("args is files");
				return;
			}

			if (!File.Exists(args[0]))
			{
				throw new Exception("File does not exists");
			}

			string? serializedConfiguration = File.ReadAllText(args[0]);
			TaskConfiguration? configuration = JsonConvert.DeserializeObject<TaskConfiguration>(serializedConfiguration);
			if (configuration == null)
			{
				throw new ArgumentException($"Configuration must be null.");
			}
			configuration.Check();
			var robotMoveConfigurationConverter = new RobotMoveConfigurationConverter();
			IList<RobotMoveConfiguration> commands = robotMoveConfigurationConverter.GetByCode(configuration.Commands!);
			var robot = new Robot(new Position(configuration.Start!.X, configuration.Start.Y), DirectionConverter.ConvertToDirection(configuration.Start!.Facing), configuration.Battery!.Value, commands);

			var cellConfigurationConverter = new CellConfigurationConverter();
			Cell[][] worldCells = configuration.Map!.Select((m, y) => m.Select((c, x) => new Cell(x, y, cellConfigurationConverter.GetByCode(c))).ToArray()).ToArray();
			var world = new World(worldCells);

			bool result;
			do
			{
				result = robot.DoCommand(world);
				Console.WriteLine(world.ToString(robot));
				Console.WriteLine(robot.ToString());
				Console.WriteLine();
			} while (result);
		}
	}
}