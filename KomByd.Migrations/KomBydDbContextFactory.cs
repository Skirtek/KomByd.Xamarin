using Microsoft.EntityFrameworkCore.Design;

namespace KomByd.Migrations
{
    public class KomBydDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            return new AppDbContext("KomByd.db");
        }
    }
}
