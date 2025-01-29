using Microsoft.EntityFrameworkCore;

namespace Labb3_School.Models;

public partial class Labb3SchoolDbEgzonContext : DbContext
{
    public Labb3SchoolDbEgzonContext()
    {
    }

    public Labb3SchoolDbEgzonContext(DbContextOptions<Labb3SchoolDbEgzonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Profession> Professions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Labb3_SchoolDb_Egzon;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927C03C983249");

            entity.Property(e => e.ClassName).HasMaxLength(100);

            entity.HasOne(d => d.Employee).WithMany(p => p.Classes)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Classes__Employe__3C69FB99");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F11DC23E8D6");

            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);

            entity.HasOne(d => d.Profession).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ProfessionId)
                .HasConstraintName("FK__Employees__Profe__398D8EEE");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__Grades__54F87A57E3CA2A4C");

            entity.Property(e => e.Grade1)
                .HasMaxLength(20)
                .HasColumnName("Grade");

            entity.HasOne(d => d.Employee).WithMany(p => p.Grades)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Grades__Employee__48CFD27E");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Grades__StudentI__46E78A0C");

            entity.HasOne(d => d.Subject).WithMany(p => p.Grades)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__Grades__SubjectI__47DBAE45");
        });

        modelBuilder.Entity<Profession>(entity =>
        {
            entity.HasKey(e => e.ProfessionId).HasName("PK__Professi__3F309E3FB5078C29");

            entity.Property(e => e.ProfessionName).HasMaxLength(100);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52B99CDEBD557");

            entity.HasIndex(e => e.Ssn, "UQ__Students__CA1E8E3CEDAB7C0D").IsUnique();

            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Ssn)
                .HasMaxLength(100)
                .HasColumnName("SSN");

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Students__ClassI__403A8C7D");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__AC1BA3A89D9CFCA7");

            entity.Property(e => e.SubjectName).HasMaxLength(100);

            entity.HasOne(d => d.Class).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Subjects__ClassI__440B1D61");

            entity.HasOne(d => d.Employee).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Subjects__Employ__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
