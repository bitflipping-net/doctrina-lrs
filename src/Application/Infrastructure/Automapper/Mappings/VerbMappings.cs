using AutoMapper;
using Doctrina.Application.Infrastructure.Automapper.Mappings.TypeConverters;
using Doctrina.Application.Interfaces.Mapping;
using Doctrina.Application.Mappings.ValueResolvers;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;

namespace Doctrina.Application.Mappings
{
    public class VerbMappings : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
        }
    }
}