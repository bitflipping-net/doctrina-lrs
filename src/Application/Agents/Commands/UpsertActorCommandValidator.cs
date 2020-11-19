using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Validation;
using FluentValidation;

namespace Doctrina.Application.Agents.Commands
{
    public class UpsertActorCommandValidator : AbstractValidator<UpsertActorCommand>
    {
        public UpsertActorCommandValidator()
        {
            RuleFor(x=> x.Actor).SetValidator(new AgentValidator()).When(x=> x.Actor.ObjectType == ObjectType.Agent);
            RuleFor(x=> x.Actor as Group).SetValidator(new GroupValidator()).When(x=> x.Actor is Group);
        }
    }
}
