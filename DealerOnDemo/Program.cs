using System;
using System.Collections.Generic;
using System.Linq;

namespace DealerOnDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Location currentLoc = new Location();
            printRoverStatus(currentLoc);
            List<Location> goToRequests = new List<Location>();
            goToRequests.Add(new Location(2,3,Location.direction.E));
            printRoverStatus(goToRequests.First());
        }
        private static void printRoverStatus(Location loc)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (loc.TerrainGrid[i,j]==1)
                        Console.WriteLine("I am currently at coordinates: {0} {1} facing:{2} direction",i,j,loc.Heading);
                }
            }
        }
    }
}
