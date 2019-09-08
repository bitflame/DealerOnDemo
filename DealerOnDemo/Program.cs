using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static DealerOnDemo.Location;

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
                currentLoc = processRequest(line);
            }
            printRoverStatus(currentLoc);
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
        private static Location processRequest(String request)
        {
            int xCoord, yCoord;
            Char heading;
            Char inst;
            Location currentLoc = new Location();
            String[] charSeparators = new String[] {" ", "\n", "\r\n" };
            String[] items = request.Split(charSeparators, StringSplitOptions.None);
            for (int i = 0; i < items.Length; i++)
            {
                Console.WriteLine(items[i]);
                //If it is an integer update destination's x coordinate 
                if (Int32.TryParse(items[i], out xCoord)) i++;
                //If it is not an integer throw an error stating something is wrong and return
                else if (!Int32.TryParse(items[i], out yCoord))
                {
                    Console.WriteLine("Error - Expecting a second integer");
                    return currentLoc;
                }
                //Read the next token, which should be another integer and update the destination's y coordinate
                else if (Int32.TryParse(items[i], out yCoord))
                {
                    currentLoc = new Location(xCoord, yCoord);
                    i++;
                }
                //If you have a value for heading, update the Current Location with the new value
                else if (char.TryParse(items[i], out heading))
                {

                    currentLoc.Heading = ToEnum<direction>(items[i]);
                }
                //If it is L, change heading to your left
                else if (char.TryParse(items[i], out inst) && inst == 'L')
                    currentLoc.Heading = currentLoc.TurnLeft(currentLoc);
                //If it is R, change heading to your right
                else if (char.TryParse(items[i], out inst) && inst == 'R')
                    currentLoc.Heading = currentLoc.TurnRight(currentLoc);
                //If it is M, move forward one cell in whatever direction you were
                else if (char.TryParse(items[i], out inst) && inst == 'M')
                    currentLoc.MoveOneCellForward(currentLoc);
                else Console.WriteLine("Error at:{0}. Please check the request and resubmit.", items[i]); 
            }
            return currentLoc;
        }
        public static T ToEnum<T>(string @string)
        {
            //From https://www.codeproject.com/Articles/78600/C-Enum-with-Char-Valued-Items
            if (string.IsNullOrEmpty(@string))
            {
                throw new ArgumentException("Argument null or empty");
            }
            if (@string.Length > 1)
            {
                throw new ArgumentException("Argument length greater than one");
            }
            return (T)Enum.ToObject(typeof(T), @string[0]);
        }
    }
}
