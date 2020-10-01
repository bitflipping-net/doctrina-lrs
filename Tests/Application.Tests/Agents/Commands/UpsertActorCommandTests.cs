using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Agents.Queries;
using Doctrina.Application.Personas.Queries;
using Doctrina.Application.Tests.Infrastructure;
using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Moq;
using Shouldly;
using System;
using System.Threading;
using Xunit;

namespace Application.Tests.Agents.Commands
{
    public class UpsertActorCommandTests : CommandTestBase
    {
        [Fact]
        public async void ShouldQueryAgent()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var entityId = Guid.NewGuid();
            var agent = new Agent()
            {
                Mbox = new Mbox("mailto:doctrina@doctrina.com")
            };

            var handler = new UpsertActorCommandHandler(_storeContext, mediatorMock.Object, _mapper);

            // Act
            PersonaModel result = await handler.Handle(UpsertActorCommand.Create(agent), CancellationToken.None);

            // Assert
            result.ShouldNotBe(null);
            result.PersonaId.ShouldBe(entityId);
        }
    }
}
