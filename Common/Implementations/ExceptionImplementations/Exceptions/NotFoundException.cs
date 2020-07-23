using Common.Domain.Enums;

namespace Common.Implementations.ExceptionImplementations.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(ErrorCode errorCode, string message = null)
            : base(errorCode, message)
        {
        }
    }
}
