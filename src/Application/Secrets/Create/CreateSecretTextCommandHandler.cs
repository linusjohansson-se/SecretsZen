using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.SecretStrings;
using Domain.Todos;
using Domain.Users;
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
        var secretText = SecretText.Create(
            command.SecretString,
            command.Views,
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
