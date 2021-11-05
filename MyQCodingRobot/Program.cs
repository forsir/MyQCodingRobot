using MyQCodingRobot.Configurations;
using MyQCodingRobot.Robots;
using MyQCodingRobot.Worlds;
using Newtonsoft.Json;

namespace MyQCodingRobot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("args is files");
                return;
            }

            if (!File.Exists(args[0]))
            {
                throw new Exception("File does not exists");
            }

            string? serializedConfiguration = File.ReadAllText(args[0]);
            Configuration? configuration = JsonConvert.DeserializeObject<Configuration>(serializedConfiguration);
            if (configuration == null)
            {
                throw new ArgumentException($"Configuration must be null.");
            }
            configuration.Check();
            var robot = new Robot((configuration.Start!.X, configuration.Start.Y), DirectionConverter.ConvertToDirection(configuration.Start!.Facing), configuration.Battery!.Value);

            var cellConfigurationConverter = new CellConfigurationConverter();
            Cell[][] worldCells = configuration.Map!.Select((m, y) => m.Select((c, x) => new Cell(x, y, cellConfigurationConverter.GetByCode(c))).ToArray()).ToArray();
            var world = new World(worldCells);
            Console.WriteLine(world.ToString());
            Console.WriteLine(robot.ToString());
        }
    }
}