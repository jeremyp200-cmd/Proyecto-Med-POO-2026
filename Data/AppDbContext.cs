using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Models;

namespace ProyectoAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Medicamento> Medicamentos { get; set; }
    }
}