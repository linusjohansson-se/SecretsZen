using Application.Secrets.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SecretTexts;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("secrettexts", async (CreateSecretTextCommand request, ISender sender, CancellationToken cancellationToken) =>
            {
                Result<Guid> result = await sender.Send(request, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.SecretTexts)
            .RequireAuthorization();
    }
}
