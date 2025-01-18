using SharedKernel;

namespace Domain.SecretStrings;

public sealed record SecretTextCreatedDomainEvent(Guid SecretTextId) : IDomainEvent;
