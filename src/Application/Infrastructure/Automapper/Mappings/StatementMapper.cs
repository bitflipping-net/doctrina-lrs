
using AutoMapper;
using Doctrina.Application.Infrastructure.Automapper.Mappings.TypeConverters;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.OwnedTypes;
using Doctrina.ExperienceApi.Data;
using System.Collections.Generic;
using Data = Doctrina.ExperienceApi.Data;

namespace Doctrina.Application.Mappings
{
    public class StatementMapping : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<string, Iri>()
               .ConvertUsing<Infrastructure.Automapper.Mappings.TypeConverters.IriTypeConverter>();

            configuration.CreateMap<Verb, VerbEntity>()
                .ForMember(x => x.VerbId, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.ToString()))
                .ForMember(x => x.Hash, opt => opt.MapFrom(x => x.Id.ComputeHash()))
                .ForMember(x => x.Display, opt => opt.MapFrom(x => x.Display))
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Display, opt => opt.MapFrom(x => x.Display));

            configuration.CreateMap<Statement, StatementEntity>()
                // Statement base
                .ForMember(x => x.StatementId, opt => opt.MapFrom(x => x.Id.GetValueOrDefault()))
                .ForMember(x => x.Actor, opt => opt.MapFrom(x => x.Actor))
                .ForMember(x => x.Verb, opt => opt.MapFrom(x => x.Verb))
                .ForMember(x => x.ObjectId, opt => opt.Ignore())
                .ForMember(x => x.ObjectType, opt => opt.Ignore())
                .ForMember(x => x.Timestamp, opt => opt.MapFrom(x => x.Timestamp))
                .ForMember(x => x.Attachments, opt => opt.MapFrom(x => x.Attachments))
                // Statement only
                .ForMember(x => x.Result, opt => opt.MapFrom(x => x.Result))
                .ForMember(x => x.Context, opt => opt.MapFrom(x => x.Context))
                .ForMember(x => x.Client, opt => opt.MapFrom(x => x.Authority))
                .ForMember(x => x.Stored, opt => opt.MapFrom(x => x.Stored))
                .ForMember(x => x.Version, opt => opt.MapFrom(x => x.Version))
                // Database specfic
                .ForMember(x => x.ActorId, opt => opt.Ignore())
                .ForMember(x => x.VerbId, opt => opt.Ignore())
                .ForMember(x => x.ClientId, opt => opt.Ignore())
                .ForMember(x => x.VoidingStatementId, opt => opt.Ignore())
                .ForMember(x => x.VoidingStatement, opt => opt.Ignore())
                .ForMember(x => x.FullStatement, opt => opt.Ignore())
                .ReverseMap()
                .ConvertUsing<StatementTypeConverter>();

            configuration.CreateMap<SubStatement, SubStatementEntity>()
                .ForMember(x => x.Actor, opt => opt.MapFrom(x => x.Actor))
                .ForMember(x => x.Verb, opt => opt.MapFrom(x => x.Verb))
                .ForMember(x => x.ObjectId, opt => opt.Ignore())
                .ForMember(x => x.ObjectType, opt => opt.Ignore())
                .ForMember(x => x.Result, opt => opt.MapFrom(x => x.Result))
                .ForMember(x => x.Context, opt => opt.MapFrom(x => x.Context))
                .ForMember(x => x.Timestamp, opt => opt.MapFrom(x => x.Timestamp))
                .ForMember(x => x.Attachments, opt => opt.MapFrom(x => x.Attachments))
                .ReverseMap()
                .ForMember(x => x.Actor, opt => opt.MapFrom(x => x.Actor))
                .ForMember(x => x.Attachments, opt => opt.MapFrom(x => x.Attachments))
                .ForMember(x => x.Verb, opt => opt.MapFrom(x => x.Verb))
                .ForMember(x => x.Result, opt => opt.MapFrom(x => x.Result))
                .ForMember(x => x.Timestamp, opt => opt.MapFrom(x => x.Timestamp))
                .ForMember(x => x.Object, opt => opt.MapFrom(x => x.Object))
                .ForMember(x => x.Context, opt => opt.MapFrom(x => x.Context));

            configuration.CreateMap<StatementRef, StatementRefEntity>()
                .ForMember(x => x.StatementRefId, opt => opt.Ignore())
                .ForMember(x => x.StatementId, opt => opt.MapFrom(x => x.Id))
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.StatementId))
                .ForMember(x => x.ObjectType, opt => opt.Ignore());

            configuration.CreateMap<Result, ResultEntity>()
                .ForMember(x => x.ResultId, opt => opt.Ignore())
                .ForMember(x => x.Completion, opt => opt.MapFrom(x => x.Completion))
                .ForMember(x => x.Score, opt => opt.MapFrom(x => x.Score))
                .ForMember(x => x.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(x => x.Response, opt => opt.MapFrom(x => x.Response))
                .ForMember(x => x.Success, opt => opt.MapFrom(x => x.Success))
                .ForMember(x => x.Extensions, opt => opt.MapFrom(x => x.Extensions))
                .ReverseMap()
                .ForMember(x => x.Completion, opt => opt.MapFrom(x => x.Completion))
                .ForMember(x => x.Score, opt => opt.MapFrom(x => x.Score))
                .ForMember(x => x.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(x => x.Response, opt => opt.MapFrom(x => x.Response))
                .ForMember(x => x.Success, opt => opt.MapFrom(x => x.Success))
                .ForMember(x => x.Extensions, opt => opt.MapFrom(x => x.Extensions));

            configuration.CreateMap<Score, ScoreEntity>()
                .ForMember(dest => dest.Max, opt => opt.MapFrom(src => src.Max))
                .ForMember(dest => dest.Min, opt => opt.MapFrom(src => src.Min))
                .ForMember(dest => dest.Raw, opt => opt.MapFrom(src => src.Raw))
                .ForMember(dest => dest.Scaled, opt => opt.MapFrom(src => src.Scaled))
                .ReverseMap()
                .ForMember(dest => dest.Max, opt => opt.MapFrom(src => src.Max))
                .ForMember(dest => dest.Min, opt => opt.MapFrom(src => src.Min))
                .ForMember(dest => dest.Raw, opt => opt.MapFrom(src => src.Raw))
                .ForMember(dest => dest.Scaled, opt => opt.MapFrom(src => src.Scaled));

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
                .ReverseMap()
                .ForMember(x => x.ContextActivities, opt => opt.MapFrom(x => x.ContextActivities))
                .ForMember(ent => ent.Instructor, opt => opt.MapFrom(x => x.Instructor))
                .ForMember(ent => ent.Language, opt => opt.MapFrom(x => x.Language))
                .ForMember(ent => ent.Revision, opt => opt.MapFrom(x => x.Revision))
                .ForMember(ent => ent.Platform, opt => opt.MapFrom(x => x.Platform))
                .ForMember(dest => dest.Registration, opt => opt.MapFrom(src => src.Registration))
                .ForMember(ent => ent.Team, opt => opt.MapFrom(x => x.Team))
                .ForMember(ent => ent.Extensions, opt => opt.MapFrom(x => x.Extensions));

            configuration.CreateMap<Attachment, AttachmentEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.ContentType, conf => conf.MapFrom(p => p.ContentType))
                .ForMember(x => x.FileUrl, conf => conf.MapFrom(p => p.FileUrl.ToString()))
                .ForMember(x => x.Length, opt => opt.MapFrom(p => p.Length))
                .ForMember(x => x.SHA2, opt => opt.MapFrom(p => p.SHA2))
                .ForMember(x => x.Display, opt => opt.MapFrom(p => p.Display))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ReverseMap()
                .ForMember(x => x.Payload, opt => opt.MapFrom(x => x.Payload))
                .ForMember(x => x.ContentType, conf => conf.MapFrom(p => p.ContentType))
                .ForMember(x => x.FileUrl, conf => conf.MapFrom(p => p.FileUrl.ToString()))
                .ForMember(x => x.Length, opt => opt.MapFrom(p => p.Length))
                .ForMember(x => x.SHA2, opt => opt.MapFrom(p => p.SHA2))
                .ForMember(x => x.Display, opt => opt.MapFrom(p => p.Display))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description));

            configuration.CreateMap<ContextActivities, ICollection<ContextActivityEntity>>()
                .ConvertUsing<ContextActivityConverter>();

            configuration.CreateMap<LanguageMap, LanguageMapCollection>()
                .ConvertUsing<LanuageMapTypeConverter>();

            configuration.CreateMap<LanguageMapCollection, LanguageMap>()
                .ConvertUsing<LanuageMapTypeConverter>();

            configuration.CreateMap<LanguageMapCollection, LanguageMapCollection>()
                .ConvertUsing<LanuageMapTypeConverter>();

            configuration.CreateMap<ExtensionsDictionary, ExtensionsCollection>()
                .ConvertUsing<ExtensionsTypeConverter>();

            configuration.CreateMap<ExtensionsCollection, ExtensionsDictionary>()
                .ConvertUsing<ExtensionsTypeConverter>();
        }
    }
}
