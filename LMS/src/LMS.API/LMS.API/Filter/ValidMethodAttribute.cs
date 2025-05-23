﻿using LMS.Application.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LMS.API.Filter;

public class ValidMethodAttribute : Attribute, IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values
                .SelectMany(modelState => modelState.Errors)
                .Select(modelError => modelError.ErrorMessage);

            context.Result = new BadRequestObjectResult(ApiResult<string>.Failure(errors));
        }

        await next();
    }
}
