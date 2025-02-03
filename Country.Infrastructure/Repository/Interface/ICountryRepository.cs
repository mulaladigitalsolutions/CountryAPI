namespace Country.Infrastructure.Repository.Interface
{
    public interface ICountryRepository
    {
        Task<List<DBEntities.Country>> GetAllCountriesAsync();
        Task<DBEntities.Country> GetCountryByNameAsync(string name);
    }
}
