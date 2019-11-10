
using AutoMapper;
using Doctrina.Application.Infrastructure.AutoMapper;

namespace Doctrina.Application.Tests.Infrastructure
{
    public static class AutoMapperFactory
    {
        public static IMapper Create()
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            mappingConfig.AssertConfigurationIsValid();

            return mappingConfig.CreateMapper();
        }
    }
}