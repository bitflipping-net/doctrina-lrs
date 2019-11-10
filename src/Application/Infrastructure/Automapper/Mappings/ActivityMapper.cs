using AutoMapper;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;

namespace Application.Infrastructure.Automapper.Mappings
{
    public class ActivityMapper : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Activity, ActivityEntity>()
                .ForMember(e => e.Id, opt => opt.MapFrom(x => x.Id.ToString()))
                .ForMember(e => e.Hash, opt => opt.MapFrom(x => x.Id.ComputeHash()))
                .ForMember(entity => entity.Definition, opt => opt.MapFrom(x => x.Definition))
                //.ForMember(entity => entity.Definition, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
