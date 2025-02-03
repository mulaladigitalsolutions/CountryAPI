using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Country.Infrastructure.DBContext
{
    public class CountryDbContextFactory : IDesignTimeDbContextFactory<CountryDbContext>
    {
        public CountryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CountryDbContext>();

            // Here you can configure the connection string or any other options needed for migration
            optionsBuilder.UseSqlServer("Server=LIVHUWANIR;Database=CountryDb;Integrated Security=True;TrustServerCertificate=True;");

            return new CountryDbContext(optionsBuilder.Options);
        }
    }
}