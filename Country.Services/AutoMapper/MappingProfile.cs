using AutoMapper;

namespace Country.Services.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Infrastructure.DBEntities.Country, Domain.Entities.Country>();             
        }
    }
}
