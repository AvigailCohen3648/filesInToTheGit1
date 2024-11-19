using ex1.Models;
using ex2.Models;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace ex2.Repositories
{
    public class TasksdbContext: DbContext
    {
        public TasksdbContext(DbContextOptions<TasksdbContext> options) : base(options)
        {
            
        }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
