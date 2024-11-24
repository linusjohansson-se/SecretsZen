using Domain.SecretStrings;
using Domain.Todos;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<SecretText> SecretTexts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
