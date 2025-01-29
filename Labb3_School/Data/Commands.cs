using Labb3_School.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3_School.Data
{
    internal class Commands
    {
        public static void GetAllStudents()
        {
            try
            {
                Console.WriteLine("Välj hur du vill sortera eleverna:");
                Console.WriteLine("1. Sortera på förnamn stigande");
                Console.WriteLine("2. Sortera på förnamn fallande");
                Console.WriteLine("3. Sortera på efternamn stigande");
                Console.WriteLine("4. Sortera på efternamn fallande");

                string choice = Console.ReadLine();

                using (var context = new Labb3SchoolDbEgzonContext())
                {
                    IEnumerable<Student> sortedStudents;

                    switch (choice)
                    {
                        case "1":
                            sortedStudents = context.Students.OrderBy(s => s.FirstName).ToList();
                            break;

                        case "2":
                            sortedStudents = context.Students.OrderByDescending(s => s.FirstName).ToList();
                            break;

                        case "3":
                            sortedStudents = context.Students.OrderBy(s => s.LastName).ToList();
                            foreach (var student in sortedStudents)
                            {
                                Console.WriteLine($"{student.LastName} {student.FirstName}");
                            }
                            return; // Returnera efter utskrift för att undvika dublett-utskrift

                        case "4":
                            sortedStudents = context.Students.OrderByDescending(s => s.LastName).ToList();
                            foreach (var student in sortedStudents)
                            {
                                Console.WriteLine($"{student.LastName} {student.FirstName}");
                            }
                            return; // Returnera efter utskrift för att undvika dublett-utskrift

                        default:
                            Console.WriteLine("Ogiltigt val. Standardval är sortering på förnamn stigande.");
                            sortedStudents = context.Students.OrderBy(s => s.FirstName).ToList();
                            break;
                    }

                    foreach (var student in sortedStudents)
                    {
                        Console.WriteLine($"{student.FirstName} {student.LastName}");
                    }
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Felaktig inmatning. Var god ange ett nummer mellan 1 och 4.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod: {ex.Message}");
            }
        }
        public static void GetStudentsByClass()
        {
            try
            {
                using (var context = new Labb3SchoolDbEgzonContext())
                {
                    // Hämta alla klasser
                    var classes = context.Classes.ToList();

                    Console.WriteLine("Välj en klass:");
                    for (int i = 0; i < classes.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {classes[i].ClassName}");
                    }

                    int classChoice = int.Parse(Console.ReadLine());

                    if (classChoice < 1 || classChoice > classes.Count)
                    {
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        return;
                    }

                    var selectedClass = classes[classChoice - 1];

                    // Hämta elever i den valda klassen
                    var studentsInClass = context.Students
                        .Where(s => s.ClassId == selectedClass.ClassId)
                        .ToList();

                    Console.WriteLine($"Elever i {selectedClass.ClassName}:");
                    foreach (var student in studentsInClass)
                    {
                        Console.WriteLine($"{student.FirstName} {student.LastName} - {student.Class.ClassName}");
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Felaktig inmatning. Var god ange ett giltigt nummer.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod: {ex.Message}");
            }
        }
        public static void AddEmployee()
        {
            try
            {
                using (var context = new Labb3SchoolDbEgzonContext())
                {
                    // Hämta och visa alla professioner
                    var professions = context.Professions.ToList();
                    Console.WriteLine("Tillgängliga yrken:");
                    foreach (var p in professions)
                    {
                        Console.WriteLine($"{p.ProfessionId}: {p.ProfessionName}");
                    }

                    Console.WriteLine("Ange Id för yrket för den nya anställda:");
                    if (!int.TryParse(Console.ReadLine(), out int professionId) || !professions.Any(p => p.ProfessionId == professionId))
                    {
                        Console.WriteLine("Ogiltigt yrkes-Id. Försök igen.");
                        return;
                    }

                    Console.WriteLine("Ange förnamn för den nya anställda:");
                    string firstName = Console.ReadLine();

                    Console.WriteLine("Ange efternamn för den nya anställda:");
                    string lastName = Console.ReadLine();

                    // Kontrollera om den valda professionen existerar
                    var profession = professions.FirstOrDefault(p => p.ProfessionId == professionId);
                    if (profession == null)
                    {
                        Console.WriteLine("Yrkes-Id kunde inte hittas. Försök igen.");
                        return;
                    }

                    // Skapa en ny Employee och spara till databasen
                    var newEmployee = new Employee
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        ProfessionId = professionId
                    };

                    context.Employees.Add(newEmployee);
                    context.SaveChanges();

                    Console.WriteLine("Den nya anställda har lagts till i databasen.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod vid sparandet av den nya anställda: {ex.Message}");
            }
        }
        public static void GetAllGrades()
        {
            using (var context = new Labb3SchoolDbEgzonContext())
            {
                // Hämta alla betyg med relaterad student och ämne
                var allGrades = context.Grades
                    .Include(g => g.Student)   // Hämtar den relaterade studenten
                    .Include(g => g.Subject)   // Hämtar det relaterade ämnet
                    .ToList();                 // Hämta till en lista

                // Skriv ut betygen
                foreach (var grade in allGrades)
                {
                    string studentName = grade.Student.FirstName + " " + grade.Student.LastName;
                    string subjectName = grade.Subject.SubjectName;
                    string gradeValue = grade.Grade1;
                    DateOnly dateAssigned = grade.DateAssigned;

                    // Skriv ut informationen för varje betyg
                    Console.WriteLine($"{studentName} - {subjectName}: {gradeValue} (Assigned on {dateAssigned.ToShortDateString()})");
                }
            }
        }
    }
}