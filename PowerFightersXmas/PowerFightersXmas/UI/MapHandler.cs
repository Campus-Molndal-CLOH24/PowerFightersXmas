﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
internal class MapHandler
{
    internal void DisplayMap(string currentRoom)
    {
        if (currentRoom == "Basement")
        {
            Console.WriteLine(
           "+------------------------------------------+" +
           "\n|                                          |" +
           "\n|                                          |" +
           "\n|                                          |" +
           "\n|                  Basement                |          N" +
           "\n|                    🎅                    |          ↑" +
           "\n|                                          |     W <-   -> E" +
           "\n|                                          |          ↓" +
           "\n|                                          |          S" +
           "\n|                                          |" +
           "\n+----------------+----  ----+--------------+"
       );

        }
        else
        {
            // Display Main Map (for Office, Living Room, etc.)
            string[] asciiMap = {
            "+------------------------------------------+",
            "| Living Room    | Kitchen  | Office       |",
            "|                |          |              |",
            "|                |    {1}   |              |", 
            "|                |          |              |          N",
            "|      {0}       +----  ----+      {2}     |          ↑",
            "|                | Entrance |              |     W <-   -> E",
            "|                                          |          ↓",
            "|                |    {4}   |              |          S",
            "|                |          |              |",
            "+----------------+----  ----+--------------+",
            "",
            "+----------------+",
            "| Pool           |",
            "|                |    {5}",
            "|                |",
            "+----------------+"
        };

            // Mark the current room with Santa or leave it blank (fixed width)
            string livingRoom = currentRoom == "Living Room" ? "🎅 " : "   ";
            string kitchen = currentRoom == "Kitchen" ? "🎅 " : "   ";
            string office = currentRoom == "Office" ? "🎅 " : "   ";
            string corridor = currentRoom == "Corridor" ? "🎅 " : "   ";
            string entrance = currentRoom == "Entrance" ? "🎅 " : "   ";
            string outside = currentRoom == "Outside" ? "🎅 " : "   ";

            foreach (string line in asciiMap)
            {
                Console.WriteLine(string.Format(line, livingRoom, kitchen, office, corridor, entrance, outside));
            }
        }
    }
}
