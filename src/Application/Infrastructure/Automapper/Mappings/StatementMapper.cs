
using AutoMapper;
using Doctrina.Application.Infrastructure.Automapper.Mappings.TypeConverters;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.ValueObjects;
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
                .ForMember(x => x.IRI, opt => opt.MapFrom(x => x.Id.ToString()))
                .ForMember(x => x.Hash, opt => opt.MapFrom(x => x.Id.ComputeHash()))
                .ForMember(x => x.Display, opt => opt.MapFrom(x => x.Display))
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.IRI))
                .ForMember(x => x.Display, opt => opt.MapFrom(x => x.Display));

            configuration.CreateMap<Statement, StatementEntity>()
                // Statement base
                .ForMember(x => x.StatementId, opt => opt.MapFrom(x => x.Id.GetValueOrDefault()))
                .ForMember(x => x.Actor, opt => opt.MapFrom(x => x.Actor))
                .ForMember(x => x.Verb, opt => opt.MapFrom(x => x.Verb))
                .ForMember(x => x.Object, opt => opt.MapFrom(x => x.Object.ToJson()))
                .ForMember(x => x.Timestamp, opt => opt.MapFrom(x => x.Timestamp))
                .ForMember(x => x.Attachments, opt => opt.MapFrom(x => x.Attachments))
                // Statement only
                .ForMember(x => x.Result, opt => opt.MapFrom(x => x.Result.ToJson()))
                .ForMember(x => x.Context, opt => opt.MapFrom(x => x.Context.ToJson()))
                .ForMember(x => x.Authority, opt => opt.MapFrom(x => x.Authority.ToJson()))
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(x => x.Stored))
                .ForMember(x => x.Version, opt => opt.MapFrom(x => x.Version))
                // Database specfic
                .ForMember(x => x.VoidingStatementId, opt => opt.Ignore())
                .ForMember(x => x.VoidingStatement, opt => opt.Ignore())
                .ReverseMap();

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

            configuration.CreateMap<ContextActivities, ICollection<ContextActivity>>()
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
