using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQCodingRobot.Robots
{
	public class RobotStrategy
	{
		private string[][] strategies = new string[][]  {
new string[]      { "TR", "A", "TL" },
new string[]      {"TR", "A", "TR"},
new string[]      { "TR", "A",   "TR"},
new string[]      { "TR", "B",   "TR"},
new string[]      { "TL", "TL", "A"}
};

		private IList<RobotMoveConfiguration> Commands { get; set; }

		public RobotStrategy(IList<RobotMoveConfiguration> commands)
		{
			Commands = commands;
		}

		public RobotMoveConfiguration? GetNextStep()
		{
			RobotMoveConfiguration? command = Commands.FirstOrDefault();
			Commands = Commands.Skip(1).ToList();
			return command;
		}

		public void SetResult(bool result)
		{
			if (!result)
			{
				SetNextStrategy();
			}
		}

		private void SetNextStrategy() { }
	}
}
