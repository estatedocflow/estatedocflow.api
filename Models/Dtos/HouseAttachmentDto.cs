using estatedocflow.api.Models.Enums;

namespace estatedocflow.api.Models.Dtos;

public class HouseAttachmentDto
{
    public Guid? Id { get; set; }
    public Guid? HouseId { get; set; }
    public string PhotoUrl { get; set; }
    public DocumentType DocumentType { get; set; }
}
