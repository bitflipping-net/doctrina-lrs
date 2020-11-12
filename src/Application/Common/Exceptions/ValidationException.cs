using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Doctrina.Application.Common.Exceptions
{
    public class ValidationException : Doctrina.ExperienceApi.Data.Exceptions.ValidationException
    {
        public ValidationException()
            : base()
        {
        }

        public ValidationException(List<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }
    }
}
