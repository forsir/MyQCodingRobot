
using MyQCodingRobot.Worlds;

namespace MyQCodingRobot.Robots
{
    public class RobotMoveConfiguration
    {
        public RobotMove RobotMove { get; init; }

        public string Code { get; init; }

        public int Cost { get; init; }

        public Func<Robot, World, bool> Function { get; init; }

        public RobotMoveConfiguration(RobotMove robotMove, string code, int cost, Func<Robot, World, bool> function)
        {
            RobotMove = robotMove;
            Code = code;
            Cost = cost;
            Function = function;
        }

        public override string ToString()
        {
            return $"{Code}";
        }

        public static readonly RobotMoveConfiguration TurnLeft = new(RobotMove.TurnLeft, "TL", 1, (r, w) => r.TurnLeft());
        public static readonly RobotMoveConfiguration TurnRight = new(RobotMove.TurnRight, "TR", 1, (r, w) => r.TurnRight());
        public static readonly RobotMoveConfiguration Advance = new(RobotMove.Advance, "A", 2, (r, w) => r.Advance(w));
        public static readonly RobotMoveConfiguration Back = new(RobotMove.Back, "B", 3, (r, w) => r.Back(w));
        public static readonly RobotMoveConfiguration Clean = new(RobotMove.Clean, "C", 5, (r, w) => r.Clean(w));
    }
}
