using Microsoft.EntityFrameworkCore;
using Project.Models.Domain;

namespace Project.Data
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Student> Students { get; set; }
    }
}
