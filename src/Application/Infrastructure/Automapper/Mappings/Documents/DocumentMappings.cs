using System.Text;
using AutoMapper;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data.Documents;

namespace Doctrina.Application.Infrastructure.Automapper.Mappings.Documents
{
    public class DocumentMappings : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ActivityProfileEntity, IDocument>().As<Document>();
            configuration.CreateMap<ActivityProfileEntity, Document>()
                .ForMember(x => x.Content, opt => opt.MapFrom(p => p.Content))
                .ForMember(x => x.ContentType, opt => opt.MapFrom(p => p.ContentType))
                .ForMember(x => x.Tag, opt => opt.MapFrom(p => p.GetChecksumString()))
                .ForMember(x => x.LastModified, opt => opt.MapFrom(p => p.UpdatedAt));

            configuration.CreateMap<ActivityStateEntity, IDocument>().As<Document>();
            configuration.CreateMap<ActivityStateEntity, Document>()
                .ForMember(x => x.Content, opt => opt.MapFrom(p => p.Content))
                .ForMember(x => x.ContentType, opt => opt.MapFrom(p => p.ContentType))
                .ForMember(x => x.Tag, opt => opt.MapFrom(p => p.GetChecksumString()))
                .ForMember(x => x.LastModified, opt => opt.MapFrom(p => p.UpdatedAt));

            configuration.CreateMap<AgentProfileEntity, IDocument>().As<Document>();
            configuration.CreateMap<AgentProfileEntity, Document>()
                .ForMember(x => x.Content, opt => opt.MapFrom(p => p.Content))
                .ForMember(x => x.ContentType, opt => opt.MapFrom(p => p.ContentType))
                .ForMember(x => x.Tag, opt => opt.MapFrom(p => p.GetChecksumString()))
                .ForMember(x => x.LastModified, opt => opt.MapFrom(p => p.UpdatedAt));
        }
    }
}
