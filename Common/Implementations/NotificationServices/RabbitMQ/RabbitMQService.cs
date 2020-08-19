using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using Common.Domain.Abstractions;
using Common.Domain.DTOs.MessageDTOs;
using RabbitMQ.Client.Events;
using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;

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

        private readonly ConcurrentDictionary<long, IModel> channels = new ConcurrentDictionary<long, IModel>();


        private IModel publishChannel;

        //public delegate Task MrthodToSubscribe(string message);

        public void Publish(ViewMessageDTO messageToSend, long chatId)
        {
            if (!_connectionService.IsConnected)
                _connectionService.TryConnect();

            if (publishChannel == null || publishChannel.IsClosed || !publishChannel.IsOpen)
            {
                publishChannel = _connectionService.CreateModel();
                publishChannel.ExchangeDeclare(exchange: exchange, type: "topic");
            }

            var routingKey = $"{exchange}.{chatId}.*";
            var message = JsonConvert.SerializeObject(messageToSend);
            var body = Encoding.UTF8.GetBytes(message);
            publishChannel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: null, body: body);
        }


        public void SubscribeToChat(long chatId, Action<string> action)
        {
            if (!_connectionService.IsConnected)
                _connectionService.TryConnect();

            IModel channel = _connectionService.CreateModel();
            channel.ExchangeDeclare(exchange: exchange, type: "topic");

            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: exchange, routingKey: $"{exchange}.{chatId}.*");

            var consumer = new EventingBasicConsumer(channel);
            
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                var routingKey = ea.RoutingKey;
                action(message);
            };
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            channels.TryAdd(chatId, channel);
        }

        public void UnsubscribeFromChat(long chatId)
        {
            channels.TryGetValue(chatId, out IModel model);
            model.Close();
            model.Dispose();
        }
    }
}
