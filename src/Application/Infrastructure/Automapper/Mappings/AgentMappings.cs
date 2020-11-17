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
           .ForMember(ent => ent.IFI_Key, opt => opt.MapFrom(x => x.GetIdentifierKey()))
           .ForMember(ent => ent.IFI_Value, opt => opt.MapFrom(x => x.GetIdentifierValue()))
           .ReverseMap()
           .ForMember(x => x.Mbox, opt =>
           {
               opt.PreCondition(src => src.IFI_Key == Ifi.Mbox);
               opt.MapFrom(src => new Mbox(src.IFI_Value));
           })
           .ForMember(x => x.Mbox_SHA1SUM, opt =>
           {
               opt.PreCondition(src => src.IFI_Key == Ifi.Mbox_SHA1SUM);
               opt.MapFrom(src => src.IFI_Value);
           })
           .ForMember(x => x.OpenId, opt =>
           {
               opt.PreCondition(src => src.IFI_Key == Ifi.OpenId);
               opt.MapFrom(src => src.IFI_Value);
           })
           .ForMember(x => x.Account, opt =>
           {
               opt.PreCondition(src => src.IFI_Key == Ifi.Account);
               opt.MapFrom(src => new Account(src.IFI_Value));
           })
           .ForMember(dst => dst.Name, opt =>
           {
               opt.PreCondition(src => src.Person != null);
               opt.MapFrom(src => src.Person.Name);
           })
           .ForMember(dst=> dst.ObjectType, opt => opt.MapFrom(src=> src.ObjectType));

        configuration.CreateMap<Group, GroupEntity>()
           .IncludeBase<Agent, AgentEntity>()
           .ForMember(ent => ent.Members, opt => opt.Ignore())
           .ReverseMap();
    }
}