using Microsoft.EntityFrameworkCore;
using ToDoWeb.Models;

namespace ToDoWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Label> Labels { get; set; }

        internal List<Label> ToList()
        {
            throw new NotImplementedException();
        }
    }

}
