using Doctrina.Application.Interfaces;
using Doctrina.ExperienceApi.Client.Http;
using Doctrina.ExperienceApi.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Doctrina.WebUI.ExperienceApi.Controllers
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        // private readonly IRequestAuthority _requestAuthority;

        // public ApiControllerBase(IRequestAuthority requestAuthority)
        // {
        //     _requestAuthority = requestAuthority;
        // }

        public Agent Authority
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    // TODO: The current authority should be resolved using authentication and scoped through application

                    return new Agent()
                    {
                        Name = User.Identity.Name,
                        OpenId = new Iri($"{Request.Scheme}://{Request.Host.Value}")
                    };
                }

                return null;
            }
        }

        public ApiVersion APIVersion
        {
            get
            {
                return Request.Headers[ApiHeaders.XExperienceApiVersion].ToString();
            }
        }
    }
}
