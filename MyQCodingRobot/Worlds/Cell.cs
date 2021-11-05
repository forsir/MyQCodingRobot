﻿
namespace MyQCodingRobot.Worlds
{
    public class Cell
    {
        public static Cell EmptyCell => new Cell(-1, -1, CellConfiguration.Wall);


        public Position Coordinates { get; private set; }

        public CellConfiguration Configuration { get; private set; }

        public bool IsCleaned { get; private set; }

        public Cell(int x, int y, CellConfiguration configuration)
        {
            Coordinates = new Position(x, y);
            Configuration = configuration;
        }

        public void Clean()
        {
            IsCleaned = true;
        }

        public override string ToString()
        {
            return $"{Coordinates}:{Configuration.CellCode ?? " "}{(IsCleaned ? "c" : " ")}";
        }

        public string ToStringShort()
        {
            return Configuration.CellCode ?? "null";
        }
    }
}
