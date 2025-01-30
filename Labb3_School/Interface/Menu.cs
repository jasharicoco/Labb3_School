using Labb3_School.Models;  // För tabellerna
using Labb3_School.Data;    // För klassen Commands

namespace Labb3_School.Interface
{
    internal class Menu
    {
        public static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Hello and welcome to the School.");
                Console.WriteLine("What would you like to do?\n" +
                                  "1. List students\n" +
                                  "2. List students in a specific class\n" +
                                  "3. Add staff\n" +
                                  "4. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Commands.GetAllStudents();
                        Console.ReadKey();
                        break;

                    case "2":
                        Commands.GetStudentsByClass();
                        Console.ReadKey();
                        break;

                    case "3":
                        Commands.AddEmployee();
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.WriteLine("Exiting the program...");
                        Console.ReadKey();
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ReadKey();
                        break;
                }

                Console.WriteLine(); // Mellanslag mellan valen
            }
        }
    }
}
