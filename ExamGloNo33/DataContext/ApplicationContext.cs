using ExamGloNo33.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamGloNo33.DataContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=your_server;Database=your_database;Trusted_Connection=True;");
        }
    }
}
