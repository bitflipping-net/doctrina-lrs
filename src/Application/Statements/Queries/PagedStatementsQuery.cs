using Doctrina.Application.Statements.Models;
using Doctrina.ExperienceApi.Client.Http;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Specialized;

namespace Doctrina.Application.Statements.Queries
{
    public class PagedStatementsQuery : StatementsQuery, IRequest<PagedStatementsResult>
    {
        [FromQuery(Name = "more")]
        public string MoreToken { get; set; }

        [FromHeader(Name = ApiHeaders.XExperienceApiVersion)]
        public string Version { get; set; }

        [FromHeader(Name = "Accept-Languge")]
        public string AcceptLanguage { get; set; }

        public int PageIndex { get; set; }

        public override NameValueCollection ToParameterMap(ApiVersion version)
        {
            if (version == null)
            {
                throw new ArgumentNullException(nameof(version));
            }

            var values = base.ToParameterMap(version);
            if (!string.IsNullOrEmpty(MoreToken))
            {
                values.Add("more", MoreToken);
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
