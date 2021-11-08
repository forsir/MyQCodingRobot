using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyQCodingRobot;
using MyQCodingRobot.Robots;
using MyQCodingRobot.Worlds;

namespace MyQCodingRobotTests.Robots
{
	[TestClass()]
	public class RobotTests
	{
		[TestMethod()]
		public void ProvideOperationTest_TestBattery()
		{
			// arrange
			var robotFixture = new RobotTestFixture();
			Robot robot = robotFixture.Generate(100);
			var robotMove = new RobotMoveConfiguration("", 40, (r, w) => true);

			// act
			bool result = robot.ProvideOperation(robotMove, robotFixture.GetRoom());

			// assert
			Assert.AreEqual(60, robot.Battery);
			Assert.IsTrue(result);
		}

		[TestMethod()]
		public void ProvideOperationTest_TestBatteryLow()
		{
			// arrange
			var robotFixture = new RobotTestFixture();
			Robot robot = robotFixture.Generate(30);
			var robotMove = new RobotMoveConfiguration("", 40, (r, w) => true);

			// act
			bool result = robot.ProvideOperation(robotMove, robotFixture.GetRoom());

			// assert
			Assert.AreEqual(30, robot.Battery);
			Assert.IsFalse(result);
		}

		[TestMethod()]
		public void ProvideOperationTest_DontDoOperation()
		{
			// arrange
			var robotFixture = new RobotTestFixture();
			Robot robot = robotFixture.Generate(100);
			var robotMove = new RobotMoveConfiguration("", 40, (r, w) => false);

			// act
			bool result = robot.ProvideOperation(robotMove, robotFixture.GetRoom());

			// assert
			Assert.AreEqual(60, robot.Battery);
			Assert.IsFalse(result);
		}

		private class RobotTestFixture
		{
			public Robot? Robot { get; set; }

			public Position? Position { get; set; }

			public Direction? Facing { get; set; }

			public IList<RobotMoveConfiguration> Moves { get; set; } = new List<RobotMoveConfiguration>();

			public Room GetRoom()
			{
				var cell = new Cell(0, 0, CellConfiguration.Space);
				var array = new Cell[][] { new Cell[] { cell } };
				return new Room(array);
			}

			public RobotTestFixture SetPosition(int x, int y)
			{
				Position = new Position(x, y);
				return this;
			}

			public Robot Generate(int battery)
			{
				Robot = new Robot(Position ?? new Position(0, 0), Facing ?? Direction.North, battery, Moves);
				return Robot;
			}
		}
	}
}
