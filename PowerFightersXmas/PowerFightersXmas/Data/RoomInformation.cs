using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Data
{
    public static class RoomInformation
    {
        public static Room InitializeRooms()
        {
             // Create the rooms
            var entrance = new Room("Entrance", "Nice house with a lot of rooms... Oh look, Santa!");
            var coridor = new Room("Corridor", "There is two doors here, bathroom and another door.");
            var outside = new Room("Outside", "It's cold outside, I need to get back in.");
            var wc = new Room("WC", "This is the WC... Eww so dirty.");
            var kitchen = new Room("Kitchen", "This is the kitchen, it's a mess.");
            var livingRoom = new Room("Living Room", "A cozy living room with stairs to the second floor.");
            var pool = new Room("OutDoor Pool", "This is a pool, it's frozen solid... \n is that an Elf stuck in the ice?");
            var office = new Room("Office", "Looks like an ordinary office...");
            var secondFloor = new Room("Second Floor Entrance", "Wow, this place is big!");
            var bar = new Room("Looks like a bar... ", "This is a room with a pool table and a fireplace... " +
                "But I need to start the fire.");
            var balcony = new Room("Balcony", "❄️❄️This place is so cold❄️❄️... Is that a frozen Elf?");
            var bedroom = new Room("Bedroom", "This is a bedroom, it's a mess, guess someone wasn't a good kid this Xmas");
            var basement = new Room("Basement", "It's pitch black here... I need to find a lamp!");

            // Connect the rooms
            // from entrance
            entrance.Exits.Add("north", kitchen);
            entrance.Exits.Add("east", coridor);
            entrance.Exits.Add("west", livingRoom);
            entrance.Exits.Add("south", outside);
            //from coridor
            coridor.Exits.Add("west", entrance);
            coridor.Exits.Add("north", office);
            coridor.Exits.Add("south", wc);
            //from outside
            outside.Exits.Add("north", entrance);
            //from kitchen
            kitchen.Exits.Add("south", entrance);
            kitchen.Exits.Add("west", livingRoom);
            //from livingroom 
            livingRoom.Exits.Add("up", secondFloor);
            livingRoom.Exits.Add("east", entrance);
            livingRoom.Exits.Add("north", pool);
            //from second floor
            secondFloor.Exits.Add("down", livingRoom);
            //from pool 
            pool.Exits.Add("south", livingRoom);
            //from office
            office.Exits.Add("south", coridor);
            office.Exits.Add("down", basement);
            //from wc
            wc.Exits.Add("west", coridor);

            // Return the entrance room
            return entrance;
        }

        /*basement.Items.Add(new Item("Tomteverkstadens nyckel",
         "En nyckel som ser ut att passa i en dörr."));*/
    }
}
