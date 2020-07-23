using Common.Domain.Enums;

namespace Common.Implementations.ExceptionImplementations.Exceptions
{
    public class InvalidDataException : BaseException
    {
        public InvalidDataException(ErrorCode errorCode, string message = null)
            : base(errorCode, message)
        {
        }
    }
}
