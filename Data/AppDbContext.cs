using Microsoft.EntityFrameworkCore; //Connects the app to a database
using TaskMangementDemo.Models;

namespace TaskMangementDemo.Data
{
   public class AppDbContext : DbContext
   {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            //constructor accepts configuration options
        }
        public DbSet<TaskItem> Tasks { get; set; }
   }
}
