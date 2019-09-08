using System;
using System.Collections.Generic;
using System.IO;
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
            string[] lines = File.ReadAllLines("hqRequestFile.txt");
            int reqWidth, reqLength;
            Int32.TryParse(lines[0], out reqWidth);
            Int32.TryParse(lines[1], out reqLength);
            currentLoc.SetGridSize(reqWidth, reqLength);
            foreach (String line in lines.Skip(1))
            {
                processRequest(line);
            }
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
        //
        private static void processRequest(String request)
        {
            String[] charSeparators = new String[] {" ", "\n", "\r\n" };
            String[] items = request.Split(charSeparators, StringSplitOptions.None);
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
    }
}
