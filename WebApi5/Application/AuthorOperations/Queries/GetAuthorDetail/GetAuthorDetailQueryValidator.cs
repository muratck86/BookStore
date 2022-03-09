using FluentValidation;

namespace WebApi5.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailQueryValidator()
        {
            RuleFor(q => q.AuthorId).GreaterThan(0);

        }
    }
}