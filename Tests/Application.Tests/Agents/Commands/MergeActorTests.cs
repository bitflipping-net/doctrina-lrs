using AutoMapper;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Tests.Infrastructure;
using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Moq;
using Shouldly;
using System;
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
            var actor = new Doctrina.ExperienceApi.Data.Group()
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
            var handler = new UpsertActorCommandHandler(_storeContext, mediatorMock.Object, mapperMock.Object);
            var validator = new UpsertActorCommandValidator();
            var cmd = UpsertActorCommand.Create(actor);

            // Act
            var validationResult = validator.Validate(cmd);
            validationResult.IsValid.ShouldBeTrue();

            var result = await handler.Handle(cmd, CancellationToken.None);

            // Assert
            result.PersonaId.ShouldNotBe(Guid.Empty);
            result.ShouldBeOfType<Doctrina.Domain.Models.PersonaGroup>();
        }
    }
}
