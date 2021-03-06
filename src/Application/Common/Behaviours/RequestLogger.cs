﻿using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Common.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;

        public RequestLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            // TODO: Add User Details

            _logger.LogInformation("Request: {@Request}", request.GetType().Name);
            _logger.LogDebug("Request information: {@Request}", request);

            return Task.CompletedTask;
        }
    }
}