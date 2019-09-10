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
                        currentLoc.SetGridSize(reqWidth, reqLength);
                    }
                    //If the line starts with integers followed by N,S,E, and W, use it as your initial location
                    String pattern2 = @"^\d\s+[nNeEsSwW]{1+}$";
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
                        currentLoc = new Location(xCoord, yCoord);
                        currentLoc.Heading = ToEnum<direction>(parts[2]);
                        printRoverStatus(currentLoc);
                    }
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
                                currentLoc.Heading = currentLoc.TurnLeft(currentLoc);
                                Console.WriteLine("Turning Left");
                            }
                            if (i == 'M')
                            {
                                currentLoc = currentLoc.MoveOneCellForward(currentLoc);
                                Console.WriteLine("Moving one cell forward");
                            }
                            if (i == 'R')
                            {
                                currentLoc.Heading = currentLoc.TurnRight(currentLoc);
                                Console.WriteLine("Turning Right");
                            }
                        }
                        Console.WriteLine("Done with the previous request. I am now at:{0} {1}", currentLoc.XLoc, currentLoc.YLoc);
                    }   
                }
                //If the line starts with characters, match only L,M, and R. to turn and move forward
                //Any other letters and or combinations throw an invalid input exception    
            }   
        }
        private static void printRoverStatus(Location loc)
        {
          Console.WriteLine("I am currently at coordinates: {0} {1} facing:{2} direction.",
              loc.XLoc,loc.YLoc,loc.Heading);
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
