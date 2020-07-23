using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Common.Domain.ActionResults
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error) : base(error)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
