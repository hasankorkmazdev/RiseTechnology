using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RiseTechnology.Common.DependencyInjectionsLifeCycles;
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
        public void Publish(string queueName, object data)
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuration.GetValue<string>("RabbitMq:Host"),
                Password = configuration.GetValue<string>("RabbitMq:Password"),
                UserName = configuration.GetValue<string>("RabbitMq:UserName")

            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queueName,
                    
                      durable: false,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null);

                var message = JsonSerializer.Serialize(data);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "Report",
                                     routingKey: "Report.Proccesor",
                                     basicProperties: properties,
                                     body: body);

            }
        }

        public void Consume(string queueName, Action<object, BasicDeliverEventArgs> callback) 
        {

            var factory = new ConnectionFactory()
            {
                HostName = configuration.GetValue<string>("RabbitMq:Host"),
                Password = configuration.GetValue<string>("RabbitMq:Password"),
                UserName = configuration.GetValue<string>("RabbitMq:UserName")

            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {

                    callback(model, ea);
                    channel.BasicConsume(queue: "Queries", autoAck: true, consumer: consumer);
                };
            }
        }
    }
}
