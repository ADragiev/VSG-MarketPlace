using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Models.ExceptionModels;
using Application.Models.GenericRepo;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Helpers.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IMarketPlaceContext marketPlaceContext)
        {
            try
            {
                await next(context);
                marketPlaceContext.Transaction.Commit();
            }
            catch (Exception ex)
            {
                marketPlaceContext.Transaction.Rollback();

                await HandleException(ex, context);
            }


        }

        private async Task HandleException(Exception ex, HttpContext context)
        {
            List<ProblemDetails> problems = new List<ProblemDetails>();
            if (ex is HttpException httpException)
            {
                problems.Add(new ProblemDetails()
                {
                    Status = (int)httpException.HttpStatusCode,
                    Title = ex.Message
                });
            }
            else if (ex is ValidationException validationException)
            {
                foreach (var error in validationException.Errors)
                {
                    problems.Add(new ProblemDetails()
                    {
                        Status = 400,
                        Title = error.ErrorMessage
                    });
                }
            }
            else
            {
                problems.Add(new ProblemDetails()
                {
                    Status = 500,
                    Title = "Internal Server Error"
                });
                logger.LogError(ex.Message);
            }

            string json = JsonSerializer.Serialize(problems);
            context.Response.StatusCode = (int)problems[0].Status;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}
