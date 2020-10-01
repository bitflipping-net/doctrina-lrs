using Doctrina.Application.Tests.Infrastructure;
using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using Shouldly;
using Xunit;

namespace Doctrina.Application.Tests.Statements.Commands
{
    public class StatementMappingTests : CommandTestBase
    {
        [Fact]
        public void Verb_Should_Map()
        {
            var verb = new Verb()
            {
                Id = new Iri("https://google.com")
            };
            var entity = _mapper.Map<VerbModel>(verb);
            entity.ShouldNotBeNull();
            entity.Id.ShouldNotBeNull();

            var backverb = _mapper.Map<Verb>(entity);
            backverb.ShouldNotBeNull();
            backverb.Id.ShouldNotBeNull();
            backverb.Equals(verb).ShouldBe(true);
        }

        [Fact]
        public void Statement_Should_Map()
        {
            var statement = new Statement()
            {
                Verb = new Verb()
                {
                    Id = new Iri("https://google.com")
                }
            };
            var entity = _mapper.Map<StatementModel>(statement);
            entity.ShouldNotBeNull();

            var back = _mapper.Map<Statement>(entity);
            back.ShouldNotBeNull();
        }

        [Fact]
        public void ShouldReturn_StatementsResult_WithAgent()
        {
            var entity = new StatementModel()
            {
                Verb = new VerbModel()
                {
                    Id = "https://google.com"
                }
            };
            var iri = Iri.Parse(entity.Verb.Id);
            var statement = new Statement();
            _mapper.Map(entity, statement);
            statement.ShouldNotBeNull();
            statement.Verb.ShouldNotBeNull();
            statement.Verb.Id.ShouldNotBeNull();
        }
    }
}
