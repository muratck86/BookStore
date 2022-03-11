using System;
using FluentAssertions;
using WebApi5.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        UpdateAuthorCommand command = new UpdateAuthorCommand(null);
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
    }
}
