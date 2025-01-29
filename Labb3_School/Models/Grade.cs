namespace Labb3_School.Models;

public partial class Grade
{
    public int GradeId { get; set; }
    public DateOnly DateAssigned { get; set; }
    public string Grade1 { get; set; } = null!;
    public int? StudentId { get; set; }
    public int? SubjectId { get; set; }
    public int? EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }
    public virtual Student? Student { get; set; }
    public virtual Subject? Subject { get; set; }
}
