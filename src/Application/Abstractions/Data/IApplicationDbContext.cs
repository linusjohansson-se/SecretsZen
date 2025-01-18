using Domain.SecretStrings;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<SecretText> SecretTexts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
