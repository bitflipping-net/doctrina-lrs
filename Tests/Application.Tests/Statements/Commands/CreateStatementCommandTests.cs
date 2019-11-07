using AutoMapper;
using Doctrina.Application.Statements.Commands;
using Doctrina.Application.Tests.Infrastructure;
using Doctrina.ExperienceApi;
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
        public void Handle_GivenValidRequest_ShouldRaiseCustomerCreatedNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var mapperMock = new Mock<IMapper>();
            var sut = new CreateStatementCommand.Handler(_context, mediatorMock.Object, mapperMock.Object);
            var newStatementId = Guid.Parse("637E9E80-4B8D-4640-AC13-615C3E413568");

            var statement = new Statement("{\"actor\":{\"objectType\":\"Agent\",\"name\":\"xAPI mbox\",\"mbox\":\"mailto:xapi@adlnet.gov\"},\"verb\":{\"id\":\"http://adlnet.gov/expapi/verbs/attended\",\"display\":{\"en-GB\":\"attended\",\"en-US\":\"attended\"}},\"object\":{\"objectType\":\"Activity\",\"id\":\"http://www.example.com/meetings/occurances/34534\"}}");
            statement.Id = newStatementId;
            // Act
            var result = sut.Handle(new CreateStatementCommand { Statement = statement }, CancellationToken.None);

            // Assert
            //mediatorMock.Verify(m => m.Publish(It.Is<StatementCreated>(cc => cc.StatementId == newStatementId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
