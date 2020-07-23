using Common.Domain.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;


namespace Common.Implementations.NotificationServices.RabbitMQ
{
    public sealed class ConnectionService : IConnectionService
    {
        private readonly RabbitMQOptions _options;
        private IConnection _connection;
        private bool _disposed;

        private readonly object sync_root = new object();

        public ConnectionService(IOptions<RabbitMQOptions> options)
        {
            _options = options.Value;
        }

        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.IsOpen && !_disposed;
            }
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message);
            }
        }

        public bool TryConnect()
        {
            lock (sync_root)
            {

                _connection = CreateRabbitConnection(_options);


                if (IsConnected)
                {
                    _connection.ConnectionShutdown += OnConnectionShutdown;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionBlocked += OnConnectionBlocked;

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public IConnection CreateRabbitConnection(RabbitMQOptions options)
        {
            var factory = new ConnectionFactory
            {
                RequestedConnectionTimeout = new TimeSpan(0, 0, 2),
                AutomaticRecoveryEnabled = true,
                HostName = options.HostName,
                UserName = options.UserName,
                Port = options.Port,
                Password = options.Password,
                VirtualHost = options.VirtualHost
            };

            return factory.CreateConnection();
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;
            TryConnect();
        }

        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;
            TryConnect();
        }

        private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;
            TryConnect();
        }
    }
}
