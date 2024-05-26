namespace estatedocflow.api.Models.Dtos;

public class DocumentDto
{
    public Guid? Id { get; set; }
    public int DocumentType { get; set; }
    public string DocumentUrl { get; set; }
}
