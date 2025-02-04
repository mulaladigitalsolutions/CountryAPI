using AutoMapper;
using Country.Infrastructure.Repository.Interface;
using Country.Services.Services;
using Moq;

namespace CountryAPI.IntegrationTest
{
    public class CountryServiceTests
    {
        private readonly IMapper _mapper;

        public CountryServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Country.Infrastructure.DBEntities.Country, Country.Domain.Entities.Country>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetCountries_ReturnsCountries()
        {
            var mockRepo = new Mock<ICountryRepository>();
            mockRepo.Setup(repo => repo.GetAllCountriesAsync()).ReturnsAsync(
            [
                new Country.Infrastructure.DBEntities.Country { Name = "USA", Flag = "🇺🇸" },
                new Country.Infrastructure.DBEntities.Country { Name = "Canada", Flag = "🇨🇦" }
            ]);

            var service = new CountryService(mockRepo.Object, _mapper);
            var result = await service.GetCountriesAsync();

            Assert.Equal(2, result.Count);
            Assert.IsType<List<Country.Domain.Entities.Country>>(result);
        }

        [Fact]
        public async Task GetCountries_ReturnsNoCountries()
        {
            var mockRepo = new Mock<ICountryRepository>();
            mockRepo.Setup(repo => repo.GetAllCountriesAsync()).ReturnsAsync([]);

            var service = new CountryService(mockRepo.Object, _mapper);
            var result = await service.GetCountriesAsync();

            Assert.Empty(result);
            Assert.IsType<List<Country.Domain.Entities.Country>>(result);
        }

        [Fact]
        public async Task GetCountryDetails_ReturnsACountry()
        {
            var mockRepo = new Mock<ICountryRepository>();
            mockRepo.Setup(repo => repo.GetCountryByNameAsync("South Africa")).ReturnsAsync(
            new Country.Infrastructure.DBEntities.Country { Name = "South Africa", Flag = "za" }
            );

            var service = new CountryService(mockRepo.Object, _mapper);
            var result = await service.GetCountryDetailsAsync("South Africa");

            Assert.Equal("South Africa", result.Name);
            Assert.IsNotType<List<Country.Domain.Entities.Country>>(result);
        }

        [Fact]
        public async Task GetCountryDetails_ReturnsNoCountry()
        {
            var mockRepo = new Mock<ICountryRepository>();
            mockRepo.Setup(repo => repo.GetCountryByNameAsync("Zimbabwe")).ReturnsAsync(new Country.Infrastructure.DBEntities.Country());

            var service = new CountryService(mockRepo.Object, _mapper);
            var result = await service.GetCountryDetailsAsync("Zimbabwe");

            Assert.NotNull(result);
            Assert.Null(result.Name);
            Assert.Null(result.Capital);
            Assert.Null(result.Flag);
            Assert.Equal(default, result.Population);
            Assert.IsType<Country.Domain.Entities.Country>(result);
        }
    }
}