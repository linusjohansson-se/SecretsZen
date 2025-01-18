using SharedKernel;

namespace Domain.SecretStrings;

public sealed record SecretTextDeletedDomainEvent(Guid SecretTextId) : IDomainEvent;
