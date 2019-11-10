using FluentValidation;

namespace Doctrina.Application.Agents.Commands
{
    public class MergeActorCommandValidator : AbstractValidator<MergeActorCommand>
    {
        public MergeActorCommandValidator()
        {
            //RuleFor(x => x.Actor).Must(actor => !actor.IsAnonymous() && actor.GetIdentifiers().Count > 1)
            //    .WithMessage($"An Identified Group/Agent does not allow for multiple identifiers.");

            //RuleFor(x => x.Actor.ObjectType)
            //    .Must(x => x == xAPI.ObjectType.Agent || x == xAPI.ObjectType.Group)
            //    .WithMessage(x=> $"Cannot create Agent/Group with objectType '{x.ObjectType}'");

            //When(x => x.ObjectType == xAPI.ObjectType.Group, 
            //    RuleFor(x=> (Group)x))
            //    .Otherwise();
            //"An Anonymous Group MUST NOT contain Group Objects in the 'member' identifiers."

            //RuleForEach(x => x.Member).NotEqual(x => x.ObjectType == ObjectType.Group
            //    .When;

            //RuleFor(x=> x.ObjectType)
            //    .When
        }
    }
}
