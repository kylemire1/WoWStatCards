using AutoMapper;
using WowStatCards.Models.Domain;
using WowStatCards.Models.DTO;

namespace WoWStatCards.API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<StatCardDto, StatCard>().ReverseMap();
        }
    }
}
