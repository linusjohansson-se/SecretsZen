using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Todos.Get;
using Domain.Todos;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Secrets.GetById;

internal sealed class GetSecretTextByIdQueryHandler(IApplicationDbContext context, IUserContext userContext) 
    : IQueryHandler<GetSecretTextByIdQuery, SecretTextResponse>
{
    public async Task<Result<SecretTextResponse>> Handle(GetSecretTextByIdQuery query, CancellationToken cancellationToken)
    {
        SecretTextResponse? todo = await context.TodoItems
            .Where(todoItem => todoItem.Id == query.TodoItemId && todoItem.UserId == userContext.UserId)
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
            .SingleOrDefaultAsync(cancellationToken);

        if (todo is null)
        {
            return Result.Failure<SecretTextResponse>(TodoItemErrors.NotFound(query.TodoItemId));
        }

        return todo;
    }
}
