using Country.Infrastructure.DBContext;
using Country.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Country.Infrastructure.Repository
{
    public class CountryRepository : ICountryRepository
    {

        private readonly CountryDbContext _context;

        public CountryRepository(CountryDbContext context)
        {
            _context = context;
        }

        public async Task<List<DBEntities.Country>> GetAllCountriesAsync()
        {
            try
            {
                return await _context.Countries.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DBEntities.Country> GetCountryByNameAsync(string name)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
