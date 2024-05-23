using estatedocflow.api.Models.Dtos;

namespace estatedocflow.api.Data.Services.Interfaces;

public interface IHouseService
{
    Task<ServiceResponse<List<HouseDto>>> List();
    Task<ServiceResponse<HouseDto>> Create(HouseDto houseDto);
    Task<ServiceResponse<HouseDto>> Get(Guid id);
    Task<ServiceResponse<HouseDto?>> Delete(Guid id);
    Task<ServiceResponse<HouseDto>> Update(HouseDto houseDto);
    Task<ServiceResponse<HouseDto>> UpdateHouseState(Guid id, PatchHouseStateDto patchStateDto);
}