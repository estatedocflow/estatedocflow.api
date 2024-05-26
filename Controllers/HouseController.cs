using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Net;
using estatedocflow.api.Data.Services.Interfaces;
using estatedocflow.api.Models.Dtos;
using estatedocflow.api.Data.Services;

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

        [HttpGet("list")]
        public async Task<ServiceResponse<List<HouseDto>>> List()
        {
            var sr = new ServiceResponse<List<HouseDto>>();
            try
            {
                sr = await _houseService.List();
                sr.Success = true;
                sr.Code = sr.Code;
                sr.Message = $"{sr.Data.Count} Record found.";
                sr.Data = sr.Data;
                return sr;
            }
            catch (DbException ex)
            {
                sr.Code = sr.Code;
                sr.Message = ex.Message;
            }
            catch (Exception ex)
            {
                sr.Code = sr.Code;
                sr.Message = $"{ex.Message}, No record found.";
            }
            return sr;
        }

        [HttpPost]
        public async Task<ServiceResponse<HouseDto>> Create(HouseDto houseDto)
        {
            var sr = new ServiceResponse<HouseDto>();
            try
            {
                sr = await _houseService.Create(houseDto);
                if (sr.Success)
                {
                    sr.Success = true;
                    sr.Code = sr.Code;
                    sr.Message = sr.Message;
                }
                else
                {
                    sr.Success = false;
                    sr.Code = sr.Code;
                    sr.Message = sr.Message;
                }
            }
            catch (DbException ex)
            {
                sr.Code = (int)HttpStatusCode.NotImplemented;
                sr.Message = ex.Message;
            }
            catch (Exception ex)
            {
                sr.Code = sr.Code;
                sr.Message = $"{ex.Message}, House not inserted, something went wrong.";
            }
            return sr;
        }

        [HttpGet("{id}")]
        public async Task<ServiceResponse<HouseDto>> Get(Guid id)
        {
            var sr = new ServiceResponse<HouseDto>();
            try
            {
                sr = await _houseService.Get(id);
                sr.Success = true;
                sr.Code = sr.Code;
                sr.Message = sr.Message;
                sr.Data = sr.Data;
                return sr;
            }
            catch (DbException ex)
            {
                sr.Code = sr.Code;
                sr.Message = ex.Message;
            }
            catch (Exception ex)
            {
                sr.Code = sr.Code;
                sr.Message = $"{ex.Message}, No record found.";
            }
            return sr;
        }

        [HttpDelete("{id}")]
        public async Task<ServiceResponse<HouseDto?>> Delete(Guid id)
        {
            var sr = new ServiceResponse<HouseDto?>();
            try
            {
                sr = await _houseService.Delete(id);
                sr.Success = true;
                sr.Code = sr.Code;
                sr.Message = sr.Message;
                sr.Data = null;
                return sr;
            }
            catch (DbException ex)
            {
                sr.Code = (int)HttpStatusCode.NotImplemented;
                sr.Message = ex.Message;
            }
            catch (Exception ex)
            {
                sr.Code = (int)HttpStatusCode.NotImplemented;
                sr.Message = ex.Message;
            }
            return sr;
        }

        [HttpPut]
        public async Task<ServiceResponse<HouseDto>> Update(HouseDto houseDto)
        {
            var sr = new ServiceResponse<HouseDto>();
            try
            {
                sr = await _houseService.Update(houseDto);
                if (sr.Success)
                {
                    sr.Success = true;
                    sr.Code = sr.Code;
                    sr.Message = sr.Message;
                }
                else
                {
                    sr.Success = false;
                    sr.Code = sr.Code;
                    sr.Message = sr.Message;
                }
            }
            catch (DbException ex)
            {
                sr.Code = (int)HttpStatusCode.NotImplemented;
                sr.Message = ex.Message;
            }
            catch (Exception ex)
            {
                sr.Code = sr.Code;
                sr.Message = $"{ex.Message}, House not updated, something went wrong.";
            }
            return sr;
        }

        [HttpPatch("{id}/state")]
        public async Task<ServiceResponse<HouseDto>> PatchHouseState(Guid id, PatchHouseStateDto patchStateDto)
        {
            var sr = new ServiceResponse<HouseDto>();
            try
            {
                sr = await _houseService.UpdateHouseState(id, patchStateDto);
                if (sr.Success)
                {
                    sr.Success = true;
                    sr.Code = sr.Code;
                    sr.Message = sr.Message;
                }
                else
                {
                    sr.Success = false;
                    sr.Code = sr.Code;
                    sr.Message = sr.Message;
                }
            }
            catch (DbException ex)
            {
                sr.Code = (int)HttpStatusCode.NotImplemented;
                sr.Message = ex.Message;
            }
            catch (Exception ex)
            {
                sr.Code = sr.Code;
                sr.Message = $"{ex.Message}, House state not updated, something went wrong.";
            }
            return sr;
        }

        [HttpPatch("{id}/photo")]
        public async Task<ServiceResponse<HouseDto>> UploadPhoto(Guid id, PatchPhotoDto patchPhotoDto)
        {
            var sr = new ServiceResponse<HouseDto>();
            try
            {
                sr = await _houseService.UploadPhoto(id, patchPhotoDto);
                if (sr.Success)
                {
                    sr.Success = true;
                    sr.Code = sr.Code;
                    sr.Message = sr.Message;
                }
                else
                {
                    sr.Success = false;
                    sr.Code = sr.Code;
                    sr.Message = sr.Message;
                }
            }
            catch (DbException ex)
            {
                sr.Code = (int)HttpStatusCode.NotImplemented;
                sr.Message = ex.Message;
            }
            catch (Exception ex)
            {
                sr.Code = sr.Code;
                sr.Message = $"{ex.Message}, House photo not uploaded, something went wrong.";
            }
            return sr;
        }

        [HttpPatch("{id}/document")]
        public async Task<ServiceResponse<HouseDto>> UploadDocument(Guid id, PatchDocumentDto patchDocumentDto)
        {
            var sr = new ServiceResponse<HouseDto>();
            try
            {
                sr = await _houseService.UploadDocument(id, patchDocumentDto);
                if (sr.Success)
                {
                    sr.Success = true;
                    sr.Code = sr.Code;
                    sr.Message = sr.Message;
                }
                else
                {
                    sr.Success = false;
                    sr.Code = sr.Code;
                    sr.Message = sr.Message;
                }
            }
            catch (DbException ex)
            {
                sr.Code = (int)HttpStatusCode.NotImplemented;
                sr.Message = ex.Message;
            }
            catch (Exception ex)
            {
                sr.Code = sr.Code;
                sr.Message = $"{ex.Message}, House document not uploaded, something went wrong.";
            }
            return sr;
        }
    }
}
