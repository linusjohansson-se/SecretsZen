using SharedKernel;

namespace Domain.SecretStrings;

public class SecretText : Entity
{
    private SecretText(string secretString, DateTime expirationDate, int views, int amountOfViews, int amountOfDays, bool unlimitedViews, bool unlimitedTime, DateTime updatedAt, DateTime createdAt)
    {
        Id = Guid.NewGuid();
        SecretString = secretString;
        ExpirationDate = expirationDate;
        Views = views;
        AmountOfViews = amountOfViews;
        AmountOfDays = amountOfDays;
        UnlimitedViews = unlimitedViews;
        UnlimitedTime = unlimitedTime;
        UpdatedAt = updatedAt;
        CreatedAt = createdAt;
    }

    public static SecretText Create(string secretString, int views, int amountOfViews, int amountOfDays, bool unlimitedViews, bool unlimitedTime, DateTime updatedAt, DateTime createdAt)
    {
        return new SecretText(
            secretString,
            DateTime.UtcNow.AddDays(amountOfDays),
            views,
            amountOfViews,
            amountOfDays,
            unlimitedViews,
            unlimitedTime,
            updatedAt,
            createdAt);
    }
    public Guid Id { get; private set; }
    
    public string SecretString { get; private set; }
    
    public int AmountOfDays { get; private set; }
    
    public DateTime ExpirationDate { get; private set; }
    
    public int Views { get; private set; }
    
    public int AmountOfViews { get; private set; }

    public bool UnlimitedViews { get; private set; }
    
    public bool UnlimitedTime { get; private set; }
    
    public DateTime UpdatedAt { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
}
