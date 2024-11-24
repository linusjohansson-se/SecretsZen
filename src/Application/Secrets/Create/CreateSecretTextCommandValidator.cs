﻿using Application.Todos.Create;
using FluentValidation;

namespace Application.Secrets.Create;

public class CreateSecretTextCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateSecretTextCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.Priority).IsInEnum();
        RuleFor(c => c.Description).NotEmpty().MaximumLength(255);
        RuleFor(c => c.DueDate).GreaterThanOrEqualTo(DateTime.Today).When(x => x.DueDate.HasValue);
    }
}
