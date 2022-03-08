using System;
using FluentValidation;

namespace WebApi5.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
            RuleFor(command => command.UpdateModel.GenreId).IsInEnum();
            RuleFor(command => command.UpdateModel.PageCount).GreaterThan(0);
            RuleFor(command => command.UpdateModel.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.UpdateModel.Title).NotEmpty().MinimumLength(2);
        }
    }
}