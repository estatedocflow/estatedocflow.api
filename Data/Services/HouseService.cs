using AutoMapper;
using estatedocflow.api.Data.Repositories.Interfaces;
using estatedocflow.api.Data.Services.Interfaces;
using estatedocflow.api.Extensions;
using estatedocflow.api.Models.Dtos;
using estatedocflow.api.Models.Entities;
using estatedocflow.api.Models.Enums;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace estatedocflow.api.Data.Services;

public class HouseService : IHouseService
{
    private readonly IHouseRepository _houseRepo;
    private readonly IHouseAttachmentRepository _houseAttachmentRepo;
    private readonly IMapper _mapper;
    private readonly FileStore _fileStore;
    public HouseService(IHouseRepository houseRepo,
                        IHouseAttachmentRepository houseAttachmentRepo,
                        IMapper mapper,
                        FileStore fileStore)
    {
        _houseRepo = houseRepo;
        _houseAttachmentRepo = houseAttachmentRepo;
        _mapper = mapper;
        _fileStore = fileStore;
    }
    public async Task<ServiceResponse<List<HouseDto>>> List()
    {
        var sr = new ServiceResponse<List<HouseDto>>();
        try
        {
            var list = _houseRepo.FindAll().ToList();
            var attachmentList = _houseAttachmentRepo.FindAll().ToList();
            var houseList = _mapper.Map<List<HouseDto>>(list);

            foreach (var house in houseList)
            {
                house.AttachmentDto = _mapper.Map<List<HouseAttachmentDto>>(attachmentList.Where(a => a.HouseId == house.Id).ToList());
            }
            if (houseList != null)
            {
                sr.Success = true;
                sr.Code = (int)HttpStatusCode.OK;
                sr.Message = $"{houseList.Count()} record found.";
                sr.Data = houseList;
            }
        }
        catch (Exception ex)
        {
            sr.Success = false;
            sr.Code = (int)HttpStatusCode.NotImplemented;
            sr.Message = $"{ex.Message}, record not found, something went wrong.";
        }

        return sr;
    }
    public async Task<ServiceResponse<HouseDto>> Create(HouseDto houseDto)
    {
        var sr = new ServiceResponse<HouseDto>();
        try
        {
            if (houseDto != null)
            {
                var house = _mapper.Map<House>(houseDto);
                List<HouseAttachmentDto> attachment = new List<HouseAttachmentDto>();
                if (house != null)
                {
                    _houseRepo.Create(house);
                    await _houseRepo.SaveChangesAsync();
                    if (houseDto.AttachmentDto?.Any() == true)
                    {
                        List<HouseAttachment> attachments = _mapper.Map<List<HouseAttachment>>(houseDto.AttachmentDto);
                        attachments.ForEach(a => a.HouseId = house.Id);
                        _houseAttachmentRepo.BulkCreate(attachments.AsQueryable());
                        await _houseAttachmentRepo.SaveChangesAsync();
                    }

                    sr.Success = true;
                    sr.Code = (int)HttpStatusCode.OK;
                    sr.Data = _mapper.Map<HouseDto>(house);
                    sr.Message = "House inserted successfully.";
                }
            }
            else
            {
                sr.Success = false;
                sr.Code = (int)HttpStatusCode.NotImplemented;
                sr.Message = "House not inserted successfully.";
            }
        }
        catch (Exception ex)
        {
            sr.Success = false;
            sr.Code = (int)HttpStatusCode.NotImplemented;
            sr.Message = $"{ex.Message}, House not inserted, something went wrong.";
        }
        return sr;
    }
    public async Task<ServiceResponse<HouseDto>> Get(Guid id)
    {
        var sr = new ServiceResponse<HouseDto>();
        try
        {
            var house = _houseRepo.FindByCondition(a => a.Id == id).FirstOrDefault();
            var attachments = _houseAttachmentRepo.FindByCondition(a => a.HouseId == id).ToList();
            if (house != null)
            {

                var houseDto = _mapper.Map<HouseDto>(house);
                houseDto.AttachmentDto = _mapper.Map<List<HouseAttachmentDto>>(attachments);

                sr.Success = true;
                sr.Code = (int)HttpStatusCode.OK;
                sr.Message = $"Record found.";
                sr.Data = houseDto;
            }
        }
        catch (Exception ex)
        {
            sr.Success = false;
            sr.Code = (int)HttpStatusCode.NotImplemented;
            sr.Message = $"{ex.Message}, record not found, something went wrong.";
        }
        return sr;
    }
    public async Task<ServiceResponse<HouseDto>> Delete(Guid id)
    {
        var sr = new ServiceResponse<HouseDto>();
        try
        {
            var house = _houseRepo.FindByCondition(a => a.Id == id).FirstOrDefault();
            if (house != null)
            {
                _houseRepo.Delete(house);
                await _houseRepo.SaveChangesAsync();

                sr.Success = true;
                sr.Code = (int)HttpStatusCode.OK;
                sr.Message = $"House of {house.OwnerFirstName} was deleted.";
                sr.Data = null;
            }
            else
            {
                sr.Success = false;
                sr.Code = (int)HttpStatusCode.NotImplemented;
                sr.Message = "House not deleted successfully.";
            }
        }
        catch (Exception ex)
        {
            sr.Success = false;
            sr.Code = (int)HttpStatusCode.NotImplemented;
            sr.Message = $"{ex.Message}, something went wrong.";
        }
        return sr;
    }
    public async Task<ServiceResponse<HouseDto>> Update(HouseDto houseDto)
    {
        var sr = new ServiceResponse<HouseDto>();
        try
        {
            if (houseDto.Id != null || houseDto.Id != Guid.Empty)
            {
                var house = _mapper.Map<House>(houseDto);
                List<HouseAttachmentDto> attachment = new List<HouseAttachmentDto>();
                if (house != null)
                {
                    _houseRepo.Update(house);
                    await _houseRepo.SaveChangesAsync();

                    if (houseDto.AttachmentDto?.Any() == true)
                    {
                        var attachmentDtos = houseDto.AttachmentDto;
                        var existingAttachments = _houseAttachmentRepo.FindByCondition(a => a.HouseId == house.Id).ToList();

                        // Separate attachments to update and to create
                        var attachmentsToUpdate = new List<HouseAttachment>();
                        var attachmentsToCreate = new List<HouseAttachment>();

                        foreach (var attachmentDto in attachmentDtos)
                        {
                            var attachments = existingAttachments.FirstOrDefault(a => a.Id == attachmentDto.Id);
                            if (attachments != null)
                            {
                                // Update existing attachment
                                _mapper.Map(attachmentDto, attachments);
                                attachmentsToUpdate.Add(attachments);
                            }
                            else
                            {
                                // Create new attachment
                                var newAttachment = _mapper.Map<HouseAttachment>(attachmentDto);
                                newAttachment.HouseId = house.Id;
                                attachmentsToCreate.Add(newAttachment);
                            }
                        }

                        // Perform bulk update
                        if (attachmentsToUpdate.Any())
                        {
                            _houseAttachmentRepo.BulkUpdate(attachmentsToUpdate.AsQueryable());
                        }

                        // Perform bulk create
                        if (attachmentsToCreate.Any())
                        {
                            _houseAttachmentRepo.BulkCreate(attachmentsToCreate.AsQueryable());
                        }

                        await _houseAttachmentRepo.SaveChangesAsync();
                    }

                    sr.Success = true;
                    sr.Code = (int)HttpStatusCode.OK;
                    sr.Data = _mapper.Map<HouseDto>(house);
                    sr.Message = "House updated successfully.";
                }
                else
                {
                    sr.Success = false;
                    sr.Code = (int)HttpStatusCode.NotImplemented;
                    sr.Message = "House not updated successfully.";
                }
            }
        }
        catch (Exception ex)
        {
            sr.Success = false;
            sr.Code = (int)HttpStatusCode.NotImplemented;
            sr.Message = $"{ex.Message}, House not updated, something went wrong.";
        }
        return sr;
    }
    public async Task<ServiceResponse<HouseDto>> UpdateHouseState(Guid id, PatchHouseStateDto patchStateDto)
    {
        var sr = new ServiceResponse<HouseDto>();
        try
        {
            var house = _houseRepo.FindByCondition(a => a.Id == id).FirstOrDefault();
            var attachments = _houseAttachmentRepo.FindByCondition(a => a.HouseId == id).ToList();
            if (house != null)
            {
                house.State = patchStateDto.State;
                _houseRepo.Update(house);
                await _houseRepo.SaveChangesAsync();

                var houseDto = _mapper.Map<HouseDto>(house);
                houseDto.AttachmentDto = _mapper.Map<List<HouseAttachmentDto>>(attachments);

                sr.Success = true;
                sr.Code = (int)HttpStatusCode.OK;
                sr.Message = "House updated successfully.";
                sr.Data = houseDto;
            }
            else
            {
                sr.Success = false;
                sr.Code = (int)HttpStatusCode.NotImplemented;
                sr.Message = "House not updated successfully.";
            }
        }
        catch (Exception ex)
        {
            sr.Success = false;
            sr.Code = (int)HttpStatusCode.NotImplemented;
            sr.Message = $"{ex.Message}, House not updated, something went wrong.";
        }
        return sr;
    }
    public async Task<ServiceResponse<HouseDto>> UploadPhoto(Guid houseId, PatchPhotoDto patchPhotoDto)
    {
        var sr = new ServiceResponse<HouseDto>();
        try
        {
            if (patchPhotoDto.PhotoUrls.Any())
            {
                // Todo generate photo url

                var houseAttachments = patchPhotoDto.PhotoUrls.Select(photoUrl => new HouseAttachment
                {
                    PhotoUrl = photoUrl, // Assuming PhotoUrl should be set based on the document information
                    HouseId = houseId,
                    DocumentType = DocumentType.Photo,
                }).ToList();

                //var keyName = "";
                //foreach ( var houseAttachment in houseAttachments)
                //{
                //    await _fileStore.UploadDocument(houseAttachment.PhotoUrl, keyName);
                //}
                var attachments = _mapper.Map<List<HouseAttachment>>(houseAttachments);
                _houseAttachmentRepo.BulkCreate(attachments.AsQueryable());
                await _houseAttachmentRepo.SaveChangesAsync();

                var house = new HouseDto();
                var houseObj = await _houseRepo.FindByCondition(a => a.Id == houseId).FirstOrDefaultAsync();
                var attachmentsObj = _houseAttachmentRepo.FindByCondition(a => a.HouseId == houseId).ToList();
                if (houseObj != null)
                {
                    house = _mapper.Map<HouseDto>(houseObj);
                    house.AttachmentDto = _mapper.Map<List<HouseAttachmentDto>>(attachmentsObj);
                }

                sr.Success = true;
                sr.Code = (int)HttpStatusCode.OK;
                sr.Data = house;
                sr.Message = "House photo uploaded successfully.";

            }
            else
            {
                sr.Success = false;
                sr.Code = (int)HttpStatusCode.NotImplemented;
                sr.Message = "House photo not uploaded successfully.";
            }
        }
        catch (Exception ex)
        {
            sr.Success = false;
            sr.Code = (int)HttpStatusCode.NotImplemented;
            sr.Message = $"{ex.Message}, House photo not uploaded, something went wrong.";
        }
        return sr;
    }
    public async Task<ServiceResponse<HouseDto>> UploadDocument(Guid houseId, PatchDocumentDto patchDocumentDto)
    {
        var sr = new ServiceResponse<HouseDto>();
        try
        {
            if (patchDocumentDto.Documents.Count() > 0)
            {
                // Todo generate document url

                var houseAttachments = patchDocumentDto.Documents.Select(document => new HouseAttachment
                {
                    PhotoUrl = document.DocumentUrl, // Assuming documentUrl should be set based on the document information
                    HouseId = houseId,
                    DocumentType = DocumentType.Document
                }).ToList();

                var attachments = _mapper.Map<List<HouseAttachment>>(houseAttachments);
                _houseAttachmentRepo.BulkCreate(attachments.AsQueryable());
                await _houseAttachmentRepo.SaveChangesAsync();

                var house = new HouseDto();
                var houseObj = await _houseRepo.FindByCondition(a => a.Id == houseId).FirstOrDefaultAsync();
                var attachmentsObj = _houseAttachmentRepo.FindByCondition(a => a.HouseId == houseId).ToList();
                if (houseObj != null)
                {
                    house = _mapper.Map<HouseDto>(houseObj);
                    house.AttachmentDto = _mapper.Map<List<HouseAttachmentDto>>(attachmentsObj);
                }

                sr.Success = true;
                sr.Code = (int)HttpStatusCode.OK;
                sr.Data = house;
                sr.Message = "House document uploaded successfully.";

            }
            else
            {
                sr.Success = false;
                sr.Code = (int)HttpStatusCode.NotImplemented;
                sr.Message = "House document not uploaded successfully.";
            }
        }
        catch (Exception ex)
        {
            sr.Success = false;
            sr.Code = (int)HttpStatusCode.NotImplemented;
            sr.Message = $"{ex.Message}, House document not uploaded, something went wrong.";
        }
        return sr;
    }
}