using SharedKernel;

namespace Domain.SecretStrings;

public static class SecretTextErrors
{
    public static Error NotFound()
    {
        return Error.NotFound(
            "SecretTexts.NotFound",
            "The Secret text record could not be found.");
    }

    public static Error ViewsZero()
    {
        return Error.Problem(
            "SecretTexts.ViewsZero",
            "AmountOfViews can't be zero");
    }

    public static Error DaysZero()
    {
        return Error.Problem(
            "SecretTexts.DaysZero",
            "AmountOfDays can't be zero");
    }

    public static Error Expired()
    {
        return Error.Problem(
            "SecretTexts.Expired",
            "The Secret text expired.");
    }
}
