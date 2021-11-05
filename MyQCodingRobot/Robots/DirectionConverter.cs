using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQCodingRobot.Robots
{
	public static class DirectionConverter
	{
		public static Direction ConvertToDirection(string? directionCode)
		{
			if (string.IsNullOrEmpty(directionCode))
			{
				throw new ArgumentNullException(nameof(directionCode));
			}

			char firstLetter = directionCode[0];
			if (Enum.IsDefined(typeof(Direction), (int)firstLetter))
			{
				return (Direction)firstLetter;
			}
			throw new Exception($"Direction {directionCode} not found.");
		}
	}
}
