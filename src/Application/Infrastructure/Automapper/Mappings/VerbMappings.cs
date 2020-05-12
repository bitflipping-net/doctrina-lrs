using AutoMapper;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Application.Mappings.ValueResolvers;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;

public class VerbMappings : IHaveCustomMapping
{
    public void CreateMappings(Profile configuration)
    {
        configuration.CreateMap<Verb, VerbEntity>()
            .ForMember(x=> x.VerbId, opt => opt.Ignore())
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.ToString()))
            .ForMember(x => x.Hash, opt => opt.MapFrom(x => x.Id.ComputeHash()))
            .ForMember(x => x.Display, opt => opt.MapFrom<LanguageMapValueResolver, LanguageMap>(x => x.Display))
            .ReverseMap();

        //configuration.CreateMap<VerbEntity, MergeVerbCommand>();
    }
}