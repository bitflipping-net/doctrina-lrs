using AutoMapper;
using Doctrina.Application.Infrastructure.Automapper.Mappings.TypeConverters;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Application.Mappings.ValueResolvers;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using Data = Doctrina.ExperienceApi.Data;
using System;

namespace Doctrina.Application.Mappings
{
    public class StatementMapping : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<StatementObjectBase, StatementObjectEntity>()
               .ForMember(ent => ent.ObjectType, opt => opt.MapFrom(x => (EntityObjectType)Enum.Parse(typeof(EntityObjectType), (string)x.ObjectType)))
               .ReverseMap();


            configuration.CreateMap<Agent, AgentEntity>()
                .ForMember(e => e.ObjectType, opt => opt.Ignore())
               .ForMember(ent => ent.Name, opt => opt.MapFrom(x => x.Name))
               .ForMember(ent => ent.Mbox, opt => opt.MapFrom(x => x.Mbox.ToString()))
               .ForMember(ent => ent.Mbox_SHA1SUM, opt => opt.MapFrom(x => x.Mbox_SHA1SUM))
               .ForMember(ent => ent.OpenId, opt => opt.MapFrom(x => x.OpenId.ToString()))
               .ForMember(ent => ent.Account, opt => opt.MapFrom(x => x.Account))
               .ReverseMap();

            configuration.CreateMap<Group, GroupEntity>()
               .IncludeBase<Agent, AgentEntity>()
               .ForMember(ent => ent.Members, opt => opt.MapFrom(x => x.Member))
               .ReverseMap();

            configuration.CreateMap<Data.Account, Domain.Entities.Account>()
                .ForMember(ent => ent.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(ent => ent.HomePage, opt => opt.MapFrom(x => x.HomePage.ToString()))
                .ReverseMap();

            configuration.CreateMap<Statement, StatementEntity>()
                // Statement base
                .ForMember(x => x.StatementId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Actor, opt => opt.MapFrom(x => x.Actor))
                .ForMember(x => x.Verb, opt => opt.MapFrom(x => x.Verb))
                .ForMember(x => x.Object, opt => opt.MapFrom<ObjectValueResolver, IStatementObject>(x => x.Object))
                .ForMember(x => x.Timestamp, opt => opt.MapFrom(x => x.Timestamp))
                .ForMember(x => x.Attachments, opt => opt.MapFrom(x => x.Attachments))
                // Statement only
                .ForMember(x => x.Result, opt => opt.MapFrom(x => x.Result))
                .ForMember(x => x.Context, opt => opt.MapFrom(x => x.Context))
                .ForMember(x => x.Authority, opt => opt.MapFrom(x => x.Authority))
                .ForMember(x => x.Stored, opt => opt.MapFrom(x => x.Stored))
                .ForMember(x => x.Version, opt => opt.MapFrom(x => x.Version))
                // Database specfic
                .ForMember(x => x.AuthorityId, opt => opt.Ignore())
                .ForMember(x => x.Voided, opt => opt.Ignore())
                .ForMember(x => x.FullStatement, opt => opt.Ignore())
                .ReverseMap()
                .ConvertUsing<StatementTypeConverter>();
                 // Statement base
                //.ForMember(x => x.Actor, opt => opt.MapFrom(x => x.Actor))
                //.ForMember(x => x.Verb, opt => opt.MapFrom(x => x.Verb))
                //.ForMember(x => x.Object, opt => opt.MapFrom<ObjectValueResolver, StatementObjectEntity>(x => x.Object))
                //.ForMember(x => x.Timestamp, opt => opt.MapFrom(x => x.Timestamp))
                // Statement only
                //.ForMember(x => x.Result, opt => opt.MapFrom(x => x.Result))
                //.ForMember(x => x.Context, opt => opt.MapFrom(x => x.Context))
                //.ForMember(x => x.Authority, opt => opt.MapFrom(x => x.Authority))
                //.ForMember(x => x.Stored, opt => opt.MapFrom(x => x.Stored))
                //.ForMember(x => x.Version, opt => opt.MapFrom(x => x.Version));

            configuration.CreateMap<SubStatement, SubStatementEntity>()
                .ForMember(e=> e.ObjectType, opt => opt.Ignore())
                .ForMember(x => x.Actor, opt => opt.MapFrom(x => x.Actor))
                .ForMember(x => x.Verb, opt => opt.MapFrom(x => x.Verb))
                .ForMember(x => x.Object, opt => opt.MapFrom<ObjectValueResolver, IStatementObject>(x => x.Object))
                .ForMember(x => x.Result, opt => opt.MapFrom(x => x.Result))
                .ForMember(x => x.Context, opt => opt.MapFrom(x => x.Context))
                .ForMember(x => x.Timestamp, opt => opt.MapFrom(x => x.Timestamp))
                .ForMember(x => x.Attachments, opt => opt.MapFrom(x => x.Attachments))
                .ReverseMap();

            configuration.CreateMap<StatementRef, StatementRefEntity>()
                .ForMember(e => e.ObjectType, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id));

