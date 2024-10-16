using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{
    public DbSet<Student> Students_tbl { get; set; }
    public DbSet<Class> Classes_tbl { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=.\\SQL2019;database=OcelotSchool;trusted_connection=true;MultipleActiveResultSets=True;TrustServerCertificate=True");
    }
}