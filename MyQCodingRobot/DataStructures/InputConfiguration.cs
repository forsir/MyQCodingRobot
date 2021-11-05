namespace MyQCodingRobot.DataStructures
{
	public class InputConfiguration
	{
		public string[][]? Map { get; set; }

		public RobotConfiguration? Start { get; set; }

		public string[]? Commands { get; set; }

		public int? Battery { get; set; }

		public void Check()
		{
			if (Map == null)
			{
				throw new Exception("Map is not configured");
			}
			if (Start == null)
			{
				throw new Exception("Start is not configured");
			}
			if (Commands == null)
			{
				throw new Exception("Commands is not configured");
			}
			if (Battery == null)
			{
				throw new Exception("Battery is not configured");
			}
		}
	}
}
