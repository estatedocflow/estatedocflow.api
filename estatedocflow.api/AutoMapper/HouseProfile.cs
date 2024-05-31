using AutoMapper;
using estatedocflow.api.Models.Dtos;
using estatedocflow.api.Models.Entities;

namespace estatedocflow.api.AutoMapper
{
    public class HouseProfile : Profile
    {
        public HouseProfile()
        {
            CreateMap<House, HouseDto>().ReverseMap();
            CreateMap<HouseAttachment, HouseAttachmentDto>().ReverseMap();
        }
    }
}
