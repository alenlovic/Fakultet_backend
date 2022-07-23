using Microsoft.EntityFrameworkCore;
using ProjekatFakultet.Models;

namespace ProjekatFakultet.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        
        }
        public DbSet<StudentiModel> Studenti { get; set; }
        public DbSet<ProfesoriModel> Profesori { get; set; }
        public DbSet<UpravaModel> Uprava { get; set; }
    }
}
