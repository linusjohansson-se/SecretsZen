using Application.Abstractions.Messaging;
using Application.Todos.Get;

namespace Application.Secrets.GetById;

public sealed record GetSecretTextByIdQuery(Guid TodoItemId) : IQuery<SecretTextResponse>;
