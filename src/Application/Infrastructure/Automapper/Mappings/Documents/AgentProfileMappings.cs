using AutoMapper;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Domain.Models.Documents;
using Doctrina.ExperienceApi.Data.Documents;

namespace Doctrina.Application.Infrastructure.Automapper.Mappings.Documents
{
    public class AgentProfileMappings : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<AgentProfileEntity, AgentProfileDocument>()
                .ForMember(x => x.Agent, opt => opt.MapFrom(p => p.Persona))
                .ForMember(x => x.Content, opt => opt.MapFrom(p => p.Content))
                .ForMember(x => x.ContentType, opt => opt.MapFrom(p => p.ContentType))
                .ForMember(x => x.Tag, opt => opt.MapFrom(p => p.Checksum))
                .ForMember(x => x.LastModified, opt => opt.MapFrom(p => p.UpdatedAt));
        }
    }
}
