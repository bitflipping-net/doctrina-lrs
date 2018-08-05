﻿using Doctrina.xAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Doctrina.Web.Areas.xAPI.Mvc.Filters
{
    /// <summary>
    /// Ensures the request has a supported 'X-Experience-Api-Version' header.
    /// </summary>
    public class VersionHeaderAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var supported = XAPIVersion.GetSupported();
            if (!context.HttpContext.Request.Headers.ContainsKey(Constants.Headers.APIVersion))
                throw new Exception("Missing 'X-Experience-API-Version' header or it's null or empty");

            string requestVersion = context.HttpContext.Request.Headers[Constants.Headers.APIVersion];
            if (string.IsNullOrEmpty(requestVersion))
                throw new Exception("Missing 'X-Experience-API-Version' header or it's null or empty");

            if (!XAPIVersion.GetSupported().ContainsKey(requestVersion))
                throw new Exception($"'X-Experience-API-Version' header is '{requestVersion}' which is not supported.");

            try
            {
                XAPIVersion version = (XAPIVersion)requestVersion;
                await next();
            }
            catch (Exception ex)
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}