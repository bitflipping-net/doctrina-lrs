using AutoMapper;
using Doctrina.Application.Infrastructure.ExperienceApi;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Domain.Models;
using Doctrina.Domain.Models.Interfaces;
using Doctrina.ExperienceApi.Data;

public class AgentMppings : IHaveCustomMapping
{
    public void CreateMappings(Profile configuration)
    {
        configuration.CreateMap<IAgentEntity, IAgentEntity>();

        configuration.CreateMap<Agent, Persona>()
            .ForMember(ent => ent.PersonaId, opt => opt.Ignore())
            .ForMember(ent => ent.Store, opt => opt.Ignore())
            .ForMember(ent => ent.StoreId, opt => opt.Ignore())
            .ForMember(ent => ent.ObjectType, opt => opt.Ignore())
           .ForMember(ent => ent.Name, opt => opt.MapFrom(x => x.Name))
           .ForMember(ent => ent.Key, opt => opt.MapFrom(x => x.GetIdentifierKey()))
           .ForMember(ent => ent.Value, opt => opt.MapFrom(x => x.GetIdentifierValue()));

        configuration.CreateMap<Group, Group>()
           .IncludeBase<Agent, Persona>()
           .ForMember(ent => ent.Member, opt => opt.Ignore()) // Members are mapped internally
           .ReverseMap();
    }
}
