using System.Reflection;

namespace MyQCodingRobot.Robots
{
    public class RobotMoveConfigurationConverter
    {
        private readonly Dictionary<string, RobotMoveConfiguration> RobotMoveConfigurations;

        public RobotMoveConfigurationConverter()
        {
            RobotMoveConfigurations = typeof(RobotMoveConfiguration)
               .GetFields(BindingFlags.Public | BindingFlags.Static)
               .Where(f => f.FieldType == typeof(RobotMoveConfiguration))
               .ToDictionary(f => ((RobotMoveConfiguration)f.GetValue(null)!).Code,
                             f => (RobotMoveConfiguration)f.GetValue(null)!);
        }

        public RobotMoveConfiguration GetByCode(string code)
        {
            if (RobotMoveConfigurations.TryGetValue(code, out RobotMoveConfiguration? RobotMoveConfiguration))
            {
                return RobotMoveConfiguration;
            }

            throw new Exception();
        }

        public IList<RobotMoveConfiguration> GetByCode(IEnumerable<string> codes)
        {
            return codes.Select(c => GetByCode(c)).ToList();
        }
    }
}
