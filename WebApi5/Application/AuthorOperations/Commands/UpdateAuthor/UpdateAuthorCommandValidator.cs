using System;
using FluentValidation;

namespace WebApi5.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).GreaterThan(0);
            RuleFor(command => command.UpdateModel.BirthDate).Must(x => x.Year < DateTime.Now.Year || x == null || x.Year == 1);
            RuleFor(command => command.UpdateModel.Name).Must(x => (x == null || x.Trim().Length >= 2 || x == string.Empty));
            RuleFor(command => command.UpdateModel.LastName).Must(x => (x == null || x.Trim().Length >= 2 || x == string.Empty));
        }
    }
}