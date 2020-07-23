using System;
using Common.Domain.Enums;
using Common.Domain.DTOs;
using Common.Implementations.ExceptionImplementations.Extensions;

namespace Common.Implementations.ExceptionImplementations.Exceptions
{
    public class BaseException : Exception
    {
        public readonly ErrorCode ErrorCode;
        private ErrorDTO _error;
        public ErrorDTO Error
        {
            get
            {
                return _error ??
                    new ErrorDTO(ErrorCode, ErrorCode.GetDescription());
            }
        }


        public BaseException(ErrorCode errorCode, string message = null)
            : base(message)
        {
            ErrorCode = errorCode;
            _error = new ErrorDTO(ErrorCode, message ?? ErrorCode.GetDescription());
        }
    }
}
