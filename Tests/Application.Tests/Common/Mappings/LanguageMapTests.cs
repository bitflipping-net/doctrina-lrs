using Doctrina.Application.Tests.Infrastructure;
using Doctrina.Domain.Models.OwnedTypes;
using Doctrina.ExperienceApi.Data;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Application.Tests.Infrastructure.Mappings
{
    public class LanguageMapTests : CommandTestBase
    {
        [Fact]
        public void LanguageMapMapping()
        {
            var languageMap = new LanguageMap();
            languageMap.Add("en-US", "some american description");
            languageMap.Add("en-GB", "some british description");

            var collection = _mapper.Map<LanguageMapCollection>(languageMap);

            collection.ShouldContainKey("en-US");
            collection.ShouldContainKey("en-GB");
        }

        [Fact]
        public void DictionaryTest()
        {
            var source = new Dictionary<string, string>()
            {
                { "en-GB", "british description" },
                { "da-DK", "dansk beskrivelse" }
            };
            var destination = new Dictionary<string, string>();
            _mapper.Map(source, destination);

            destination.Count.ShouldBe(2);
            destination.ShouldContainKeyAndValue("en-GB", "british description");
            destination.ShouldContainKeyAndValue("da-DK", "dansk beskrivelse");
        }

        [Fact]
        public void LanguageMapInstanceMapping()
        {
            var source = new LanguageMap()
            {
                { "en-GB", "british description" },
                { "da-DK", "dansk beskrivelse" }
            };
            var destination = new LanguageMapCollection();
            _mapper.Map(source, destination);

            destination.Count.ShouldBe(2);
            destination.ShouldContainKeyAndValue("en-GB", "british description");
            destination.ShouldContainKeyAndValue("da-DK", "dansk beskrivelse");
        }
    }
}
