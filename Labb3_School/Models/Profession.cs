namespace Labb3_School.Models;

public partial class Profession
{
    public int ProfessionId { get; set; }
    public string ProfessionName { get; set; } = null!;
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
