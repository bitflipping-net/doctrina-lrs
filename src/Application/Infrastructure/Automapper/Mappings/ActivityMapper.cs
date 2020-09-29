using AutoMapper;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Domain.Models;
using Doctrina.Domain.Models.InteractionActivities;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.InteractionTypes;
using DataInteractions = Doctrina.ExperienceApi.Data.InteractionTypes;
using EntityInteractions = Doctrina.Domain.Models.InteractionActivities;

namespace Application.Infrastructure.Automapper.Mappings
{
    public class ActivityMapper : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Activity, ActivityModel>()
              .ForMember(e => e.ActivityId, opt => opt.Ignore())
              .ForMember(e => e.Iri, opt => opt.MapFrom(x => x.Id.ToString()))
              .ForMember(e => e.Hash, opt => opt.MapFrom(x => x.Id.ComputeHash()))
              .ForMember(dest => dest.Definition, opt => opt.MapFrom(src => src.Definition));

            configuration.CreateMap<ActivityModel, Activity>()
              .ForMember(e => e.Id, opt => opt.MapFrom(x => x.Iri))
              .ForMember(e => e.Definition, opt => opt.MapFrom(x => x.Definition))
              .ForMember(a => a.ObjectType, opt => opt.Ignore());

            configuration.CreateMap<ActivityDefinition, ActivityDefinitionEntity>()
              .ForMember(x => x.Descriptions, opt => opt.MapFrom(x => x.Description))
              .ForMember(x => x.Extensions, opt => opt.MapFrom(x => x.Extensions))
              .ForMember(x => x.MoreInfo, opt => opt.MapFrom(a => a.MoreInfo))
              .ForMember(x => x.Names, opt => opt.MapFrom(x => x.Name))
              .ForMember(x => x.Type, opt => opt.MapFrom(a => a.Type))
              .ForMember(dest => dest.InteractionActivity, opt => opt.Ignore())
              .ReverseMap()
              .ForMember(d => d.Description, opt => opt.MapFrom(x => x.Descriptions))
              .ForMember(d => d.Name, opt => opt.MapFrom(x => x.Names))
              .ForMember(d => d.Extensions, opt => opt.MapFrom(x => x.Extensions))
              .ForMember(d => d.MoreInfo, opt => opt.MapFrom(e => e.MoreInfo))
              .ForMember(d => d.Type, opt => opt.MapFrom(e => e.Type));

            configuration.CreateMap<ActivityDefinitionEntity, ActivityDefinitionEntity>();

            configuration.CreateMap<InteractionActivityDefinitionBase, InteractionActivityBase>()
              .ForMember(a => a.CorrectResponsesPattern, opt => opt.MapFrom(a => a.CorrectResponsesPattern))
              .ForMember(a => a.InteractionType, opt => opt.MapFrom(a => a.InteractionType.ToString()));

            configuration.CreateMap<Choice, ChoiceInteractionActivity>()
              .IncludeBase<InteractionActivityDefinitionBase, InteractionActivityBase>()
              .ForMember(dest => dest.Choices, opt => opt.MapFrom(src => src.Choices));

            configuration.CreateMap<FillIn, FillInInteractionActivity>()
              .IncludeBase<InteractionActivityDefinitionBase, InteractionActivityBase>();

            configuration.CreateMap<Likert, LikertInteractionActivity>()
              .IncludeBase<InteractionActivityDefinitionBase, InteractionActivityBase>()
              .ForMember(dest => dest.Scale, opt => opt.MapFrom(src => src.Scale));

            configuration.CreateMap<LongFillIn, LongFillInInteractionActivity>()
              .IncludeBase<InteractionActivityDefinitionBase, InteractionActivityBase>();

            configuration.CreateMap<Matching, MatchingInteractionActivity>()
              .IncludeBase<InteractionActivityDefinitionBase, InteractionActivityBase>()
              .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source));

            configuration.CreateMap<Numeric, NumericInteractionType>()
              .IncludeBase<InteractionActivityDefinitionBase, InteractionActivityBase>();

            configuration.CreateMap<Other, OtherInteractionActivity>()
              .IncludeBase<InteractionActivityDefinitionBase, InteractionActivityBase>();

            configuration.CreateMap<Performance, PerformanceInteractionActivity>()
              .IncludeBase<InteractionActivityDefinitionBase, InteractionActivityBase>()
              .ForMember(x => x.Steps, opt => opt.MapFrom(c => c.Steps));

            configuration.CreateMap<Sequencing, SequencingInteractionActivity>()
              .IncludeBase<InteractionActivityDefinitionBase, InteractionActivityBase>()
              .ForMember(c => c.Choices, opt => opt.MapFrom(src => src.Choices));

            configuration.CreateMap<TrueFalse, TrueFalseInteractionActivity>()
              .IncludeBase<InteractionActivityDefinitionBase, InteractionActivityBase>();

            configuration.CreateMap<DataInteractions.InteractionComponentCollection, EntityInteractions.InteractionComponentCollection>();

            configuration.CreateMap<DataInteractions.InteractionComponent, EntityInteractions.InteractionComponent>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(x => x.Description));
        }
    }
}
