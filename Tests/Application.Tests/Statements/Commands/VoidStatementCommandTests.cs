using Doctrina.Application.Common;
using Doctrina.Application.Statements.Commands;
using Doctrina.Application.Statements.Notifications;
using Doctrina.Application.Statements.Queries;
using Doctrina.Application.Tests.Infrastructure;
using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Extensions;
using MediatR;
using Moq;
using Shouldly;
using System;
using System.Threading;
using Xunit;

namespace Doctrina.Application.Tests.Statements.Commands
{
    public class VoidStatementCommandTests : CommandTestBase
    {
        [Fact]
        public async void ShouldReturnVoidedStatement()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var storeHttpContextMock = new Mock<IClientHttpContext>();
            storeHttpContextMock.SetupGet(c => c.GetClient())
                .Returns(new Domain.Models.Client()
                {
                    Authority = new Agent()
                    {
                        Account = new Account()
                        {
                            HomePage = new Uri("https://doctrina.net"),
                            Name = "User"
                        }
                    }.ToJson()
                });

            var createStatementHandler = new CreateStatementCommandHandler(
                _storeContext, storeHttpContextMock.Object, mediatorMock.Object, _mapper
            );

            var voidStatementQueryHandler = new VoidedStatemetQueryHandler(_context, _mapper);
            var voidedStatementId = Guid.Parse("637E9E80-4B8D-4640-AC13-615C3E413568");

            var statement = new Statement("{\"actor\":{\"objectType\":\"Agent\",\"name\":\"xAPI mbox\",\"mbox\":\"mailto:xapi@adlnet.gov\"},\"verb\":{\"id\":\"http://adlnet.gov/expapi/verbs/attended\",\"display\":{\"en-GB\":\"attended\",\"en-US\":\"attended\"}},\"object\":{\"objectType\":\"Activity\",\"id\":\"http://www.example.com/meetings/occurances/34534\"}}");
            statement.Id = voidedStatementId;

            var voidingStatement = new Statement()
            {
                Actor = new Agent() { Mbox = new Mbox("mailto:xapi@adlnet.gov") },
                Verb = new Verb() { Id = new Iri("http://adlnet.gov/expapi/verbs/voided") },
                Object = new StatementRef() { Id = voidedStatementId },
            };
            voidingStatement.Object.As<StatementRef>().Id = statement.Id.Value;

            // Act
            Guid statementId = await createStatementHandler.Handle(
                CreateStatementCommand.Create(statement),
                CancellationToken.None
            );

            // Create voiding statement
            Guid voidingStatementId = await createStatementHandler.Handle(
                CreateStatementCommand.Create(voidingStatement),
                CancellationToken.None
            );

            // Query voided statement
            StatementModel voidedStatement = await voidStatementQueryHandler.Handle(
                VoidedStatemetQuery.Create(voidedStatementId),
                CancellationToken.None
            );

            // Assert
            mediatorMock.Verify(m => m.Publish(It.IsAny<StatementCreated>(), It.IsAny<CancellationToken>()), Times.AtLeast(2));
            voidedStatement.ShouldNotBe(null);
            voidedStatement.VoidingStatementId.ShouldBe(voidingStatementId);
            voidedStatement.VoidedAt.ShouldNotBe(null);
        }
    }
}
