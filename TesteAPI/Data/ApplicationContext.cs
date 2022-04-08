using Microsoft.EntityFrameworkCore;
using TesteAPI.Entities;

namespace TesteAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<AgendaTarefas> AgendaTarefas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=TesteAPI;Data Source=LAPTOP-TFG9GU34");
        }
    }
}
