using Application.Abstractions.Messaging;

namespace Application.Secrets.GetById;

public sealed record GetSecretTextByIdQuery(Guid Id) : IQuery<SecretTextResponse>;
