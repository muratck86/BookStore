using System;
using FluentValidation;

namespace WebApi5.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().NotNull().Matches("^([^0-9]*)$");
            RuleFor(command => command.Model.LastName).NotEmpty().NotNull().Matches("^([^0-9]*)$");
            RuleFor(command => command.Model.BirthDate).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}