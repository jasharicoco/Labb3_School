namespace Labb3_School.Models;

public partial class Subject
{
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = null!;
    public int? EmployeeId { get; set; }
    public int? ClassId { get; set; }
    public virtual Class? Class { get; set; }
    public virtual Employee? Employee { get; set; }
    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
