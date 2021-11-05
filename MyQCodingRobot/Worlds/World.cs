using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQCodingRobot.Worlds
{
    public class World
    {
        private Cell[][] Cells { get; init; }

        public World(Cell[][] cells)
        {
            Cells = cells;
        }

        private Cell GetCell((int X, int Y) position)
        {
            if (position.X < 0 || position.X > Cells[0].Length)
            {
                return Cell.EmptyCell;
            }

            if (position.Y < 0 || position.Y > Cells.Length)
            {
                return Cell.EmptyCell;
            }
            return Cells[position.Y][position.X];
        }

        public bool CanMove((int X, int Y) position)
        {
            return GetCell(position).Configuration.CanBeOccupied;
        }

        public void Clean((int X, int Y) position)
        {
            GetCell(position).Clean();
        }
    }
}
