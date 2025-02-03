
using Country.Infrastructure.Repository.Interface;
using Country.Services.Services.Interfaces;
using AutoMapper;

namespace Country.Services.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryService(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<List<Domain.Entities.Country>> GetCountriesAsync()
        {

            return _mapper.Map<List<Domain.Entities.Country>>(await _countryRepository.GetAllCountriesAsync()); 
        }

        public async Task<Domain.Entities.Country> GetCountryDetailsAsync(string name)
        {
            return _mapper.Map<Domain.Entities.Country>(await _countryRepository.GetCountryByNameAsync(name));
        }

        
    }
}
