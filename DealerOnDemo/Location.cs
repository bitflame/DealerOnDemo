using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DealerOnDemo
{
    public class Location
    {
        public enum direction { N, S, E, W};
        private direction _heading;

        public direction Heading
        {
            get { return _heading; }
            set { _heading = value; }
        }


        private int[,] _terrainGrid = new int [5,5];
        public int[,] TerrainGrid
        {
            get { return _terrainGrid; }
            set { _terrainGrid = value; }
        }
        public void Coordinates(int x, int y)
        {
            this._terrainGrid[x, y] = 1;
            this.Heading = 0;
        }
        public Location()
        {
            this.TerrainGrid[0, 0] = 1;
            this.Heading = direction.N;
        }
        public Location(int x, int y, direction dir)
        {
            this._terrainGrid[x, y] = 1;
            this._heading = dir;
        }
    }
}
