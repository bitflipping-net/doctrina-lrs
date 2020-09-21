using AutoMapper;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Interfaces;
using Doctrina.ExperienceApi.Data;

public class PersonaMappings : IHaveCustomMapping
{
    public void CreateMappings(Profile configuration)
    {
        configuration.CreateMap<IAgentEntity, IAgentEntity>();

        configuration.CreateMap<Agent, Persona>()
            .ForMember(ent => ent.Id, opt => opt.Ignore())
            .ForMember(ent => ent.Persona, opt => opt.Ignore())
            .ForMember(ent => ent.PersonaId, opt => opt.Ignore())
            .ForMember(ent => ent.StoreId, opt => opt.Ignore())
            .ForMember(ent => ent.Store, opt => opt.Ignore())
           .ForMember(ent => ent.Type, opt => opt.MapFrom(x => x.GetIdentifierKey()))
           .ForMember(ent => ent.Value, opt => opt.MapFrom(x => x.GetIdentifierValue()))
           .ReverseMap();

        configuration.CreateMap<Group, Group>()
           .IncludeBase<Agent, Persona>()
           .ForMember(ent => ent.People, opt => opt.Ignore())
           .ReverseMap();

        configuration.CreateMap<Agent, Persona>()
            .ForMember(x=> x.Type, opt=> opt.MapFrom(x=> x.GetIdentifierKey()))
    }
}