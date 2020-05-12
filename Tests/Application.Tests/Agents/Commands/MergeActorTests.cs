using AutoMapper;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Tests.Infrastructure;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.Agents.Commands
{
    public class MergeActorTests : CommandTestBase
    {
        [Fact]
        public async Task AnonymousGroup()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var mapperMock = new Mock<IMapper>();
            var actor = new Group()
            {
                Member = new[]{
                    new Agent()
                    {
                        Account = new Doctrina.ExperienceApi.Data.Account(){
                            Name = "test-user",
                            HomePage = new Uri("https://bitflipping-net"),
                        }
                    },
                    new Agent()
                    {
                        Name = "Test Agent",
                        Mbox = new Mbox("mailto:test@doctrina.net")
                    }
                }
            };
            var handler = new UpsertActorCommand.Handler(_context, mediatorMock.Object, mapperMock.Object);
            var validator = new UpsertActorCommandValidator();
            var cmd = UpsertActorCommand.Create(actor);

            // Act
            var validationResult = validator.Validate(cmd);
            validationResult.IsValid.ShouldBeTrue();

            var result = await handler.Handle(cmd, CancellationToken.None);

            // Assert
            result.AgentId.ShouldNotBe(Guid.Empty);
            result.Hash.ShouldNotBeNullOrWhiteSpace();
            result.ShouldBeOfType<GroupEntity>();
        }
    }
}
