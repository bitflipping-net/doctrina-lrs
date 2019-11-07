using AutoMapper;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data.Documents;

namespace Doctrina.Application.Mappings
{
    public class ActivityStateMappings : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ActivityStateEntity, ActivityStateDocument>()
                .ForMember(x => x.Activity, opt => opt.MapFrom(p => p.Activity))
                .ForMember(x => x.Agent, opt => opt.MapFrom(p => p.Agent))
                .ForMember(x => x.Registration, opt => opt.MapFrom(p => p.Registration))
                .ForMember(x => x.Content, opt => opt.MapFrom(p => p.Document.Content))
                .ForMember(x => x.ContentType, opt => opt.MapFrom(p => p.Document.ContentType))
                .ForMember(x => x.Tag, opt => opt.MapFrom(p => p.Document.Checksum))
                .ForMember(x => x.LastModified, opt => opt.MapFrom(p => p.Document.LastModified));
        }
    }
}
