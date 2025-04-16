using System.Text.Json;
using Country.Infrastructure.Configuration;
using Country.Infrastructure.DBContext;
using Country.Infrastructure.Repository.Interface;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Country.Infrastructure.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly CountryDbContext _context; //If you want to use SQL database to store the data
        private readonly HttpClient _httpClient;
        private readonly ILogger<CountryRepository> _logger;
        private readonly string _apiUrl;
        private readonly IMemoryCache _cache;

        public CountryRepository(CountryDbContext context, 
            HttpClient httpClient, 
            ILogger<CountryRepository> logger, 
            IOptions<CountryApiSettings> settings,
            IMemoryCache cache)
        {
            _context = context;
            _httpClient = httpClient;
            _logger = logger;
            _apiUrl = settings.Value.RestCountriesUrl;
            _cache = cache;
        }

        public async Task<List<DBEntities.Country>> GetAllCountriesAsync()
        {
            return await ReturnCountriesOpenAPIAsync();
        }

        public async Task<DBEntities.Country> GetCountryByNameAsync(string name)
        {
            var countries = await ReturnCountriesOpenAPIAsync();
            return countries.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        private async Task<List<DBEntities.Country>> ReturnCountriesOpenAPIAsync()
        {
            const string cacheKey = "countries-cache";

            if (_cache.TryGetValue(cacheKey, out List<DBEntities.Country> cachedCountries))
            {
                _logger.LogInformation("Returning countries from cache.");
                return cachedCountries;
            }

            try
            {
                var response = await _httpClient.GetAsync(_apiUrl).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var countries = JsonSerializer.Deserialize<List<CountryInfo>>(jsonResponse, options);

                var result = countries?.Select(countryInfo => new DBEntities.Country
                {
                    Name = countryInfo.Name.Common,
                    Capital = countryInfo.Capital?.FirstOrDefault(),
                    Flag = countryInfo.Flags.TryGetValue("png", out var flagUrl) ? flagUrl : null,
                    Population = countryInfo.Population
                }).ToList() ?? new List<DBEntities.Country>();

                // Cache for 30 minutes (adjust as needed)
                _cache.Set(cacheKey, result, TimeSpan.FromMinutes(30));

                _logger.LogInformation("Fetched and cached countries from API.");

                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"HTTP Request Error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, $"JSON Parsing Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected Error: {ex.Message}");
            }

            return [];
        }

    }
}
