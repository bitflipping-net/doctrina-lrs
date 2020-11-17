using AutoMapper;
using Doctrina.Application.Infrastructure.ExperienceApi;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Interfaces;
using Doctrina.ExperienceApi.Data;

public class AgentMppings : IHaveCustomMapping
{
    public void CreateMappings(Profile configuration)
    {
        configuration.CreateMap<IAgentEntity, IAgentEntity>();

        configuration.CreateMap<Agent, AgentEntity>()
            .ForMember(ent => ent.AgentId, opt => opt.Ignore())
            .ForMember(ent => ent.ObjectType, opt => opt.Ignore())
           .ForMember(ent => ent.Name, opt => opt.MapFrom(x => x.Name))
           .ForMember(ent => ent.IFI_Key, opt => opt.MapFrom(x => x.GetIdentifierKey()))
           .ForMember(ent => ent.IFI_Value, opt => opt.MapFrom(x => x.GetIdentifierValue()))
           .ReverseMap();

        configuration.CreateMap<Group, GroupEntity>()
           .IncludeBase<Agent, AgentEntity>()
           .ForMember(ent => ent.Members, opt => opt.Ignore())
           .ReverseMap();
    }
}