using AutoMapper;
using Doctrina.Application.Infrastructure.ExperienceApi;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Domain.Models;
using Doctrina.Domain.Models.Interfaces;
using Doctrina.ExperienceApi.Data;

public class PersonaMappings : IHaveCustomMapping
{
    public void CreateMappings(Profile configuration)
    {
        configuration.CreateMap<IAgentEntity, IAgentEntity>();

        configuration.CreateMap<Agent, PersonaModel>()
            .ForMember(ent => ent.PersonaId, opt => opt.Ignore())
            .ForMember(ent => ent.StoreId, opt => opt.Ignore())
            .ForMember(ent => ent.Store, opt => opt.Ignore())
            .ForMember(ent => ent.ObjectType, opt => opt.MapFrom(x => x.ObjectType))
           .ForMember(ent => ent.Key, opt => opt.MapFrom(x => x.GetIdentifierKey()))
           .ForMember(ent => ent.Value, opt => opt.MapFrom(x => x.GetIdentifierValue()))
           .ReverseMap();

        configuration.CreateMap<Group, PersonaGroup>()
           .IncludeBase<Agent, PersonaModel>()
           .ForMember(ent => ent.Members, opt => opt.Ignore()) // Members are mapped when created from Agent
           .ReverseMap();
    }
}
