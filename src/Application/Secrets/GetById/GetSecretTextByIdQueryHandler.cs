using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Secrets.Delete;
using Domain.SecretStrings;
using Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Secrets.GetById;

internal sealed class GetSecretTextByIdQueryHandler(
    IApplicationDbContext context,
    ISender sender,
    IDateTimeProvider dateTimeProvider,
    IEncryptionService encryptionService)
    : IQueryHandler<GetSecretTextByIdQuery, SecretTextResponse>
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly IEncryptionService _encryptionService = encryptionService;

    public async Task<Result<SecretTextResponse>> Handle(GetSecretTextByIdQuery query,
        CancellationToken cancellationToken)
    {
        SecretText? secretText = await context.SecretTexts
            .Where(x => x.Id == query.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (secretText is null)
        {
            return Result.Failure<SecretTextResponse>(SecretTextErrors.NotFound());
        }

        secretText.UpdateViews(secretText.Views);

        await context.SaveChangesAsync(cancellationToken);

        if (!secretText.UnlimitedViews && secretText.Views >= secretText.AmountOfViews)
        {
            await sender.Send(new DeleteSecretTextCommand(secretText.Id), cancellationToken);
        }

        if (!secretText.UnlimitedTime && secretText.ExpirationDate < _dateTimeProvider.UtcNow)
        {
            await sender.Send(new DeleteSecretTextCommand(secretText.Id), cancellationToken);
            return Result.Failure<SecretTextResponse>(SecretTextErrors.Expired());
        }

        string decryptedSecret;
        try
        {
            decryptedSecret = _encryptionService.Decrypt(secretText.SecretString);
        }
        catch
        {
            return Result.Failure<SecretTextResponse>(SecretTextErrors.DecryptFailed());
        }


        var response = new SecretTextResponse
        {
            Id = secretText.Id,
            SecretString = decryptedSecret,
            Views = secretText.Views,
            AmountOfViews = secretText.AmountOfViews,
            AmountOfDays = secretText.AmountOfDays,
            UnlimitedViews = secretText.UnlimitedViews,
            UnlimitedTime = secretText.UnlimitedTime,
            ExpirationDate = secretText.ExpirationDate,
            UpdatedAt = secretText.UpdatedAt,
            CreatedAt = secretText.CreatedAt
        };

        return Result.Success(response);
    }
}
