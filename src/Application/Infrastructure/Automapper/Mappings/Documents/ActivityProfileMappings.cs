using AutoMapper;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data.Documents;

namespace Doctrina.Application.Infrastructure.Automapper.Mappings.Documents
{
    public class ActivityProfileMappings : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ActivityProfileEntity, ActivityProfileDocument>()
                .ForMember(x => x.ActivityId, opt => opt.MapFrom(p => p.Activity.Id))
                .ForMember(x => x.Registration, opt => opt.MapFrom(p => p.RegistrationId))
                .ForMember(x => x.Content, opt => opt.MapFrom(p => p.Document.Content))
                .ForMember(x => x.ContentType, opt => opt.MapFrom(p => p.Document.ContentType))
                .ForMember(x => x.Tag, opt => opt.MapFrom(p => p.Document.Checksum))
                .ForMember(x => x.LastModified, opt => opt.MapFrom(p => p.Document.LastModified));
        }
    }
}
