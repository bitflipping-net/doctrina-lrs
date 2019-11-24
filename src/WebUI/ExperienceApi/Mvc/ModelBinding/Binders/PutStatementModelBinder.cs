﻿using Doctrina.Application.Common.Exceptions;
using Doctrina.ExperienceApi.Client;
using Doctrina.ExperienceApi.Client.Exceptions;
using Doctrina.ExperienceApi.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Doctrina.WebUI.Mvc.ModelBinders
{
    /// <summary>
    /// Binds a single statement
    /// </summary>
    public class PutStatementModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // Specify a default argument name if none is set by ModelBinderAttribute
            //var modelName = bindingContext.BinderModelName;
            //if (string.IsNullOrEmpty(modelName))
            //{
            //    modelName = "statements";
            //}

            var request = bindingContext.ActionContext.HttpContext.Request;

            try
            {
                var jsonModelReader = new JsonModelReader(request.Headers, request.Body);
                Statement statement = await jsonModelReader.ReadAs<Statement>();
                if(statement != null)
                {
                    bindingContext.Result = ModelBindingResult.Success(statement);
                }
                else
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                }
            }
            catch (JsonModelReaderException ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}
