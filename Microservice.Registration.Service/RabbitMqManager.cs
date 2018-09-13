using System;
using System.Collections.Generic;
using System.Text;
using Microservice.Messaging;
using Microservice.Registration.Service.Message;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Microservice.Registration.Service
{
    public class RabbitMqManager: IDisposable
    {
        private readonly RabbitMQ.Client.IModel channel;

        public RabbitMqManager()
        {
            var connectionFactory =
                new ConnectionFactory { Uri = new Uri(RabbitMqConstant.RabbitMqUri) };
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            connection.AutoClose = true;
        }

        public void ListenForRegisterOrderCommand()
        {
            channel.QueueDeclare(
                queue: RabbitMqConstant.RegisterOrderserviceQueue, durable: false,//set durable true
                exclusive: false, autoDelete: false, arguments: null
            );

            channel.BasicQos(prefetchSize:0, prefetchCount:1, global:false);

            var consumer = new RegisterOrderCommandConsumer(this);

            channel.BasicConsume(
                queue: RabbitMqConstant.RegisterOrderserviceQueue,
                autoAck: false,
                consumer: consumer
            );
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void sendAck(ulong deliveryTag)
        {
            channel.BasicAck(deliveryTag: deliveryTag, multiple: false);
        }

        public void SendOrderRegisteredEvent(IOrderRegisteredEvent command)
        {
            channel.ExchangeDeclare(
                exchange: RabbitMqConstant.RegisterOrderserviceExchange,
                type: ExchangeType.Fanout
            );

            channel.QueueDeclare(
                queue: RabbitMqConstant.RegisterOrderserviceQueue, durable: false,//set durable true
                exclusive: false, autoDelete: false, arguments: null
            );

            channel.QueueBind(
                queue: RabbitMqConstant.RegisterOrderserviceQueue,
                exchange: RabbitMqConstant.RegisterOrderserviceExchange,
                routingKey: ""
            );
            var serialized = JsonConvert.SerializeObject(command);
            var messageProperties = channel.CreateBasicProperties();
            messageProperties.ContentType = RabbitMqConstant.JsonMimeType;

            channel.BasicPublish(
                exchange: RabbitMqConstant.RegisterOrderserviceExchange,
                routingKey: "",
                basicProperties: messageProperties,
                body: Encoding.UTF8.GetBytes(serialized)
            );
        }
    }

    public class RegisterOrderCommandConsumer : IBasicConsumer
    {
        private readonly RabbitMqManager rabbitManager;
        public RegisterOrderCommandConsumer(RabbitMqManager rabbitMqManager)
        {
            this.rabbitManager = rabbitMqManager;
        }

        public RegisterOrderCommandConsumer()
        {
            
        }

        public void HandleBasicCancel(string consumerTag)
        {
            throw new NotImplementedException();
        }

        public void HandleBasicCancelOk(string consumerTag)
        {
            throw new NotImplementedException();
        }

        public void HandleBasicConsumeOk(string consumerTag)
        {
            throw new NotImplementedException();
        }

        public void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey,
            IBasicProperties properties, byte[] body)
        {
            if (properties.ContentType != RabbitMqConstant.JsonMimeType)
                throw new ArgumentException($"Cant Handle content type {properties.ContentType}");

            var message = Encoding.UTF8.GetString(body);
            var commandObj = JsonConvert.DeserializeObject<RegisterOrderCommand>(message);

            Consume(commandObj);
            rabbitManager.sendAck(deliveryTag);
        }

        private void Consume(RegisterOrderCommand commandObj)
        {
            //store order registration in db
            var id = 12;

            Console.WriteLine($"Order with id {id} registered");
            //notify suscribers
            var orderRegisteredEvent = new OrderRegisteredEvent(commandObj, id);
            //publish event
            rabbitManager.SendOrderRegisteredEvent(orderRegisteredEvent);
        }

        public void HandleModelShutdown(object model, ShutdownEventArgs reason)
        {
            throw new NotImplementedException();
        }

        public IModel Model { get; }
        public event EventHandler<ConsumerEventArgs> ConsumerCancelled;
    }
}
