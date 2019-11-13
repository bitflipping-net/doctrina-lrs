using AutoMapper;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Common.Interfaces;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.Agents.Commands
{
    public class MergeActorTests
    {
        [Fact]
        public void AnonymousGroup()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var mapperMock = new Mock<IMapper>();
            var authorityMock = new Mock<IAuthorityContext>();

            //var handler = new MergeActorCommand.Handler(_context, mediatorMock.Object, mapperMock.Object, authorityMock.Object);
            var newStatementId = Guid.Parse("637E9E80-4B8D-4640-AC13-615C3E413568");

            var statement = new Group()
            {
                Account = new Account()
                {
                    HomePage = new Uri("https://twitter.com"),
                    Name = "bitflipping-net"
                }
            };

            // Act
            // var result = sut.Handle(new CreateStatementCommand { Statement = statement }, CancellationToken.None);

            // Assert
            //mediatorMock.Verify(m => m.Publish(It.Is<StatementCreated>(cc => cc.StatementId == newStatementId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
