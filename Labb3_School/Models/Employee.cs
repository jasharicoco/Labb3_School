namespace Labb3_School.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int? ProfessionId { get; set; }
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
    public virtual Profession? Profession { get; set; }
    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
