using Doctrina.Application.ActivityStates.Commands;
using Doctrina.Application.Tests.Infrastructure;
using Doctrina.Domain.Models.Documents;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Json;
using Newtonsoft.Json.Linq;
using Shouldly;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.ActivityStates.Commands
{
    public class UpdateStateDocumentHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task State_Document_Merge()
        {
            // Arrange
            // var mediatorMock = new Mock<IMediator>();

            string stateId = "cc6605d6-b12f-41d4-bd76-6a5a2fc5f13b";
            var activityEntity = _context.Activities.FirstOrDefault();
            var agentEntity = _context.Personas.FirstOrDefault();
            string contentType = "application/json";

            string strBody1 = "{\"car\":\"Honda\"}";
            // var honda = new CreateStateDocumentCommand()
            // {
            //     StateId = stateId,
            //     ActivityId = new Iri(activityEntity.Id),
            //     Agent = agent,
            //     Content = Encoding.UTF8.GetBytes(strBody1),
            //     ContentType = contentType,
            //     Registration = Guid.Empty
            // };
            // var createHandler = new CreateStateDocumentHandler(_context, _mapper, mediatorMock.Object);

            // mediatorMock.Setup(m => m.Send(It.IsAny<UpsertActivityCommand>(), It.IsAny<CancellationToken>()))
            //     .Returns(Task.FromResult((IActivityEntity)activityEntity));

            // mediatorMock.Setup(m => m.Send(It.IsAny<UpsertActorCommand>(), It.IsAny<CancellationToken>()))
            //     .Returns(Task.FromResult((IAgentEntity)_mapper.Map<AgentEntity>(agent)));

            // var stateDocument1 = await createHandler.Handle(honda, CancellationToken.None);

            // stateDocument1.ContentType.ShouldBe(honda.ContentType);
            // stateDocument1.Content.ShouldBe(honda.Content);

            _context.ActivityStates.Add(new ActivityStateEntity(Encoding.UTF8.GetBytes(strBody1), contentType)
            {
                StateId = stateId,
                Activity = activityEntity,
                Persona = agentEntity
            });
            await _context.SaveChangesAsync();

            string strBody2 = "{\"type\":\"Civic\"}";
            var civic = new UpdateStateDocumentCommand()
            {
                StateId = stateId,
                ActivityId = new Iri(activityEntity.Iri),
                Persona = agentEntity,
                Content = Encoding.UTF8.GetBytes(strBody2),
                ContentType = contentType,
                Registration = Guid.Empty
            };
            var updateHandler = new UpdateStateDocumentHandler(_storeContext, _mapper);
            var stateDocument2 = await updateHandler.Handle(civic, CancellationToken.None);
            JsonString strBodyReturned = Encoding.UTF8.GetString(stateDocument2.Content);
            strBodyReturned.IsValid().ShouldBe(true);
            var jobj = strBodyReturned.Deserialize<JObject>();
            jobj.ShouldContainKeyAndValue("type", "Civic");
            jobj.ShouldContainKeyAndValue("car", "Honda");
        }
    }
}
