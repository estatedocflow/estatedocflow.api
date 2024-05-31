namespace estatedocflow.api.Models.Dtos;

public class CreateHouseDto
{
    public Guid? Id { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string OwnerFirstName { get; set; }
    public string OwnerLastName { get; set; }
    public string Description { get; set; }
}
