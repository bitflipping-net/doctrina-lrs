
using AutoMapper;
using Doctrina.Application.Infrastructure.Automapper.Mappings.TypeConverters;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Application.Mappings.ValueResolvers;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.OwnedTypes;
using Doctrina.ExperienceApi.Data;
using System;
using System.Collections.Generic;
using Data = Doctrina.ExperienceApi.Data;

namespace Doctrina.Application.Mappings
{
    public class StatementMapping : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<IStatementObject, StatementObjectEntity>()
               .ForMember(ent => ent.ObjectType, opt => opt.MapFrom(x => (EntityObjectType)Enum.Parse(typeof(EntityObjectType), (string)x.ObjectType)))
               .ForMember(ent => ent.Activity, opt =>
               {
                   opt.PreCondition(c => c.ObjectType == ObjectType.Activity);
                   opt.MapFrom(c => (Activity)c);
               })
               .ForMember(ent => ent.Agent, opt =>
               {
                   opt.PreCondition(c => c.ObjectType == ObjectType.Agent);
                   opt.MapFrom(c => (Agent)c);
               })
               .ForMember(ent => ent.Agent, opt =>
               {
                   opt.PreCondition(c => c.ObjectType == ObjectType.Group);
                   opt.MapFrom(c => (Group)c);
               })
               .ForMember(ent => ent.StatementRef, opt =>
               {
                   opt.PreCondition(c => c.ObjectType == ObjectType.StatementRef);
                   opt.MapFrom(c => (StatementRef)c);
               })
               .ForMember(ent => ent.SubStatement, opt =>
               {
                   opt.PreCondition(c => c.ObjectType == ObjectType.SubStatement);
                   opt.MapFrom(c => (SubStatement)c);
               })
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
                .ForMember(x => x.Object, opt => opt.MapFrom(x => x.Object))
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
                .ForMember(x => x.FullStatement, opt => opt.Ignore());

            configuration.CreateMap<StatementEntity, Statement>()
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
                .ForMember(x => x.Actor, opt => opt.MapFrom(x => x.Actor))
                .ForMember(x => x.Verb, opt => opt.MapFrom(x => x.Verb))
                .ForMember(x => x.Object, opt => opt.MapFrom<ObjectValueResolver, IStatementObject>(x => x.Object))
                .ForMember(x => x.Result, opt => opt.MapFrom(x => x.Result))
                .ForMember(x => x.Context, opt => opt.MapFrom(x => x.Context))
                .ForMember(x => x.Timestamp, opt => opt.MapFrom(x => x.Timestamp))
                .ForMember(x => x.Attachments, opt => opt.MapFrom(x => x.Attachments))
                .ReverseMap();

            configuration.CreateMap<StatementRef, StatementRefEntity>()
                .ForMember(x => x.StatementRefId, opt => opt.Ignore())
                .ForMember(x => x.StatementId, opt => opt.MapFrom(x => x.Id));

            configuration.CreateMap<Result, ResultEntity>()
                .ForMember(x => x.ResultId, opt => opt.Ignore())
                .ForMember(x => x.Completion, opt => opt.MapFrom(x => x.Completion))
                .ForMember(x => x.Score, opt => opt.MapFrom(x => x.Score))
                .ForMember(x => x.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(x => x.Response, opt => opt.MapFrom(x => x.Response))
                .ForMember(x => x.Success, opt => opt.MapFrom(x => x.Success))
                .ForMember(x => x.Extensions, opt => opt.MapFrom(x => x.Extensions))
                .ReverseMap();

            configuration.CreateMap<Score, ScoreEntity>()
                .ForMember(dest => dest.Max, opt => opt.MapFrom(src => src.Max))
                .ForMember(dest => dest.Min, opt => opt.MapFrom(src => src.Min))
                .ForMember(dest => dest.Raw, opt => opt.MapFrom(src => src.Raw))
                .ForMember(dest => dest.Scaled, opt => opt.MapFrom(src => src.Scaled))
                .ReverseMap();

            configuration.CreateMap<Context, ContextEntity>()
                .ForMember(x => x.ContextId, opt => opt.Ignore())
                .ForMember(x => x.ContextActivities, opt => opt.MapFrom(x => x.ContextActivities))
                .ForMember(ent => ent.Instructor, opt => opt.MapFrom(x => x.Instructor))
                .ForMember(ent => ent.Language, opt => opt.MapFrom(x => x.Language))
                .ForMember(ent => ent.Revision, opt => opt.MapFrom(x => x.Revision))
                .ForMember(ent => ent.Platform, opt => opt.MapFrom(x => x.Platform))
                .ForMember(dest => dest.Registration, opt => opt.MapFrom(src => src.Registration))
                .ForMember(ent => ent.Team, opt => opt.MapFrom(x => x.Team))
                .ForMember(ent => ent.Extensions, opt => opt.MapFrom(x => x.Extensions))
                .ReverseMap();

            configuration.CreateMap<Attachment, AttachmentEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.ContentType, conf => conf.MapFrom(p => p.ContentType))
                .ForMember(x => x.FileUrl, conf => conf.MapFrom(p => p.FileUrl.ToString()))
                .ForMember(x => x.Length, opt => opt.MapFrom(p => p.Length))
                .ForMember(x => x.SHA2, opt => opt.MapFrom(p => p.SHA2))
                .ForMember(x => x.Display, opt => opt.MapFrom(p => p.Display))
                .ForMember(x => x.Description, opt => opt.MapFrom(p => p.Description))
                .ReverseMap()
                .ForMember(x => x.Payload, opt => opt.MapFrom(x => x.Payload));

            configuration.CreateMap<ContextActivities, ContextActivitiesEntity>()
                .ForMember(x => x.ContextActivitiesId, opt => opt.Ignore())
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category))
                .ForMember(x => x.Parent, opt => opt.MapFrom(x => x.Parent))
                .ForMember(x => x.Grouping, opt => opt.MapFrom(x => x.Grouping))
                .ForMember(x => x.Other, opt => opt.MapFrom(x => x.Other));

            configuration.CreateMap<ActivityCollection, HashSet<ContextActivityEntity>>();

            configuration.CreateMap<Activity, ContextActivityEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Hash, opt => opt.MapFrom(x => x.Id.ComputeHash()))
                .ForMember(x => x.ActivityId, opt => opt.MapFrom(x => x.Id));

            configuration.CreateMap<LanguageMap, LanguageMapCollection>();

            configuration.CreateMap<ExtensionsDictionary, ExtensionsCollection>();
        }
    }
}
