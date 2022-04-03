using FluentValidation;

namespace WebApi5.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            //new name must be longer than 3 chars if it is not empty.
            RuleFor(command => command.Model.Name).Must(com => com == null || com.Trim() == string.Empty || com.Trim().Length > 2);
            RuleFor(command => command.GenreId).GreaterThan(0);
        }
    }
}