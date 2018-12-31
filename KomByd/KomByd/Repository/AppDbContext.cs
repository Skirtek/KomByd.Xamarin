using KomByd.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace KomByd.Repository
{
    public class AppDbContext : DbContext
    {
        private string _databasePath;

        public DbSet<StopRepo> StopList { get; set; }

        public AppDbContext(string databasePath)
        {
            _databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}