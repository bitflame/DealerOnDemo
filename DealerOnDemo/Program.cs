using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static DealerOnDemo.Location;
using static System.Net.Mime.MediaTypeNames;

namespace DealerOnDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Location currentLoc = new Location();
            printRoverStatus(currentLoc);
            string[] lines = File.ReadAllLines("hqRequestFile.txt");
            foreach (String line in lines)
            {
                int reqWidth, reqLength;
                String[] charSeparators = new String[] { "\n", "\r\n" };
                String[] items = line.Trim().Split(charSeparators, StringSplitOptions.None);
                foreach (var item in items)
                {
                //Use the first line to setup the grid 
                String pattern1 = @"[\d\s]+[^sSnNeEwW]$";
                    Regex rgx = new Regex(pattern1);
                    MatchCollection matches = rgx.Matches(item);
                    if (matches.Count()!=0)
                    {
                        String[] parts = item.Trim().Split(" ");
                        Int32.TryParse(parts[0], out reqWidth);
                        Int32.TryParse(parts[1], out reqLength);
                        int[] temp = new int[2];
                        temp[0] = reqWidth;
                        temp[1] = reqLength;
                        currentLoc.GridSize = temp;
                    }
                    //If the line starts with integers followed by N,S,E, and W, use it as your initial location
                    String pattern2 = @"^[\d\s]+[nNeEsSwW]$";
                    rgx = new Regex(pattern2, RegexOptions.IgnoreCase);
                    matches = rgx.Matches(item);
                    if (matches.Count != 0)
                    {
                        String[] parts = item.Trim().Split(" ");
                        Int32 xCoord, yCoord;
                        Char heading;
                        Int32.TryParse(parts[0], out xCoord);
                        Int32.TryParse(parts[1], out yCoord);
                        Char.TryParse(parts[2], out heading);
                        currentLoc.XLoc = xCoord;
                        currentLoc.YLoc = yCoord;
                        currentLoc.Heading = ToEnum<direction>(parts[2]);
                        Console.WriteLine("------------Printing the starting Location---");
                        printRoverStatus(currentLoc);
                    }
                    //If the line starts with characters, match only L,M, and R. to turn and move forward
                    String pattern3 = @"[L*M*R*]";
                    rgx = new Regex(pattern3, RegexOptions.IgnoreCase);
                    matches = rgx.Matches(item);
                    if (matches.Count != 0)
                    {
                        Console.WriteLine("You asked to do {0}",item);
                        foreach (var i in item)
                        {
                            if (i == 'L')
                            {
                                currentLoc = currentLoc.TurnLeft(currentLoc);
                                Console.WriteLine("Turning Left");
                                printRoverStatus(currentLoc);
                            }
                            if (i == 'M')
                            {
                                currentLoc = currentLoc.MoveOneCellForward(currentLoc);
                                Console.WriteLine("Moving one cell forward");
                                printRoverStatus(currentLoc);
                            }
                            if (i == 'R')
                            {
                                currentLoc = currentLoc.TurnRight(currentLoc);
                                Console.WriteLine("Turning Right");
                                printRoverStatus(currentLoc);
                            }
                        }
                        //Console.WriteLine("Done with the previous request. I am now at:{0} {1}", currentLoc.XLoc, currentLoc.YLoc);
                        printRoverStatus(currentLoc);

                    }   
                }
                //Any other letters and or combinations throw an invalid input exception    
            }   
        }
        private static void printRoverStatus(Location loc)
        {
          Console.WriteLine("I am currently at coordinates: {0} {1} facing:{2} direction. Grid Size is: {3} {4}",
              loc.XLoc,loc.YLoc,loc.Heading,loc.GridSize[0],loc.GridSize[1]);
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
