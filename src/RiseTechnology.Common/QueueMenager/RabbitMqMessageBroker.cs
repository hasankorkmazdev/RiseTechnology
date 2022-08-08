using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RiseTechnology.Common.DependencyInjectionsLifeCycles;
using RiseTechnology.Common.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RiseTechnology.Common.QueueMenager
{
    public class RabbitMqMessageBroker : IMessageBrokerService, ISingletonLifetime
    {
       private readonly IConfiguration configuration;
        public RabbitMqMessageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void Publish(string queueName, Guid data)
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuration.GetValue<string>("RabbitMq:Host"),
                Password = configuration.GetValue<string>("RabbitMq:Password"),
                UserName = configuration.GetValue<string>("RabbitMq:UserName"),
                Port= configuration.GetValue<int>("RabbitMq:Port")

            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var body = Encoding.UTF8.GetBytes(data.ToString());

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                channel.QueueDeclare(queue: queueName,
                      durable: true,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null);
                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: properties,
                                     body: body);

            }
        }

        public void Consume(string queueName, Action<object> callback)
        {

            var factory = new ConnectionFactory()
            {
                HostName = configuration.GetValue<string>("RabbitMq:Host"),
                Password = configuration.GetValue<string>("RabbitMq:Password"),
                UserName = configuration.GetValue<string>("RabbitMq:UserName"),
                Port = configuration.GetValue<int>("RabbitMq:Port")

            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();


            channel.QueueDeclare(queue: queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);


            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, eventArgs) =>
            {
                try
                {
                    var data = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                    callback(data);
                    channel.BasicAck(eventArgs.DeliveryTag, false);

                }
                catch (Exception)
                {
                    channel.BasicNack(eventArgs.DeliveryTag, false,true);
                }

            };
            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

        }
    }
}
