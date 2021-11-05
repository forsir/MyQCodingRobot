using MyQCodingRobot.Robots;

namespace MyQCodingRobot.Worlds
{
	public class Room
	{
		private Cell[][] Cells { get; init; }

		public IEnumerable<Cell> VisitedCells => Cells.SelectMany(cr => cr.Where(c => c.IsVisited));

		public IEnumerable<Cell> CleanedCells => Cells.SelectMany(cr => cr.Where(c => c.IsCleaned));

		public Room(Cell[][] cells)
		{
			Cells = cells;
		}

		private Cell GetCell(Position position)
		{
			if (position.Y < 0 || position.Y > Cells.Length)
			{
				return Cell.EmptyCell;
			}

			if (position.X < 0 || position.X > Cells[position.Y].Length)
			{
				return Cell.EmptyCell;
			}

			return Cells[position.Y][position.X];
		}

		public bool CanMoveTo(Position position)
		{
			return GetCell(position).Configuration.CanBeOccupied;
		}

		public void CleanCell(Position position)
		{
			GetCell(position).Clean();
		}

		public void VisitCell(Position position)
		{
			GetCell(position).VisitCell();
		}

		public override string ToString()
		{
			return string.Join("\n", Cells.Select(cr => string.Join(", ", cr.Select(c => c.ToString()))));
		}

		public string ToString(Robot robot)
		{
			return string.Join("\n", Cells.Select(cr => string.Join(", ", cr.Select(c =>
			{
				string? r = robot.Position == c.Coordinates ? robot.ToMark() : " ";
				return c.ToString() + r;
			}))));
		}
	}
}
