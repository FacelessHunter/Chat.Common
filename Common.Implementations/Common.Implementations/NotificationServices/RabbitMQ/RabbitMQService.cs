using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using Common.Domain.DTOs;
using Common.Domain.Abstractions;

namespace Common.Implementations.NotificationServices.RabbitMQ
{
    public class RabbitMQService : INotificationService
    {
        public RabbitMQService(IConnectionService connectionService)
        {
            _connectionService = connectionService;

            if (!_connectionService.IsConnected)
                _connectionService.TryConnect();

        }
        private readonly IConnectionService _connectionService;

        private const string exchange = "chats";

        private IModel channel;

        public void Publish(ViewMessageDTO messageToSend, long chatId)
        {
            if (!_connectionService.IsConnected)
                _connectionService.TryConnect();

            if (channel == null || channel.IsClosed || !channel.IsOpen)
            {
                channel = _connectionService.CreateModel();
                channel.ExchangeDeclare(exchange: exchange, type: "topic");
            }

            var routingKey = $"{exchange}.{chatId}.*";
            var message = JsonConvert.SerializeObject(messageToSend);
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: null, body: body);
        }
    }
}
