namespace Labb3_School.Models;

public partial class Class
{
    public int ClassId { get; set; }
    public string ClassName { get; set; } = null!;
    public int? EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
