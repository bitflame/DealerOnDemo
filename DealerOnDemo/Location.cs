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
        public Location TurnLeft(Location currLoc)
        {
            if ((Char)currLoc.Heading == 'E') currLoc.Heading = direction.N;
            if ((Char)currLoc.Heading == 'N') currLoc.Heading = direction.W;
            if ((Char)currLoc.Heading == 'W') currLoc.Heading = direction.S;
            if ((Char)currLoc.Heading == 'S') currLoc.Heading = direction.E;
            return currLoc;
        }
        public Location TurnRight(Location currLoc)
        {
            if ((Char)currLoc.Heading == 'E') currLoc.Heading = direction.S;
            if ((Char)currLoc.Heading == 'N') currLoc.Heading = direction.E;
            if ((Char)currLoc.Heading == 'W') currLoc.Heading = direction.N;
            if ((Char)currLoc.Heading == 'S') currLoc.Heading = direction.W;
            return currLoc;
        }
        public Location MoveOneCellForward(Location currLoc)
        {
            if (currLoc._heading == direction.E) { currLoc.XLoc = currLoc.XLoc + 1; }
            if (currLoc._heading == direction.N) { currLoc.YLoc = currLoc.YLoc + 1; }
            if (currLoc._heading == direction.W) { currLoc.XLoc = currLoc.XLoc - 1; }
            if (currLoc._heading == direction.S) { currLoc.YLoc = currLoc.YLoc - 1; }
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
        private int[] _gridSize = new int[] { 0, 0};       

        public int[] GridSize
        {
            get { return _gridSize; }
            set { _gridSize = value; }
        }

        //public int[,] SetGridSize(int x, int y)
        //{
        //    this._terrainGrid = new int[x, y];
        //    Console.WriteLine("Grid Size set to: {0}, {1} as per your instructions.", x, y);
        //    return _terrainGrid;
        //}
        //public int [] GetGridSize(Location currLoc)
        //{
        //    int[] x_y_Values = new int[2];
        //    x_y_Values[0] = currLoc.XLoc;
        //    x_y_Values[1] = currLoc.YLoc;
        //    return x_y_Values;
        //}
    }
}
