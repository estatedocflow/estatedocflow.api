using AutoMapper;
using estatedocflow.Models.Dtos.House;
using estatedocflow.Models.Entities.House;

namespace estatedocflow.api.AutoMapper
{
    public class HouseProfile : Profile
    {
        public HouseProfile()
        {
            CreateMap<House, HouseDto>();
            CreateMap<HouseDto, House>();
        }
    }
}
