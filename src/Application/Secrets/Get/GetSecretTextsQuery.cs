using Application.Abstractions.Messaging;
using Application.Todos.Get;

namespace Application.Secrets.Get;

public sealed record GetSecretTextsQuery(Guid UserId): IQuery<List<SecretTextResponse>>;
