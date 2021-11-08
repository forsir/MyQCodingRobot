using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQCodingRobot.Robots
{
	public class RobotStrategy
	{
		private readonly string[][] _backOffStrategies = new string[][] {
			new string[] { "TR", "A", "TL" },
			new string[] { "TR", "A", "TR" },
			new string[] { "TR", "A", "TR" },
			new string[] { "TR", "B", "TR", "A" },
			new string[] { "TL", "TL", "A" }
		};

		private int actualProfile = -1;

		private IList<RobotMoveConfiguration> CurrentBackOffCommands { get; set; } = new List<RobotMoveConfiguration>();

		private IList<RobotMoveConfiguration> CurrentCommands { get; set; }

		public RobotStrategy(IList<RobotMoveConfiguration> commands, string[][]? backOffStrategies = null)
		{
			CurrentCommands = commands;
			_backOffStrategies = backOffStrategies ?? _backOffStrategies;
		}

		public RobotMoveConfiguration? GetNextStep()
		{
			RobotMoveConfiguration? command = CurrentBackOffCommands.FirstOrDefault();
			if (command != null)
			{
				CurrentBackOffCommands = CurrentBackOffCommands.Skip(1).ToList();
				return command;
			}

			actualProfile = -1;
			command = CurrentCommands.FirstOrDefault();
			CurrentCommands = CurrentCommands.Skip(1).ToList();
			return command;
		}

		public bool SetByResult(bool result)
		{
			if (result)
			{
				return true;
			}

			return SetBackOffStrategy();
		}

		private bool SetBackOffStrategy()
		{
			actualProfile++;
			if (actualProfile >= _backOffStrategies.Length)
			{
				return false;
			}
			CurrentBackOffCommands = RobotMoveConfigurationConverter.GetByCode(_backOffStrategies[actualProfile]);
			return true;
		}

		public override string ToString()
		{
			if (CurrentBackOffCommands.Count > 0)
			{
				return $"B{actualProfile + 1}> {string.Join(", ", CurrentBackOffCommands)}";
			}
			else
			{
				return $"C > {string.Join(", ", CurrentCommands)}";
			}
		}
	}
}
