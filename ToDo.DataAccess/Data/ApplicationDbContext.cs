using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Label> Labels { get; set; }
        public DbSet<ToDoTask> ToDoTasks { get; set; }
    }

}
