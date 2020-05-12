using AutoMapper;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.InteractionActivities;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.InteractionTypes;
using EntityInteractions = Doctrina.Domain.Entities.InteractionActivities;
using DataInteractions = Doctrina.ExperienceApi.Data.InteractionTypes;
namespace Application.Infrastructure.Automapper.Mappings
{
    public class ActivityMapper : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Activity, ActivityEntity>()
                .ForMember(e => e.ActivityId, opt => opt.Ignore())
                .ForMember(e => e.Id, opt => opt.MapFrom(x => x.Id.ToString()))
                .ForMember(e => e.Hash, opt => opt.MapFrom(x => x.Id.ComputeHash()))
                .ForMember(dest => dest.Definition, opt => opt.MapFrom(src => src.Definition))
                .ReverseMap();

            configuration.CreateMap<ActivityDefinition, ActivityDefinitionEntity>()
                .ForMember(x => x.ActivityDefinitionId, opt => opt.Ignore())
                .ForMember(x => x.Descriptions, opt => opt.MapFrom(a => a.Description))
                .ForMember(x => x.Extensions, opt => opt.MapFrom(a => a.Extensions))
                .ForMember(x => x.MoreInfo, opt => opt.MapFrom(a => a.MoreInfo))
                .ForMember(x => x.Names, opt => opt.MapFrom(a => a.Name))
                .ForMember(x => x.Type, opt => opt.MapFrom(a => a.Type))
                .ForMember(dest => dest.InteractionActivity, opt => opt.Ignore());

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
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
