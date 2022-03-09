using System;
using FluentValidation;

namespace WebApi5.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).GreaterThan(0);
            RuleFor(command => command.UpdateModel.BirthDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.UpdateModel.Name).NotEmpty().MinimumLength(2);
            RuleFor(command => command.UpdateModel.LastName).NotEmpty().MinimumLength(2);
        }
    }
}