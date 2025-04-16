using Country.Services.Services.Interfaces;
using CountryAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CountryAPI.ControllerTests
{
    public class CountryControllerTests
    {
        private readonly Mock<ICountryService> _mockService;
        private readonly CountryController _controller;

        public CountryControllerTests()
        {
            _mockService = new Mock<ICountryService>();
            //_controller = new CountryController(_mockService.Object);
        }

        [Fact]
        public async Task GetCountries_ReturnsOkResultWithCountries()
        {
            var countries = new List<Country.Domain.Entities.Country>
            {
                new() { Name = "USA", Flag = "🇺🇸" },
                new() { Name = "Canada", Flag = "🇨🇦" }
            };
            _mockService.Setup(service => service.GetCountriesAsync()).ReturnsAsync(countries);

            var result = await _controller.GetCountries();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCountries = Assert.IsType<List<Country.Domain.Entities.Country>>(okResult.Value);
            Assert.Equal(2, returnedCountries.Count);
        }
    }
}
