using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Models.ExceptionModels;
using Application.Models.GenericRepo;
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

        public async Task InvokeAsync(HttpContext context, IMarketPlaceContext marketPlaceContext)
        {
            try
            {
                await next(context);
                marketPlaceContext.Transaction.Commit();   
            }
            catch (HttpException ex)
            {
                marketPlaceContext.Transaction.Rollback();

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
