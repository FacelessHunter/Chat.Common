using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Common.Domain.Enums
{
    public enum ErrorCode
    {

        [Description("Invalid ConnectionId")]
        InvalidConnectionId = 3400,

        [Description("ConnectionId Not Found")]
        ConnectionIdNotFound = 3404,

        [Description("Invalid")]
        Invalid = 2400,

        [Description("Invalid Permission")]
        InvalidPermision = 2401,

        [Description("Forbidden access")]
        Forbidden = 2403,

        [Description("Not found")]
        NotFound = 2404,

        [Description("Conflict")]
        Conflict = 2409,

        [Description("Unknown Error")]
        UnknownError = 2500,

        [Description("Not found user")]
        NotFoundUser = 1404,

    }
}
