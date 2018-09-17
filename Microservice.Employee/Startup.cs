using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Microservice.Employee.Startup))]

namespace Microservice.Employee
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
			//var bus = BusConfigurator.ConfigureBus((cfg, host) =>


			//bus.StartAsync();
		}
	}
}
