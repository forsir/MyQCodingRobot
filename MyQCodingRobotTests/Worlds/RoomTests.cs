using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyQCodingRobot;
using MyQCodingRobot.Worlds;

namespace MyQCodingRobotTests.Worlds
{
	[TestClass]
	public class RoomTests
	{
		[TestMethod]
		public void RoomTest_CanMoveTo()
		{
			// Arrange
			Room room = new RoomTestFixture().SetOneCellRoom().Generate();
			var position = new Position(0, 0);

			// Act
			bool result = room.CanMoveTo(position);

			// Assert
			Assert.IsTrue(result);
		}

		[DataRow(-1, 0)]
		[DataRow(0, -1)]
		[DataRow(0, 2)]
		[DataRow(1, 3)]
		[DataRow(2, 0)]
		[DataTestMethod]
		public void RoomTests_CanMoveTo_ChekcBoundaries(int x, int y)
		{
			// Arrange
			Room room = new RoomTestFixture().SetTriangleRoom().Generate();
			var position = new Position(x, y);

			// Act
			bool result = room.CanMoveTo(position);

			// Assert
			Assert.IsFalse(result);
		}

		[DataRow(0, 0)]
		[DataRow(0, 1)]
		[DataRow(1, 0)]
		[DataTestMethod]
		public void RoomTests_CanMoveTo_ChekcSpace(int x, int y)
		{
			// Arrange
			Room room = new RoomTestFixture().SetTriangleRoom().Generate();
			var position = new Position(x, y);

			// Act
			bool result = room.CanMoveTo(position);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void RoomTests_CleanCell()
		{
			// Arrange
			Room room = new RoomTestFixture().SetOneCellRoom().Generate();
			var position = new Position(0, 0);

			// Act
			bool cleaned = room.GetCell(position).IsCleaned;
			room.CleanCell(position);

			// Assert
			Assert.IsFalse(cleaned);
			Assert.IsTrue(room.GetCell(position).IsCleaned);
		}

		[TestMethod]
		public void RoomTests_VisitCell_ExpectedBehavior()
		{
			// Arrange
			Room room = new RoomTestFixture().SetOneCellRoom().Generate();
			var position = new Position(0, 0);

			// Act
			bool visited = room.GetCell(position).IsVisited;
			room.VisitCell(position);

			// Assert
			Assert.IsFalse(visited);
			Assert.IsTrue(room.GetCell(position).IsVisited);
		}

		private class RoomTestFixture
		{
			public Cell[][]? Cells { get; set; }

			public RoomTestFixture SetOneCellRoom()
			{
				var cell = new Cell(0, 0, CellConfiguration.Space);
				Cells = new Cell[][] { new Cell[] { cell } };
				return this;
			}

			public RoomTestFixture SetTriangleRoom()
			{
				Cells = new Cell[][] {
					new Cell[] { new Cell(0, 0, CellConfiguration.Space), new Cell(0, 1, CellConfiguration.Space) },
					new Cell[] { new Cell(1, 0, CellConfiguration.Space) }
				};
				return this;
			}

			public Room Generate()
			{
				return new Room(Cells!);
			}
		}
	}
}
