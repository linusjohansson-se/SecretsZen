using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Secrets.Delete;
using Domain.SecretStrings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Secrets.GetById;

internal sealed class GetSecretTextByIdQueryHandler(IApplicationDbContext context, ISender sender) 
    : IQueryHandler<GetSecretTextByIdQuery, SecretTextResponse>
{
    public async Task<Result<SecretTextResponse>> Handle(GetSecretTextByIdQuery query, CancellationToken cancellationToken)
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
        
        var response = new SecretTextResponse
        {
            Id = secretText.Id,
            SecretString = secretText.SecretString,
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
