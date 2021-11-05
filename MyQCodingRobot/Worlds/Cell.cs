using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQCodingRobot.Worlds
{
    public class Cell
    {
        public static Cell EmptyCell => new Cell(-1, -1, CellConfiguration.Wall);


        public (int X, int Y) Coordinates { get; private set; }

        public CellConfiguration Configuration { get; private set; }

        public bool IsCleaned { get; private set; }

        public Cell(int x, int y, CellConfiguration configuration)
        {
            Coordinates = (x, y);
            Configuration = configuration;
        }

        public void Clean()
        {
            IsCleaned = true;
        }
    }
}
