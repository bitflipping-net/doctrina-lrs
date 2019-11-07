using Doctrina.Application.Statements.Models;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Client.Http;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Specialized;

namespace Doctrina.Application.Statements.Queries
{
    public class PagedStatementsQuery : StatementsQuery, IRequest<PagedStatementsResult>
    {
        [FromQuery(Name = "token")]
        public string Token { get; set; }

        [FromHeader(Name = ApiHeaders.XExperienceApiVersion)]
        public string Version { get; set; }

        [FromHeader(Name = "Accept-Languge")]
        public string AcceptLanguage { get; set; }

        public override NameValueCollection ToParameterMap(ApiVersion version)
        {
            if (version == null)
            {
                throw new ArgumentNullException(nameof(version));
            }

            var values = base.ToParameterMap(version);
            if (!string.IsNullOrEmpty(Token))
            {
                values.Add("token", Token);
            }
            return values;
        }
    }
}
