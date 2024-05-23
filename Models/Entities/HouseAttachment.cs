using estatedocflow.api.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace estatedocflow.api.Models.Entities;

public class HouseAttachment
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey("House")]
    public Guid HouseId { get; set; }
    public string PhotoUrl { get; set; }
    public DocumentType DocumentType { get; set; }
    public virtual House House { get; set; }
}

