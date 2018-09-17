using CQRSlite.Routing;
using Microservice.Employee.App_Start;
using Microservice.Employee.CQRS.WriteModel.Handlers;
using Microservice.Employee.DependencyResolution;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Microservice.Employee
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			StructuremapWebApi.Start();

			var container = StructuremapMvc.StructureMapDependencyScope.Container;
			var serviceProvider = new StructureMapResolver(container);
			var registrar = new RouteRegistrar(serviceProvider);
			//registrar.RegisterInAssemblyOf(typeof(InventoryCommandHandlers));
			var result = serviceProvider.GetService(typeof(InventoryCommandHandlers));
			registrar.RegisterHandlers(typeof(InventoryCommandHandlers));
		}
	}
}
