namespace Application.Secrets;

public sealed class SecretTextResponse
{
    public Guid Id { get; set; }
    public string SecretString { get; set; }
    public int Views { get; set; }
    public int AmountOfViews { get; set; }
    public int AmountOfDays { get; set; }
    public bool UnlimitedViews { get; set; }
    public bool UnlimitedTime { get; set; }
    
    public DateTime ExpirationDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
