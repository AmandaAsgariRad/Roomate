// See https://aka.ms/new-console-template for more information
using Roommates.Repositories;
using Roommates.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true;TrustServerCertificate=true;";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);

            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);

            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);
            
            bool runProgram = true;
            
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Name} has in Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                        }
                        Console.WriteLine("Press any key to continue");
                        Console.ReadLine();
                        break;

                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Update a room"):
                        List<Room> roomOptions = roomRepo.GetAll();
                        foreach (Room r in roomOptions)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }

                        Console.Write("Which room would you like to update? ");
                        int selectedRoomId = int.Parse(Console.ReadLine());
                        Room selectedRoom = roomOptions.FirstOrDefault(r => r.Id == selectedRoomId);

                        Console.Write("New Name: ");
                        selectedRoom.Name = Console.ReadLine();

                        Console.Write("New Max Occupancy: ");
                        selectedRoom.MaxOccupancy = int.Parse(Console.ReadLine());

                        roomRepo.Update(selectedRoom);

                        Console.WriteLine("Room has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Delete a room"):
                        List<Room> allRooms = roomRepo.GetAll();
                        foreach (Room r in allRooms)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name}");
                        }
                        Console.WriteLine("Select a Room Id: ");
                        int deleteRoomId = int.Parse(Console.ReadLine());

                        if (deleteRoomId != null)
                        {
                            roomRepo.Delete(deleteRoomId);
                            Console.WriteLine($"Room with Id {deleteRoomId} has been deleted.");
                        }
                        else
                        {
                            Console.WriteLine($"Room with Id {deleteRoomId} does not exist.");
                        }

                        Console.WriteLine("Press any key to continue");
                        Console.ReadLine();
                        break;

                    case ("Show all chores"):
                        List<Chore> chores = choreRepo.GetAll();
                        foreach (Chore c in chores)
                        {
                            Console.WriteLine($"{c.Name}");
                        }
                        Console.WriteLine("Press any key to continue");
                        Console.ReadLine();
                        break;

                    case ("Show all unassigned chores"):
                        List<Chore> unassignedChores = choreRepo.GetUnassignedChores();
                        foreach (Chore c in unassignedChores)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }
                        Console.WriteLine("Press any key to continue");
                        Console.ReadLine();
                        break;

                    case ("Search for a chore"):
                        Console.Write("Chore Id: ");
                        int choreId = int.Parse(Console.ReadLine());

                        Chore chore = choreRepo.GetById(choreId);

                        Console.WriteLine($"{chore.Id} - {chore.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                        
                    case ("Add a chore"):
                        Console.WriteLine("Chore name: ");
                        string choreName = Console.ReadLine();

                        Chore choreToAdd = new Chore()
                        {
                            Name = choreName
                        };

                        choreRepo.Insert(choreToAdd);

                        Console.WriteLine($"{choreToAdd.Name} has been added");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Update a chore"):
                        List<Chore> choreOptions = choreRepo.GetAll();
                        foreach (Chore c in choreOptions)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }

                        Console.Write("Which chore would you like to update? ");
                        int selectedChoreId = int.Parse(Console.ReadLine());
                        Chore selectedChore = choreOptions.FirstOrDefault(c => c.Id == selectedChoreId);

                        Console.Write("New chore: ");
                        selectedChore.Name = Console.ReadLine();


                        choreRepo.Update(selectedChore);

                        Console.WriteLine("The chore has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Delete a chore"):
                        List<Chore> choreDeleteOptions = choreRepo.GetAll();
                        foreach (Chore c in choreDeleteOptions)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }
                        
                        Console.WriteLine("Select a Room Id: ");
                        int deleteChoreId = int.Parse(Console.ReadLine());


                        if (deleteChoreId != null)
                        {
                            choreRepo.Delete(deleteChoreId);
                            Console.WriteLine($"Chore with Id {deleteChoreId} has been deleted.");
                        }
                        else
                        {
                            Console.WriteLine($"Room with Id {deleteChoreId} does not exist.");
                        }

                        Console.WriteLine("Press any key to continue");
                        Console.ReadLine();
                        break;

                    case ("Search for a roommate"):

                        Console.Write("Roommate Id: ");
                        int roommateId = int.Parse(Console.ReadLine());

                        Roommate roommate = roommateRepo.GetById(roommateId);

                        Console.WriteLine($"{roommate.FirstName} pays ${roommate.RentPortion} in the {roommate.room.Name}.");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Exit"):
                        runProgram = false;
                        break;


                }
            }


        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Add a room",
                "Update a room",
                "Delete a room",
                "Show all chores",
                "Show all unassigned chores",
                "Search for a chore",
                "Add a chore",
                "Update a chore",
                "Delete a chore",
                "Search for a roommate",
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}
