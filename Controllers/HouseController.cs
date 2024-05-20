using estatedocflow.Data.Repositories.IRepository.House;
using estatedocflow.Data.Services.Iservice;
using estatedocflow.Models.Dtos.House;
using estatedocflow.Models.Dtos.Shared;
using Microsoft.AspNetCore.Mvc;

namespace estatedocflow.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IHouseService _houseService;
        public HouseController(IHouseService houseService)
        {
            _houseService = houseService;
        }
        [HttpPost]
        public async Task<ServiceResponse<HouseDto>> Create(HouseDto houseDto)
        {
            var sr = new ServiceResponse<HouseDto>();
            try
            {
                sr = await _houseService.Create(houseDto);
                if (sr.Success == true)
                {
                    sr.Success = true;
                    sr.Code = sr.Code;
                    sr.Message = "House inserted successfully.";
                }
                else
                {
                    sr.Success = false;
                    sr.Code = 500;
                    sr.Message = $"House not inserted successfully.";
                }
            }
            catch (Exception ex)
            {
                sr.Code = sr.Code;
                sr.Message = $"{ex.Message}, House not inserted, something went wrong.";
            }
            return sr;
        }
    }
}
