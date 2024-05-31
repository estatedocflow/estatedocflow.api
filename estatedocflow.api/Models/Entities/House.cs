using estatedocflow.api.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace estatedocflow.api.Models.Entities;

public class House
{
    [Key]
    public Guid Id { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string OwnerFirstName { get; set; }
    public string OwnerLastName { get; set; }
    public string Description { get; set; }
    public HouseState State { get; set; }
    public virtual IEnumerable<HouseAttachment>? HouseAttachments { get; set; }
}