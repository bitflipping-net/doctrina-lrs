using Doctrina.Application.Statements.Models;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Specialized;

namespace Doctrina.Application.Statements.Queries
{
    public class PagedStatementsQuery : StatementsQuery, IRequest<PagedStatementsResult>
    {
        public string Cursor { get; set; }

        public string Version { get; set; }

        public string AcceptLanguage { get; set; }

        public int PageIndex { get; set; }

        public override NameValueCollection ToParameterMap(ApiVersion version)
        {
            if (version == null)
            {
                throw new ArgumentNullException(nameof(version));
            }

            var values = base.ToParameterMap(version);
            if (!string.IsNullOrEmpty(Cursor))
            {
                values.Add("more", Cursor);
            }
            return values;
        }

        public string ToJson()
        {
            var contractResolver = new DefaultContractResolver();
            contractResolver.IgnoreSerializableInterface = false;
            contractResolver.IgnoreSerializableAttribute = false;
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = contractResolver
            };
            return JsonConvert.SerializeObject(this, settings);
        }

        public static PagedStatementsQuery FromJson(string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                throw new ArgumentNullException(nameof(jsonString));
            }

            return JsonConvert.DeserializeObject<PagedStatementsQuery>(jsonString);
        }
    }
}
