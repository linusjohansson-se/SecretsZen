using SharedKernel;

namespace Domain.SecretStrings;

public static class SecretTextErrors
{
    public static Error NotFound() => Error.NotFound(
        "SecretTexts.NotFound",
        $"The Secret text record could not be found.");
    
    public static Error ViewsZero() => Error.Problem(
        "SecretTexts.ViewsZero",
        $"AmountOfViews can't be zero");
    
    public static Error DaysZero() => Error.Problem(
        "SecretTexts.DaysZero",
        $"AmountOfDays can't be zero");
}
