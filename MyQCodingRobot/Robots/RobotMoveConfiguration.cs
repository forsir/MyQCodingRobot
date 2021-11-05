
using MyQCodingRobot.Worlds;

namespace MyQCodingRobot.Robots
{
	public class RobotMoveConfiguration
	{
		public string Code { get; init; }

		public int Cost { get; init; }

		public Func<Robot, Room, bool> Function { get; init; }

		public RobotMoveConfiguration(string code, int cost, Func<Robot, Room, bool> function)
		{
			Code = code;
			Cost = cost;
			Function = function;
		}

		public override string ToString()
		{
			return $"{Code}";
		}

		public static readonly RobotMoveConfiguration TurnLeft = new("TL", 1, (r, w) => r.TurnLeft());
		public static readonly RobotMoveConfiguration TurnRight = new("TR", 1, (r, w) => r.TurnRight());
		public static readonly RobotMoveConfiguration Advance = new("A", 2, (r, w) => r.Advance(w));
		public static readonly RobotMoveConfiguration Back = new("B", 3, (r, w) => r.Back(w));
		public static readonly RobotMoveConfiguration Clean = new("C", 5, (r, w) => r.Clean(w));
	}
}
