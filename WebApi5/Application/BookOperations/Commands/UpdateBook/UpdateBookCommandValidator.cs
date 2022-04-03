using System;
using FluentValidation;

namespace WebApi5.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
            RuleFor(command => command.UpdateModel.PageCount).GreaterThanOrEqualTo(0);
            RuleFor(command => command.UpdateModel.GenreId).GreaterThanOrEqualTo(0);
            RuleFor(command => command.UpdateModel.AuthorId).GreaterThanOrEqualTo(0);
            RuleFor(command => command.UpdateModel.PublishDate.Date).Must(x => x == null || x <= DateTime.Now);
            RuleFor(command => command.UpdateModel.Title).Must(x => x == null || x.Trim() == string.Empty ||x.Trim().Length >= 2);
        }
    }
}