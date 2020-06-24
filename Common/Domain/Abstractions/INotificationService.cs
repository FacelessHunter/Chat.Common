using Common.Domain.DTOs;

namespace Common.Domain.Abstractions
{
    public interface INotificationService
    {
        void Publish(ViewMessageDTO messageToSend, long chatId);
    }
}
