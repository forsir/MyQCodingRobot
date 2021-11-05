using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQCodingRobot.Worlds
{
    public class CellConfiguration
    {
        public CellType CellType { get; init; }

        public bool CanBeOccupied { get; init; }

        public string? CellCode { get; init; }

        public CellConfiguration(CellType cellType, bool canBeOccupied, string? cellCode)
        {
            CellType = cellType;
            CanBeOccupied = canBeOccupied;
            CellCode = cellCode;
        }

        public static CellConfiguration Space = new CellConfiguration(CellType.Space, true, "S");

        public static CellConfiguration Column = new CellConfiguration(CellType.Column, false, "C");

        public static CellConfiguration Wall = new CellConfiguration(CellType.Wall, false, null);
    }
}
