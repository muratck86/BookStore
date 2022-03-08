using FluentValidation;

namespace WebApi5.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            //new name must be longer than 3 chars if it is not empty.
            RuleFor(command => command.Model.Name).MinimumLength(3).When(command => command.Model.Name.Trim() != string.Empty);
            RuleFor(command => command.GenreId).GreaterThan(0);
        }
    }
}