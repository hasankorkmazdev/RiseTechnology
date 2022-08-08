using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseTechnology.Common.QueueMenager
{
    public interface IMessageBrokerService
    {
        public void Publish(string queueName, Guid reportUuid);
        public void Consume(string queueName, Action<object> callback);
    }
}
