using Common.Domain.Enums;

namespace Common.Implementations.ExceptionImplementations.Exceptions
{
    public class InvalidPermissionException : BaseException
    {
        public InvalidPermissionException(ErrorCode errorCode, string message = null)
            : base(errorCode, message)
        {
        }
    }
}
