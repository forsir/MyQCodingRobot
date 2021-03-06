using MyQCodingRobot.Robots;
using MyQCodingRobot.Worlds;

namespace MyQCodingRobot.DataStructures
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
	public class OutputStructure
	{
		public Position[] visited { get; set; }

		public Position[] cleaned { get; set; }

		public RobotConfiguration final { get; set; }

		public int battery { get; set; }

		public OutputStructure(Room room, Robot robot)
		{
			room = room ?? throw new ArgumentNullException(nameof(room));
			robot = robot ?? throw new ArgumentNullException(nameof(robot));

			visited = room.VisitedCells.Select(c => c.Coordinates).ToArray();
			cleaned = room.CleanedCells.Select(c => c.Coordinates).ToArray();
			final = new RobotConfiguration() { X = robot.Position.X, Y = robot.Position.Y, Facing = ((char)robot.Facing).ToString() };
			battery = robot.Battery;
		}
	}
}
