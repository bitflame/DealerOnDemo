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
            if ((Char)currLoc.Heading == 'E') currLoc.Heading = (direction)Enum.ToObject(typeof(direction), 'N');
            //if ((Char)currLoc.Heading == 'N') currLoc.Heading = direction.W;
            else if ((Char)currLoc.Heading == 'N') currLoc.Heading = (direction)Enum.ToObject(typeof(direction),'W');
            else if ((Char)currLoc.Heading == 'W') currLoc.Heading = (direction)Enum.ToObject(typeof(direction), 'S');
            else if ((Char)currLoc.Heading == 'S') currLoc.Heading = (direction)Enum.ToObject(typeof(direction), 'E');
            return currLoc;
        }
        public Location TurnRight(Location currLoc)
        {
            if ((Char)currLoc.Heading == 'E') currLoc.Heading = (direction)Enum.ToObject(typeof(direction), 'S');
            else if ((Char)currLoc.Heading == 'N') currLoc.Heading = (direction)Enum.ToObject(typeof(direction), 'E');
            else if ((Char)currLoc.Heading == 'W') currLoc.Heading = (direction)Enum.ToObject(typeof(direction), 'N');
            else if ((Char)currLoc.Heading == 'S') currLoc.Heading = (direction)Enum.ToObject(typeof(direction), 'W');
            return currLoc;
        }
        public Location MoveOneCellForward(Location currLoc)
        {
            if ((Char)currLoc._heading == 'E') { currLoc.XLoc = currLoc.XLoc + 1; }
            else if ((Char)currLoc._heading == 'N') { currLoc.YLoc = currLoc.YLoc + 1; }
            else if ((Char)currLoc._heading == 'W') { currLoc.XLoc = currLoc.XLoc - 1; }
            else if ((Char)currLoc._heading == 'S') { currLoc.YLoc = currLoc.YLoc - 1; }
            return currLoc;
        }
        private int[,] _terrainGrid ;
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
            this.TerrainGrid = new int [1,1];
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
    }
}
