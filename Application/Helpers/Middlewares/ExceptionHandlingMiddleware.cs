using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Models.ExceptionModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Helpers.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (HttpException ex)
            {
                //TODO: Може да го направя с custom error, за да не depend-вам на using Microsoft.AspNetCore.Mvc;


                ProblemDetails problem = new ProblemDetails()
                {
                    Status = (int)ex.HttpStatusCode,
                    Title = ex.Message,
                };

                string json = JsonSerializer.Serialize(problem);
                context.Response.StatusCode = (int)ex.HttpStatusCode;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }
        }
    }
}