            configuration.CreateMap<Verb, VerbEntity>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.ToString()))
                .ForMember(x => x.Hash, opt => opt.MapFrom(x => x.Id.ComputeHash()))
                .ForMember(x => x.Display, opt => opt.MapFrom<LanguageMapValueResolver, LanguageMap>(x => x.Display))
                .ReverseMap();

            configuration.CreateMap<Activity, ActivityEntity>()
                .ForMember(e => e.ObjectType, opt => opt.Ignore())
                .ForMember(e => e.Id, opt => opt.MapFrom(x => x.Id.ToString()))
                .ForMember(e => e.Hash, opt => opt.MapFrom(x => x.Id.ComputeHash()))
                .ForMember(entity => entity.Definition, opt => opt.MapFrom(x => x.Definition))
                //.ForMember(entity => entity.Definition, opt => opt.Ignore())
                .ReverseMap();

            configuration.CreateMap<ActivityDefinition, ActivityDefinitionEntity>()
                .ForMember(ent => ent.ActivityDefinitionId, opt => opt.Ignore())
                .ForMember(ent => ent.Type, opt => opt.MapFrom(x => x.Type.ToString()))
                .ForMember(ent => ent.Descriptions, opt => opt.MapFrom<LanguageMapValueResolver, LanguageMap>(x => x.Description))
                .ForMember(ent => ent.Extensions, opt => opt.MapFrom<ExtenstionsValueResolver, ExtensionsDictionary>(x => x.Extensions))
                .ForMember(ent => ent.MoreInfo, opt => opt.MapFrom(x => x.MoreInfo.ToString()))
                .ForMember(ent => ent.Names, opt => opt.MapFrom<LanguageMapValueResolver, LanguageMap>(src => src.Name))
                .ReverseMap();

            configuration.CreateMap<Result, ResultEntity>()
                .ForMember(x => x.ResultId, opt => opt.Ignore())
                .ForMember(x => x.Completion, opt => opt.MapFrom(x => x.Completion))
                .ForMember(x => x.Score, opt => opt.MapFrom(x => x.Score))
                .ForMember(x => x.Duration, opt => opt.Ignore()/*MapFrom(x => (string)x.Duration.ToString())*/)
                .ForMember(x => x.Response, opt => opt.MapFrom(x => x.Response))
                .ForMember(x => x.Success, opt => opt.MapFrom(x => x.Success))
                .ForMember(x => x.Extensions, opt => opt.MapFrom<ExtenstionsValueResolver, ExtensionsDictionary>(x => x.Extensions))
                .ReverseMap();

            configuration.CreateMap<Score, ScoreEntity>()
                .ReverseMap();

            configuration.CreateMap<Context, ContextEntity>()
                .ForMember(x => x.ContextId, opt => opt.Ignore())
                .ForMember(x => x.ContextActivities, opt => opt.MapFrom(x => x.ContextActivities))
                .ReverseMap();

            configuration.CreateMap<Attachment, AttachmentEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.ContentType, conf => conf.MapFrom(p => p.ContentType))
                .ForMember(x => x.FileUrl, conf => conf.MapFrom(p => p.FileUrl.ToString()))
                .ForMember(x => x.Length, opt => opt.MapFrom(p => p.Length))
                .ForMember(x => x.SHA2, opt => opt.MapFrom(p => p.SHA2))
                .ForMember(x => x.Display, opt => opt.MapFrom<LanguageMapValueResolver, LanguageMap>(p => p.Display))
                .ForMember(x => x.Description, opt => opt.MapFrom<LanguageMapValueResolver, LanguageMap>(p => p.Description))
                .ReverseMap()
                .ForMember(x=> x.Payload, opt => opt.MapFrom(x=> x.Payload));

            configuration.CreateMap<ContextActivities, ContextActivitiesEntity>()
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category))
                .ForMember(x => x.Parent, opt => opt.MapFrom(x => x.Parent))
                .ForMember(x => x.Grouping, opt => opt.MapFrom(x => x.Grouping))
                .ForMember(x => x.Other, opt => opt.MapFrom(x => x.Other));

            configuration.CreateMap<ActivityCollection, ContextActivityCollection>();
        }

    }


}
