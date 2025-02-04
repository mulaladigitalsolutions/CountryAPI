using Microsoft.EntityFrameworkCore;

namespace Country.Infrastructure.DBContext
{
    public class CountryDbContext : DbContext
    {
        public CountryDbContext(DbContextOptions<CountryDbContext> options) : base(options) { }

        // Define DbSets for your models (tables)
        public virtual DbSet<DBEntities.Country> Countries { get; set; }

        // Seeding initial data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add seed data for the Countries table
            modelBuilder.Entity<DBEntities.Country>().HasData(
                new DBEntities.Country { Name = "USA", Flag = "🇺🇸", Population = 331002651, Capital = "Washington, D.C." },
                new DBEntities.Country { Name = "Canada", Flag = "🇨🇦", Population = 37742154, Capital = "Ottawa" },
                new DBEntities.Country { Name = "South Africa", Flag = "🇿🇦", Population = 59308690, Capital = "Pretoria" },
                new DBEntities.Country { Name = "Namibia", Flag = "🇳🇦", Population = 2540905, Capital = "Windhoek" },
                new DBEntities.Country { Name = "Rwanda", Flag = "🇷🇼", Population = 12952218, Capital = "Kigali" },
                new DBEntities.Country { Name = "Germany", Flag = "🇩🇪", Population = 83166711, Capital = "Berlin" },
                new DBEntities.Country { Name = "France", Flag = "🇫🇷", Population = 65273511, Capital = "Paris" },
                new DBEntities.Country { Name = "Japan", Flag = "🇯🇵", Population = 126476461, Capital = "Tokyo" },
                new DBEntities.Country { Name = "Brazil", Flag = "🇧🇷", Population = 212559417, Capital = "Brasília" },
                new DBEntities.Country { Name = "Australia", Flag = "🇦🇺", Population = 25687041, Capital = "Canberra" }
            );
        }
    }
}
