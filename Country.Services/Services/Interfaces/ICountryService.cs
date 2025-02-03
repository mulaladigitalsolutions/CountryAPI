using Country.Domain.Entities;

namespace Country.Services.Services.Interfaces
{
    public interface ICountryService
    {
        Task<List<Domain.Entities.Country>> GetCountriesAsync();
        Task<Domain.Entities.Country> GetCountryDetailsAsync(string name);
    }
}
