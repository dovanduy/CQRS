using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.Messaging;
using Microservice.Registration.Service.Message;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var bus = BusConfigurator.ConfigureBus();
            var sendUri = new Uri($"{RabbitMqConstant.RabbitMqUri}" + $"{RabbitMqConstant.RegisterOrderserviceQueue}");
            var endpoint = await bus.GetSendEndpoint(sendUri);

            await endpoint.Send<IRegisterOrderCommand>(new
            {
                DeliverAddress = "AddressD",
                DeliverName = "Deliver",
                DeliverCity = "DeliverCity",
                PickupCity = "PickUpCity",
                PickupAddress = "PickUpAdress",
                PickupName = "PickName"
            });

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
