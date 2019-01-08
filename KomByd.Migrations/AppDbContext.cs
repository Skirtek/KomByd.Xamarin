using KomByd.Migrations.Models;
using Microsoft.EntityFrameworkCore;

namespace KomByd.Migrations
{
    public class AppDbContext : DbContext
    {
        private string _databasePath;

        public DbSet<StopRepo> StopList { get; set; }
        public DbSet<Line> LinesList { get; set; }

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