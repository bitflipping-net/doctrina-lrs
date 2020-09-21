using AutoMapper;
using System;

namespace Doctrina.Application.Infrastructure.Automapper.Mappings.TypeConverters
{
    using Doctrina.ExperienceApi.Data;
    using Newtonsoft.Json.Linq;

    public class StatementObjectConverter :
    ITypeConverter<IStatementObject, string>,
    ITypeConverter<string, IStatementObject>
    {
        public string Convert(IStatementObject source, string destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            return source.ToJson();
        }

        public IStatementObject Convert(string source, IStatementObject destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            var version = ApiVersion.GetLatest();

            var @object = JObject.Parse(source);
            if (@object != null)
            {
                var jobjectType = @object["objectType"];
                if (jobjectType != null)
                {
                    ObjectType type = jobjectType.Value<string>();
                    return type.CreateInstance(@object, version);
                }
                else if (@object["id"] != null)
                {
                    // Assume activity
                    return new Activity(@object, version);
                }
            }

            throw new NotImplementedException();
        }
    }
}
