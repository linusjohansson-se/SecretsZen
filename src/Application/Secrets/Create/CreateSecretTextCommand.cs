using Application.Abstractions.Messaging;
using Domain.Todos;

namespace Application.Secrets.Create;

public sealed class CreateSecretTextCommand : ICommand<Guid>
{
    public Guid UserId { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public List<string> Labels { get; set; } = [];
    public Priority Priority { get; set; }
}
