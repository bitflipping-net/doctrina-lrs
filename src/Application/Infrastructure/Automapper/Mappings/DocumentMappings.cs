using AutoMapper;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Domain.Models.Documents;
using Doctrina.ExperienceApi.Data.Documents;

namespace Doctrina.Application.Mappings
{
    public class DocumentMappings : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<DocumentModel, Document>()
                .ForMember(x => x.Content, opt => opt.MapFrom(p => p.Content))
                .ForMember(x => x.ContentType, opt => opt.MapFrom(p => p.ContentType))
                .ForMember(x => x.Tag, opt => opt.MapFrom(p => p.Checksum))
                .ForMember(x => x.LastModified, opt => opt.MapFrom(p => p.UpdatedAt));
        }
    }
}
