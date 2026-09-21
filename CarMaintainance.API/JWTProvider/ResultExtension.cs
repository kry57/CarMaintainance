using CarMaintenance.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintainance.API.JWTProvider
{
    public static class ResultExtension
    {
        public static ObjectResult ToProblem(this Result result, int statusCode)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("Cannot convert a successful result to a problem.");

            var problemDetails = new ProblemDetails { Status = statusCode };
            problemDetails.Extensions["error"] = new[] { result.Error };

            return new ObjectResult(problemDetails) { StatusCode = statusCode };
        }
    }
}