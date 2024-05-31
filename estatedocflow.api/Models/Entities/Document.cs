using System.ComponentModel.DataAnnotations;

namespace estatedocflow.api.Models.Entities;

public class Document
{
    [Key]
    public Guid Id { get; set; }
    public string DocumentType { get; set; }
    public string DocumentUrl { get; set; }
}

