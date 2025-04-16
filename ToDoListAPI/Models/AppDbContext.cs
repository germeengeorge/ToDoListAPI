using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Models;
namespace ToDoListAPI.Models
{
    public class AppDbContext:DbContext
    {
        public DbSet<todolist> Todolists { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
