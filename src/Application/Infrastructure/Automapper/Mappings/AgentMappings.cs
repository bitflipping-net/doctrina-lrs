using AutoMapper;
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
            .ForMember(ent => ent.Hash, opt => opt.MapFrom(x => x.ComputeHash()))
           .ForMember(ent => ent.Name, opt => opt.MapFrom(x => x.Name))
           .ForMember(ent => ent.Mbox, opt => opt.MapFrom(x => x.Mbox.ToString()))
           .ForMember(ent => ent.Mbox_SHA1SUM, opt => opt.MapFrom(x => x.Mbox_SHA1SUM))
           .ForMember(ent => ent.OpenId, opt => opt.MapFrom(x => x.OpenId.ToString()))
           .ForMember(ent => ent.Account, opt => opt.MapFrom(x => x.Account))
           .ReverseMap();

        configuration.CreateMap<Group, GroupEntity>()
           .IncludeBase<Agent, AgentEntity>()
           .ForMember(ent => ent.Members, opt => opt.MapFrom(x => x.Member))
           .ReverseMap();
    }
}