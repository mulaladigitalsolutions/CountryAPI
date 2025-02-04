using System.Text.Json;
using Country.Infrastructure.DBContext;
using Country.Infrastructure.Repository.Interface;

namespace Country.Infrastructure.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly CountryDbContext _context; //If you want to use SQL database to store the data
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://restcountries.com/v3.1/all";

        public CountryRepository(CountryDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<List<DBEntities.Country>> GetAllCountriesAsync()
        {
            return ReturnCountriesOpenAPIAsync().Result;
        }

        public async Task<DBEntities.Country> GetCountryByNameAsync(string name)
        {
            var countries = await ReturnCountriesOpenAPIAsync();
            return countries.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        private async Task<List<DBEntities.Country>> ReturnCountriesOpenAPIAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiUrl).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var countries = System.Text.Json.JsonSerializer.Deserialize<List<CountryInfo>>(jsonResponse, options);

                return countries?.Select(countryInfo => new DBEntities.Country
                {
                    Name = countryInfo.Name.Common,
                    Capital = countryInfo.Capital?.FirstOrDefault(),
                    Flag = countryInfo.Flags.TryGetValue("png", out var flagUrl) ? flagUrl : null,
                    Population = countryInfo.Population
                }).ToList() ?? new List<DBEntities.Country>();
            }
            catch (HttpRequestException ex)
            {
                // Handle API call errors (e.g., logging)
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
            }
            catch (System.Text.Json.JsonException ex)
            {
                // Handle JSON deserialization errors
                Console.WriteLine($"JSON Parsing Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }

            return new List<DBEntities.Country>(); // Return empty list on failure
        }
    }
}
