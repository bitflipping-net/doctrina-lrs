using Doctrina.Application.Activities.Commands;
using Doctrina.Application.Tests.Infrastructure;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.Activities.Commands
{
    public class UpsertActivityTests : CommandTestBase
    {
        [Fact]
        public async Task DeepEqual()
        {
            var mediatorMock = new Mock<IMediator>();
            var activity1 = new Activity(@"{
                ""objectType"": ""Activity"",
                ""id"": ""http://www.example.com/verify/complete/34534"",
                ""definition"": {
                    ""type"": ""http://adlnet.gov/expapi/activities/meeting"",
                    ""name"": {
                        ""en-GB"": ""example meeting""
                    },
                    ""description"": {
                        ""en-GB"": ""An example meeting that happened on a specific occasion with certain people present.""
                    },
                    ""moreInfo"": ""http://virtualmeeting.example.com/345256"",
                    ""extensions"": {
                        ""http://example.com/profiles/meetings/extension/location"": ""X:\\\\meetings\\\\minutes\\\\examplemeeting.one"",
                        ""http://example.com/profiles/meetings/extension/reporter"": {
                            ""name"": ""Thomas"",
                            ""id"": ""http://openid.com/342""
                        }
                    }
                }
            }");
            var activity2 = new Activity(@"{
                ""objectType"": ""Activity"",
                ""id"": ""http://www.example.com/verify/complete/34534"",
                ""definition"": {
                    ""type"": ""http://adlnet.gov/expapi/activities/meeting"",
                    ""name"": {
                        ""en-US"": ""example meeting""
                    },
                    ""description"": {
                        ""en-US"": ""An example meeting that happened on a specific occasion with certain people present.""
                    },
                    ""moreInfo"": ""http://virtualmeeting.example.com/345256"",
                    ""extensions"": {
                        ""http://example.com/profiles/meetings/extension/location"": ""X:\\\\meetings\\\\minutes\\\\examplemeeting.one"",
                        ""http://example.com/profiles/meetings/extension/reporter"": {
                            ""name"": ""Thomas"",
                            ""id"": ""http://openid.com/342""
                        }
                    }
                }
            }");

            var upsertHandler = new UpsertActivityCommandHandler(_context, _mapper);

            var entity1 = await upsertHandler.Handle(UpsertActivityCommand.Create(activity1), CancellationToken.None);
            await _context.SaveChangesAsync();
            var entity2 = await upsertHandler.Handle(UpsertActivityCommand.Create(activity2), CancellationToken.None);

            entity2.Id.ShouldBe("http://www.example.com/verify/complete/34534");
            entity2.Definition.ShouldNotBeNull();

            entity2.Definition.Names.Count.ShouldBe(2);
            entity2.Definition.Names.ShouldContainKey("en-US");
            entity2.Definition.Names.ShouldContainKey("en-GB");

            entity2.Definition.Descriptions.Count.ShouldBe(2);
            entity2.Definition.Descriptions.ShouldContainKey("en-US");
            entity2.Definition.Descriptions.ShouldContainKey("en-GB");
        }
    }
}
