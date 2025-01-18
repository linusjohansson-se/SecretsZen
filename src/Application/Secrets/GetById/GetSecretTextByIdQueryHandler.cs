using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Secrets.GetById;

internal sealed class GetSecretTextByIdQueryHandler(IApplicationDbContext context) 
    : IQueryHandler<GetSecretTextByIdQuery, SecretTextResponse>
{
    public async Task<Result<SecretTextResponse>> Handle(GetSecretTextByIdQuery query, CancellationToken cancellationToken)
    {
        SecretTextResponse? secretText = await context.SecretTexts
            .Where(x => x.Id == query.Id)
            .Select(x => new SecretTextResponse
            {
                Id = x.Id,
                SecretString = x.SecretString,
                Views = x.Views,
                AmountOfViews = x.AmountOfViews,
                AmountOfDays = x.AmountOfDays,
                UnlimitedViews = x.UnlimitedViews,
                UnlimitedTime = x.UnlimitedTime,
                ExpirationDate = x.ExpirationDate,
                UpdatedAt = x.UpdatedAt,
                CreatedAt = x.CreatedAt
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (secretText is null)
        {
            //return Result.Failure<SecretTextResponse>(SecretTextEr.NotFound(query.Id));
        }

        return secretText;
    }
}
