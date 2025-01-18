using Application.Abstractions.Messaging;

namespace Application.Secrets.Delete;

public sealed record DeleteSecretTextCommand(Guid Id) : ICommand;
