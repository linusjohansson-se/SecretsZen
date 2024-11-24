namespace Domain.SecretStrings;

public class SecretText
{
    private SecretText(string secretString, DateTime expirationDate, int views, int amountOfViews, int amountOfDays, bool unlimitedViews, bool unlimitedTime)
    {
        Id = Guid.NewGuid();
        SecretString = secretString; //Implement hasher?
        ExpirationDate = expirationDate;
        Views = views;
        AmountOfViews = amountOfViews;
        AmountOfDays = amountOfDays;
        UnlimitedViews = unlimitedViews;
        UnlimitedTime = unlimitedTime;
    }

    public static SecretText Create(string secretString, int views, int amountOfViews, int amountOfDays, bool unlimitedViews, bool unlimitedTime)
    {
        return new SecretText(
            secretString,
            DateTime.UtcNow.AddDays(amountOfDays),
            views,
            amountOfViews,
            amountOfDays,
            unlimitedViews,
            unlimitedTime);
    }
    public Guid Id { get; private set; }
    
    public string SecretString { get; private set; }
    
    public int AmountOfDays { get; private set; }
    
    public DateTime? ExpirationDate { get; private set; }
    
    public int? Views { get; private set; }
    
    public int AmountOfViews { get; private set; }

    public bool UnlimitedViews { get; private set; }
    
    public bool UnlimitedTime { get; private set; }
}
