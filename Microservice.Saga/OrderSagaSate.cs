using System;
using System.Collections.Generic;
using System.Text;
using Automatonymous;

namespace Microservice.Saga
{
    public class OrderSagaSate: SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public  State CurrentState { get; set; }
        public DateTime ReceiveDateTime { get; set; }
        public DateTime RegisteredDateTime { get; set; }

        public string PickupName { get; set; }
        public string PickupAddress { get; set; }
        public string PickupCity { get; set; }
        public string DeliverName { get; set; }
        public string DeliverAddress { get; set; }
        public string DeliverCity { get; set; }
    }
}
