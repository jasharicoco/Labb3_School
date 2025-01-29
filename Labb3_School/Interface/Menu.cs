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
                Console.WriteLine("Hej och välkommen till Skolan.");
                Console.WriteLine("Vad vill du göra?\n" +
                                  "1. Lista elever\n" +
                                  "2. Lista elever i en viss klass\n" +
                                  "3. Lägg till personal\n" +
                                  "4. Avsluta");

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
                        Console.WriteLine("Avslutar programmet...");
                        Console.ReadKey();
                        return;

                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        Console.ReadKey();
                        break;
                }

                Console.WriteLine(); // Mellanslag mellan valen
            }
        }
    }
}
