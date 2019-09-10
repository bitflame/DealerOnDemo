using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DealerOnDemo
{
    public class Location
    {
        public enum direction { N, S, E, W };
        private direction _heading;
        private int width = 0, length = 0;
        public direction Heading
        {
            get { return _heading; }
            set { _heading = value; }
        }
        public direction TurnLeft(Location currLoc)
        {
            if (currLoc._heading == direction.E) currLoc._heading = direction.N;
            if (currLoc._heading == direction.N) currLoc._heading = direction.W;
            if (currLoc._heading == direction.W) currLoc._heading = direction.S;
            if (currLoc._heading == direction.S) currLoc._heading = direction.E;
            return currLoc.Heading;
        }
        public direction TurnRight(Location currLoc)
        {
            if (currLoc._heading == direction.E) currLoc._heading = direction.S;
            if (currLoc._heading == direction.N) currLoc._heading = direction.E;
            if (currLoc._heading == direction.W) currLoc._heading = direction.N;
            if (currLoc._heading == direction.S) currLoc._heading = direction.W;
            return currLoc.Heading;
        }
        public Location MoveOneCellForward(Location currLoc)
        {
            if (currLoc._heading == direction.E) currLoc.XLoc++;
            if (currLoc._heading == direction.N) currLoc.YLoc++;
            if (currLoc._heading == direction.W) currLoc.XLoc--;
            if (currLoc._heading == direction.S) currLoc.YLoc--;
            return currLoc;
        }
        private int[,] _terrainGrid = new int[1, 1];
        public int[,] TerrainGrid
        {
            get { return _terrainGrid; }
            set { _terrainGrid = value; }
        }
        private int _xLoc;

        public int XLoc
        {
            get { return _xLoc; }
            set { _xLoc = value; }
        }
        private int _yLoc;

        public int YLoc
        {
            get { return _yLoc; }
            set { _yLoc = value; }
        }


        public Location()
        {
            this.TerrainGrid[0, 0] = 1;
            this.Heading = direction.N;
        }
        public Location(int x, int y, direction dir = direction.N)
        {
            this.XLoc = x;
            this.YLoc = y;
            this._heading = dir;
        }
        public int[,] SetGridSize(int x, int y)
        {
            this._terrainGrid = new int[x, y];
            Console.WriteLine("Grid Size set to: {0}, {1} as per your instructions.", x, y);
            return _terrainGrid;
        }
    }
}
