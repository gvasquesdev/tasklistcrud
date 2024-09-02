using Microsoft.EntityFrameworkCore;
using TaskList.Models;

namespace TaskList.Data
{
    public class AppDbContext : DbContext 
    {
        public DbSet<TaskModel> Tarefas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=app.db;Cache=Shared");
    }
 }