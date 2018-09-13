using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservice.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using IModel = Microsoft.EntityFrameworkCore.Metadata.IModel;
using RabbitMqConstant = Microservice.Notification.RabbitMqConstant;

namespace Microservice
{
    public class RabbitMqManager : IDisposable
    {
        private readonly RabbitMQ.Client.IModel channel;

        public RabbitMqManager()
        {
            var connectionFactory = 
                new ConnectionFactory { Uri = new Uri(RabbitMqConstant.RabbitMqUri) };

            connectionFactory.AutomaticRecoveryEnabled = true;
            connectionFactory.TopologyRecoveryEnabled = true;
            connectionFactory.NetworkRecoveryInterval = TimeSpan.FromSeconds(10);

            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            connection.AutoClose = true;
        }

        public void SendRegisteredOrderCommand(IRegisterOrderCommand command)
        {
            channel.ExchangeDeclare(
                exchange: RabbitMqConstant.RegisterOrderserviceExchange,
                type: ExchangeType.Direct
                );

            channel.QueueDeclare(
                queue: RabbitMqConstant.RegisterOrderserviceQueue, durable: false,//set durable true
                exclusive: false, autoDelete: false, arguments: null
            );

            channel.QueueBind(
                queue:RabbitMqConstant.RegisterOrderserviceQueue,
                exchange:RabbitMqConstant.RegisterOrderserviceExchange,
                routingKey:""
                );
            var serialized = JsonConvert.SerializeObject(command);
            var messageProperties = channel.CreateBasicProperties();
            messageProperties.ContentType = RabbitMqConstant.JsonMimeType;

            channel.BasicPublish(
                exchange: RabbitMqConstant.RegisterOrderserviceExchange,
                routingKey:"",
                basicProperties:messageProperties,
                body:Encoding.UTF8.GetBytes(serialized)
                );
        }

        //Generate Method that That send Command 
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
