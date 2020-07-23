using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Common.Domain.ActionResults
{
    public class ForbiddenObjectResult : ObjectResult
    {
        public ForbiddenObjectResult(object error) : base(error)
        {
            StatusCode = (int)HttpStatusCode.Forbidden;
        }
    }
}
