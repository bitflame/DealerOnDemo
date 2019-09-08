using System;
using System.Collections.Generic;
using System.Text;

namespace DealerOnDemo
{
    class hqRequest
    {
        private int [,] _terrainGrid;

        public int [,] TerrainGrid
        {
            get { return  _terrainGrid; }
            set { _terrainGrid = value; }
        }

    }
}
