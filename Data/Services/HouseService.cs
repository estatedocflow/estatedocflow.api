using estatedocflow.api.Data.Services.Interfaces;
using estatedocflow.api.Models.Dtos;

namespace estatedocflow.api.Data.Services;

public class HouseService : IHouseService
{
    public Task<ServiceResponse<List<HouseDto>>> List()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<HouseDto>> Create(HouseDto houseDto)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<HouseDto>> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<HouseDto?>> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<HouseDto>> Update(HouseDto houseDto)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<HouseDto>> UpdateHouseState(Guid id, PatchHouseStateDto patchStateDto)
    {
        throw new NotImplementedException();
    }
}