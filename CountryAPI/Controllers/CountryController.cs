using Country.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace CountryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly ICountryService _countryService;

        public CountryController(ILogger<CountryController> logger, ICountryService countryService) { _logger = logger; _countryService = countryService; }

        /// <summary>
        /// Returns list of all countries and their information.
        /// </summary>
        [HttpGet("GetCountries")]
        public async Task<ActionResult<List<Country.Domain.Entities.Country>>> GetCountries()
        {
            try
            {
                var countries = await _countryService.GetCountriesAsync();
                return Ok(countries);
            }
            catch (Exception)
            {
                //Log the error somewhere either to a file, elastic or some database
            }

            return BadRequest("Could not return Countries at this time");

        }

        /// <summary>
        /// Returns Country details by country name.
        /// </summary>
        /// <param name="name"></param>
        [HttpGet("GetCountryDetails/{name}")]
        public async Task<ActionResult<Country.Domain.Entities.Country>> GetCountryDetails(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                try
                {
                    var countryDetails = await _countryService.GetCountryDetailsAsync(name);
                    if (countryDetails == null)
                    {
                        return NotFound(new { message = "Country not found" });
                    }
                    return Ok(countryDetails);

                }
                catch (Exception ex)
                {
                    //Log the error somewhere either to a file, elastic or some database
                    _logger.LogError(ex.Message);
                }
            }

            return BadRequest("CountryDetails requires a valid country name");

        }
    }
}
