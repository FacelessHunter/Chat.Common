using Common.Domain.DTOs.MessageDTOs;
using System;
using System.Threading.Tasks;

namespace Common.Domain.Abstractions
{
    public interface INotificationService
    {
        void Publish(ViewMessageDTO messageToSend, long chatId);

        void SubscribeToChat(long chatId, Action<string> action);

        void UnsubscribeFromChat(long chatId);
    }
}
