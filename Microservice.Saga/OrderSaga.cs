using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Automatonymous;
using Microservice.Messaging;

namespace Microservice.Saga
{
    public class OrderSaga: MassTransitStateMachine<OrderSagaSate>
    {
        public State Received { get; private set; }
        public State Registered { get; private set; }

        public Event<IRegisterOrderCommand> RegisterOrder { get; private set; }
        public Event<IOrderRegisteredEvent> OrderRegistered { get; private set; }

        public OrderSaga()
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
            InstanceState(s => s.CurrentState);
            //Event((() => RegisterOrder, cc => cc.CorrelatedBy(state => state.PickupName,)));
        }
    }
}
