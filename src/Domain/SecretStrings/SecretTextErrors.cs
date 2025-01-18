using SharedKernel;

namespace Domain.SecretStrings;

public static class SecretTextErrors
{
    public static Error NotFound() => Error.NotFound(
        "SecretTexts.NotFound",
        $"The Secret text record could not be found.");
}
