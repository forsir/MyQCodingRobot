using System.Reflection;

namespace MyQCodingRobot.Robots
{
	public class RobotMoveConfigurationConverter
	{
		private readonly Dictionary<string, RobotMoveConfiguration> _robotMoveConfigurations;

		public RobotMoveConfigurationConverter()
		{
			_robotMoveConfigurations = typeof(RobotMoveConfiguration)
				.GetFields(BindingFlags.Public | BindingFlags.Static)
				.Where(f => f.FieldType == typeof(RobotMoveConfiguration))
				.ToDictionary(f => ((RobotMoveConfiguration)f.GetValue(null)!).Code,
							 f => (RobotMoveConfiguration)f.GetValue(null)!);
		}

		public RobotMoveConfiguration GetByCode(string code)
		{
			if (_robotMoveConfigurations.TryGetValue(code, out RobotMoveConfiguration? robotMoveConfiguration))
			{
				return robotMoveConfiguration;
			}

			throw new Exception();
		}

		public IList<RobotMoveConfiguration> GetByCode(IEnumerable<string> codes)
		{
			return codes.Select(c => GetByCode(c)).ToList();
		}
	}
}
