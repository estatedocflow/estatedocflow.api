namespace estatedocflow.api.Models.Dtos;

public class ServiceResponse<T>
{
    public bool Success { get; set; }
    public int Code { get; set; }
    public string Message { get; set; } = null!;
    public T Data { get; set; } = default!;
}