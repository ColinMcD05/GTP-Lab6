using System;
using System.Collections.Generic;

namespace Zork
{
    internal class Program
    {
        private static readonly Room[,] rooms = {
            { new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View") },
            {  new Room("Forest"),  new Room("West of House"),  new Room("Behind House")},
            {  new Room("Dense Woods"),  new Room("North of House"),  new Room("Clearing") } };

        private static (int row, int column) location = (1, 1);

        private static Room CurrentRoom
        {
            get
            {
                return rooms[location.row, location.column];
            }
        }

        static void Main(string[] args)
        {
            //Initialization
            InitializeRoomDesctiptions();
            Console.WriteLine("Welcome to Zork!");

            Room previousRoom = null;
            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                //Print position
                PrintRoom();
                if (previousRoom != CurrentRoom)
                {
                    Console.WriteLine(CurrentRoom.description);
                    previousRoom = CurrentRoom;
                }

                //Player input
                Console.Write("> ");
                //Read Input
                command = ToCommand(Console.ReadLine().Trim());

                //Behvaiour based on input
                string outputString;
                switch(command)
                {
                    case Commands.LOOK:
                        Console.WriteLine(CurrentRoom.description);
                        break;
                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:                  
                        if(!Move(command))
                        {
                            Console.WriteLine("The way is shut!");
                        }
                        break;
                    case Commands.QUIT:
                        Console.WriteLine("Thank you for playing!");
                        break;
                    case Commands.UNKNOWN:
                    default:
                        Console.WriteLine("Unkown command.");
                        break;
                }
            }
        }

        private static Commands ToCommand(string commandString) =>
            Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKNOWN;

        private static bool IsDirection(Commands command) => directions.Contains(command);

        private static bool Move(Commands command)
        {
            Assert.IsTrue(IsDirection(command), "Invalid direction.");

            bool isValidMove = true;
            switch(command)
            {
                case Commands.EAST when location.column < rooms.GetLength(1) - 1:
                    location.column++;         
                    break;
                case Commands.WEST when location.column > 0:
                    location.column--;
                    break;
                case Commands.NORTH when location.row < rooms.GetLength(0) - 1:
                    location.row++;
                    break;
                case Commands.SOUTH when location.row > 0:
                    location.row--;
                    break;
                default:
                    isValidMove = false;
                    break;
            }

            return isValidMove;
        }

        private static readonly List<Commands> directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };

        private static void PrintRoom()
        {
            Console.WriteLine(CurrentRoom);
        }

        private static void InitializeRoomDesctiptions()
        {
            var roomMap = new Dictionary<string, Room>();
            foreach (Room room in rooms)
            {
                roomMap[room.name] = room;
            }

            roomMap["Rocky Trail"].description = "You are on a rock-strewn trail.";
            roomMap["South of House"].description = "You are facing the south side of a white house. There is no door here, and all the windows are barred.";
            roomMap["Canyon View"].description = "You are at the top of the Great Canyon on its south wall.";
            roomMap["Forest"].description = "This is a forest, with trees in all directions around you.";
            roomMap["West of House"].description = "This is an open field west of a white house, with a boarded front door.";
            roomMap["Behind House"].description = "You are behind the white house. In one corner of the house there is a small window which is slightly ajar.";
            roomMap["Dense Woods"].description = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight.";
            roomMap["North of House"].description = "You are facing the north side of a white house. There is no door here, and all the windows are barred.";
            roomMap["Clearing"].description = "You are in a clearing, with a forest surrounding you on the west and south.";
        }
    }
}
