using System.Reflection;

namespace MyQCodingRobot.Robots
{
	public static class RobotMoveConfigurationConverter
	{
		private static readonly Dictionary<string, RobotMoveConfiguration> RobotMoveConfigurations = typeof(RobotMoveConfiguration)
				.GetFields(BindingFlags.Public | BindingFlags.Static)
				.Where(f => f.FieldType == typeof(RobotMoveConfiguration))
				.ToDictionary(f => ((RobotMoveConfiguration)f.GetValue(null)!).Code,
							 f => (RobotMoveConfiguration)f.GetValue(null)!);

		public static RobotMoveConfiguration GetByCode(string code)
		{
			if (RobotMoveConfigurations.TryGetValue(code, out RobotMoveConfiguration? robotMoveConfiguration))
			{
				return robotMoveConfiguration;
			}

			throw new Exception($"Robot move with code {code} not found.");
		}

		public static IList<RobotMoveConfiguration> GetByCode(IEnumerable<string> codes)
		{
			return codes.Select(c => GetByCode(c)).ToList();
		}
	}
}
