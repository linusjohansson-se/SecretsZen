using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.SecretStrings;
using Domain.Services;
using SharedKernel;

namespace Application.Secrets.Create;

internal sealed class CreateSecretTextCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider,
    IEncryptionService encryptionService)
    : ICommandHandler<CreateSecretTextCommand, Guid>
{
    private readonly IEncryptionService _encryptionService = encryptionService;

    public async Task<Result<Guid>> Handle(CreateSecretTextCommand command, CancellationToken cancellationToken)
    {
        if (!command.UnlimitedTime && command.AmountOfDays < 1)
        {
            return Result.Failure<Guid>(SecretTextErrors.DaysZero());
        }

        if (!command.UnlimitedViews && command.AmountOfViews < 1)
        {
            return Result.Failure<Guid>(SecretTextErrors.ViewsZero());
        }

        string secret = _encryptionService.Encrypt(command.SecretString);

        var secretText = SecretText.Create(
            secret,
            command.AmountOfViews,
            command.AmountOfDays,
            command.UnlimitedViews,
            command.UnlimitedTime,
            dateTimeProvider.UtcNow,
            dateTimeProvider.UtcNow
        );

        secretText.Raise(new SecretTextCreatedDomainEvent(secretText.Id));

        context.SecretTexts.Add(secretText);

        await context.SaveChangesAsync(cancellationToken);

        return secretText.Id;
    }
}
