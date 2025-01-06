﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Data
{
    public static class RoomInformation
    {
        public static Room InitializeRooms(string playerName)
        {
            // Create the rooms
            var entrance = new Room("Entrance", "Nice house with a lot of rooms... Oh look, Santa!");
            var outside = new Room("Outside", "It's cold outside. There is a pool, it's frozen solid... \\n is that an Elf stuck in the ice?!");
            var kitchen = new Room("Kitchen", "This is the kitchen, it's a mess.");
            var livingRoom = new Room("Living Room", "A cozy living room with stairs to the second floor.");
            var pool = new Room("Outdoor Pool", "This is a pool, it's frozen solid... \n is that an Elf stuck in the ice?");
            var office = new Room("Office", "Looks like an ordinary office...");
            var secondFloor = new Room("Second Floor Entrance", "Wow, this place is big!");
            var bar = new Room("Looks like a bar... ", "This is a room with a pool table and a fireplace... " +
                "But I need to start the fire.");
            var balcony = new Room("Balcony", "❄️❄️This place is so cold❄️❄️... Is that a frozen Elf?");
            var bedroom = new Room("Bedroom", "This is a bedroom, it's a mess, guess someone wasn't a good kid this Xmas");
            var basement = new Room("Basement", "It's pitch black here... I need to find a lamp!");

            // Dynamically fetch items for each room
            foreach (var item in DatabaseManager.GetRoomItems("Entrance", playerName))
                entrance.Items.Add(item);

            foreach (var item in DatabaseManager.GetRoomItems("Kitchen", playerName))
                kitchen.Items.Add(item);

            foreach (var item in DatabaseManager.GetRoomItems("Office", playerName))
                office.Items.Add(item);

            foreach (var item in DatabaseManager.GetRoomItems("Basement", playerName))
                basement.Items.Add(item);

            // Connect the rooms
            entrance.Exits.Add("north", kitchen);
            entrance.Exits.Add("east", office);
            entrance.Exits.Add("south", outside);
            entrance.Exits.Add("west", livingRoom);
            outside.Exits.Add("north", entrance);
            kitchen.Exits.Add("south", entrance);
            livingRoom.Exits.Add("up", secondFloor);
            livingRoom.Exits.Add("east", entrance);
            secondFloor.Exits.Add("down", livingRoom);
            office.Exits.Add("west", entrance);
            office.Exits.Add("down", basement);
            basement.Exits.Add("up", office);

            // Return the entrance room
            return entrance;
        }

        /*basement.Items.Add(new Item("Tomteverkstadens nyckel",
         "En nyckel som ser ut att passa i en dörr."));*/

        // Method to find a room by name
        public static Room FindRoom(string roomName, string playerName)
        {
            // Run InitializeRooms and iterate through all rooms to find the room with the specified name
            var entrance = InitializeRooms(playerName);

            // Recursive method to find the room
            var visitedRooms = new HashSet<Room>();
            return FindRoomRecursive(entrance, roomName, visitedRooms);
        }

        private static Room FindRoomRecursive(Room currentRoom, string roomName, HashSet<Room> visitedRooms)
        {
            if (currentRoom.Name.Equals(roomName, StringComparison.OrdinalIgnoreCase))
                return currentRoom;

            visitedRooms.Add(currentRoom);

            foreach (var exit in currentRoom.Exits.Values)
            {
                if (!visitedRooms.Contains(exit))
                {
                    var foundRoom = FindRoomRecursive(exit, roomName, visitedRooms);
                    if (foundRoom != null) return foundRoom;
                }
            }

            return null;
        }
    }
}
