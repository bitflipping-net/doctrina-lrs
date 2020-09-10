using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Statements.Commands;
using Doctrina.Application.Statements.Notifications;
using Doctrina.Application.Tests.Infrastructure;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace Doctrina.Application.Tests.Statements.Commands
{
    public class CreateStatementCommandTests : CommandTestBase
    {
        [Fact]
        public async void ShouldRaiseStatementCreatingNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var authorityMock = new Mock<IAuthorityContext>();

            var sut = new CreateStatementCommandHandler(_context, mediatorMock.Object, _mapper, authorityMock.Object);
            Statement statement = GetStatement(Guid.NewGuid());

            // Act
            var result = await sut.Handle(CreateStatementCommand.Create(statement), CancellationToken.None);

            // Assert
            mediatorMock.Verify(m => m.Publish(
                It.IsAny<StatementCreating>(),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async void Handle_GivenValidRequest_ShouldRaiseStatementCreatedNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var authorityMock = new Mock<IAuthorityContext>();

            var createStatement = new CreateStatementCommandHandler(_context, mediatorMock.Object, _mapper, authorityMock.Object);
            Guid newStatementId = Guid.NewGuid();
            Statement statement = GetStatement(newStatementId);

            // Act
            var result = await createStatement.Handle(CreateStatementCommand.Create(statement), CancellationToken.None);

            // Assert
            mediatorMock.Verify(m => m.Publish(
                It.Is<StatementCreated>(cc => cc.Created.StatementId == newStatementId),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        private static Statement GetStatement(Guid newStatementId)
        {
            var statement = new Statement()
            {
                Id = newStatementId,
                Actor = new Agent() { Mbox = new Mbox("mailto:testing@example.com")},
                Object = new Activity() { Id = new Iri("https://bitflipping.net/activity/testing") },
                Verb = new Verb() { Id = new Iri("https://bitflipping.net/verbs/testings") }
            };
            statement.Id = newStatementId;
            return statement;
        }
    }
}
