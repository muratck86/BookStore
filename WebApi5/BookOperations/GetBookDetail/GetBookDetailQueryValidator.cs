using System;
using FluentValidation;

namespace WebApi5.BookOperations.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(q => q.BookId).GreaterThan(0);

        }
    }
}