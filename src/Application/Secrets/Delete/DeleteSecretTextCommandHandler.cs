using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.SecretStrings;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Secrets.Delete;

internal sealed class DeleteSecretTextCommandHandler(IApplicationDbContext context)
    : ICommandHandler<DeleteSecretTextCommand>
{
    public async Task<Result> Handle(DeleteSecretTextCommand command, CancellationToken cancellationToken)
    {
        SecretText? secretText = await context.SecretTexts
            .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (secretText is null)
        {
            return Result.Failure(SecretTextErrors.NotFound());
        }

        context.SecretTexts.Remove(secretText);

        secretText.Raise(new SecretTextDeletedDomainEvent(secretText.Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

