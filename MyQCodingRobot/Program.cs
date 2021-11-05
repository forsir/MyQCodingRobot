using MyQCodingRobot.DataStructures;
using MyQCodingRobot.Robots;
using MyQCodingRobot.Worlds;
using Newtonsoft.Json;

namespace MyQCodingRobot
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			if (args == null || args.Length < 1)
			{
				Console.WriteLine("There must be two params.");
				return;
			}

			if (!File.Exists(args[0]))
			{
				throw new Exception($"Input file {args[0]} does not exists");
			}

			string? serializedConfiguration = File.ReadAllText(args[0]);
			InputConfiguration? configuration = JsonConvert.DeserializeObject<InputConfiguration>(serializedConfiguration);
			if (configuration == null)
			{
				throw new ArgumentException($"Configuration must not be null.");
			}
			configuration.Check();

			var world = new World(configuration);
			world.Solve();

			OutputStructure? outputStructure = world.GetOutputStructure();
			string? output = JsonConvert.SerializeObject(outputStructure, Formatting.Indented);
			File.WriteAllText(args[1], output);

			Console.WriteLine(output);
		}
	}
}