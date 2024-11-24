using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Todos.Get;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Secrets.Get;

internal sealed class GetSecretTextsQueryHandler(IApplicationDbContext context, IUserContext userContext) 
    : IQueryHandler<GetSecretTextsQuery, List<SecretTextResponse>>
{
    public async Task<Result<List<SecretTextResponse>>> Handle(GetSecretTextsQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId != userContext.UserId)
        {
            return Result.Failure<List<SecretTextResponse>>(UserErrors.Unauthorized());
        }

        List<SecretTextResponse> todos = await context.TodoItems
            .Where(todoItem => todoItem.UserId == query.UserId)
            .Select(todoItem => new SecretTextResponse
            {
                Id = todoItem.Id,
                UserId = todoItem.UserId,
                Description = todoItem.Description,
                DueDate = todoItem.DueDate,
                Labels = todoItem.Labels,
                IsCompleted = todoItem.IsCompleted,
                CreatedAt = todoItem.CreatedAt,
                CompletedAt = todoItem.CompletedAt
            })
            .ToListAsync(cancellationToken);

        return todos;
    }
}
