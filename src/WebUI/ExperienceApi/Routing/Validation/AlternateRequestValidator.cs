using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Doctrina.WebUI.ExperienceApi.Routing.Validation
{
    public class AlternateRequestValidator : AbstractValidator<HttpRequest>
    {
        private static string[] allowedMethodNames = new string[] { "POST", "GET", "PUT", "DELETE" };

        public AlternateRequestValidator()
        {
            //RuleFor(x => x.Method.ToUpperInvariant()).Equal("POST");

            //RuleFor(request => request.Query).Must(q => q.Count == 1)
            //    .When(request => request.Query.ContainsKey("method"))
            //    .WithMessage("An LRS will reject an alternate request syntax which contains any extra information with error code 400 Bad Request (Communication 1.3.s3.b4)");

            //RuleFor(request => request.Query[""])
            //    .

            ////if (request.Method.ToUpperInvariant() != "POST")
            ////{
            ////    throw new BadRequestException("An LRS rejects an alternate request syntax not issued as a POST");
            ////}

            //if (!allowedMethodNames.Contains(methodQuery))
            //{
            //    throw new BadRequestException($"Query parameter method \"{methodQuery}\" is not alloed. ");
            //}

            //// Multiple query parameters are not allowed
            //if (request.Query.Count != 1)
            //{
            //    throw new BadRequestException("An LRS will reject an alternate request syntax which contains any extra information with error code 400 Bad Request (Communication 1.3.s3.b4)");
            //}

            //if (!request.HasFormContentType)
            //{
            //    throw new BadRequestException("Alternate request syntax sending content does not have a form parameter with the name of \"content\"");
            //}
        }
    }
}
