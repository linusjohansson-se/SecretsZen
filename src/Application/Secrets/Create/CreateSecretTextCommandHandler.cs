using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.SecretStrings;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Secrets.Create;

internal sealed class CreateSecretTextCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateSecretTextCommand, Guid>
{
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
        
        var secretText = SecretText.Create(
            command.SecretString,
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
