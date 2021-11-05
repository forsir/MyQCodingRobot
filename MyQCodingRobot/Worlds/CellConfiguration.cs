using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQCodingRobot.Worlds
{
	public class CellConfiguration
	{
		public bool CanBeOccupied { get; init; }

		public string? CellCode { get; init; }

		public CellConfiguration(string? cellCode, bool canBeOccupied)
		{
			CanBeOccupied = canBeOccupied;
			CellCode = cellCode;
		}

		public static readonly CellConfiguration Space = new CellConfiguration("S", true);

		public static readonly CellConfiguration Column = new CellConfiguration("C", false);

		public static readonly CellConfiguration Wall = new CellConfiguration("null", false);
	}
}
