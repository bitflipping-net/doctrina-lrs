using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Statements.Queries;
using Doctrina.Application.Tests.Infrastructure;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Doctrina.Application.Tests.Statements.Commands
{
    public class PagedStatementsQueryTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldReturn_StatementsResult_WithVerb()
        {
            // Arrange
            var distributedCacheMock = new Mock<IDistributedCache>();

            var handler = new PagedStatementsQueryHandler(_context, _mapper, distributedCacheMock.Object);
            var verb = new Iri("http://adlnet.gov/expapi/verbs/attended");
            var query = new PagedStatementsQuery()
            {
                VerbId = verb
            };

            // Act
            // var result = sut.Handle(new CreateStatementCommand { Statement = statement }, CancellationToken.None);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Statements.ShouldNotBeNull();
            result.Statements.ShouldAllBe(x => x.Verb.Equals(verb));
        }

        [Fact]
        public async Task ShouldReturn_StatementsResult_WithAgent()
        {
            // Arrange
            var distributedCacheMock = new Mock<IDistributedCache>();
            var agent = new Agent()
            {
                Mbox = new Mbox("mailto:1202f754-77e1-4e77-baa2-955b0c4ed7f6@adlnet.gov"),
                Name = "xAPI account"
            };
            var handler = new PagedStatementsQueryHandler(_context, _mapper, distributedCacheMock.Object);
            var query = new PagedStatementsQuery()
            {
                Agent = agent
            };

            // Act
            // var result = sut.Handle(new CreateStatementCommand { Statement = statement }, CancellationToken.None);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Statements.ShouldNotBeNull();
            result.Statements.ShouldAllBe(x => x.Actor.Equals(agent));
        }
    }
}
