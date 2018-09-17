// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using StructureMap.Web;

namespace Microservice.Employee.DependencyResolution
{
	using AutoMapper;
	using CQRSlite.Caching;
	using CQRSlite.Commands;
	using CQRSlite.Domain;
	using CQRSlite.Events;
	using CQRSlite.Routing;
	using Microservice.Employee.CQRS;
	using Microservice.Employee.CQRS.WriteModel;
	using Microservice.Employee.CQRS.WriteModel.Handlers;
	using StackExchange.Redis;
	using StructureMap;
	using StructureMap.Configuration.DSL;
	using StructureMap.Graph;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Http.Dependencies;
	using ISession = CQRSlite.Domain.ISession;

	public class StructureMapResolver : IServiceProvider
	{
		protected IContainer _container;


		public StructureMapResolver(IContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			this._container = container;

		}

		public object GetService(Type serviceType)
		{
			try
			{
				return _container.GetInstance(serviceType);
			}
			catch (Exception c)
			{
				return null;
			}
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			try
			{

				var result = _container.GetAllInstances(serviceType);

				if (result == null)
					throw new Exception("No service registered");

				return (IEnumerable<object>)result;
			}
			catch (Exception)
			{
				return new List<object>();
			}
		}

		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			_container.Dispose();
		}

		public IDependencyScope BeginScope()
		{
			throw new NotImplementedException();
		}
	}

	public class DefaultRegistry : Registry
	{
		#region Constructors and Destructors

		public DefaultRegistry()
		{
			Scan(
				scan =>
				{
					//scan.Assembly("Microservice.Employee");
					scan.AssemblyContainingType<InventoryCommandHandlers>();
					// Filter types
					//scan.Exclude(type =>
					//{
					//	// Filter types
					//	var allInterfaces = type.GetInterfaces();
					//	return allInterfaces.Any(y => y.GetType().IsGenericType && y.GetType().GetGenericTypeDefinition() != typeof(IHandler<>)) ||
					//		   allInterfaces.Any(y => y.GetType().IsGenericType && y.GetType().GetGenericTypeDefinition() != typeof(ICancellableHandler<>));

					//});

					scan.TheCallingAssembly();
					//scan.AssemblyContainingType<BaseEvent>();

					scan.Convention<FirstInterfaceConvention>();
				});

			//services.Scan(scan => scan
			//	.FromAssemblies(typeof(InventoryCommandHandlers).GetTypeInfo().Assembly)
			//	.AddClasses(classes => classes.Where(x => {
			//		var allInterfaces = x.GetInterfaces();
			//		return
			//			allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(IHandler<>)) ||
			//			allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(ICancellableHandler<>));
			//	}))
			//	.AsSelf()
			//	.WithTransientLifetime()
			//);


			//CQRSLite
			//For<InProcessBus>().Singleton().Use<InProcessBus>();
			//For<ICommandSender>().Use(y => y.GetInstance<InProcessBus>());
			//For<IEventPublisher>().Use(y => y.GetInstance<InProcessBus>());
			//For<IHandlerRegistrar>().Use(y => y.GetInstance<InProcessBus>());
			//For<ISession>().HybridHttpOrThreadLocalScoped().Use<Session>();
			//For<IEventStore>().Singleton().Use<InMemoryEventStore>();
			//For<IRepository>().HybridHttpOrThreadLocalScoped().Use(y =>
			//	new CacheRepository(new Repository(y.GetInstance<IEventStore>()), y.GetInstance<IEventStore>()));


			//Add Cqrs services
			//services.AddSingleton<Router>(new Router());
			For<Router>().Singleton().Use<Router>();
			//services.AddSingleton<ICommandSender>(y => y.GetService<Router>());
			For<ICommandSender>().Singleton().Use(y => y.GetInstance<Router>());
			//services.AddSingleton<IEventPublisher>(y => y.GetService<Router>());
			For<IEventPublisher>().Singleton().Use(y => y.GetInstance<Router>());
			//services.AddSingleton<IHandlerRegistrar>(y => y.GetService<Router>());
			For<IHandlerRegistrar>().Singleton().Use(y => y.GetInstance<Router>());
			//services.AddScoped<ISession, Session>();
			For<ISession>().HybridHttpOrThreadLocalScoped().Use<Session>();
			//services.AddSingleton<IEventStore, InMemoryEventStore>();
			For<IEventStore>().Singleton().Use<InMemoryEventStore>()
				.Ctor<IEventPublisher>("publisher")
				.Is("publisher", y =>
				{
					var publisher = y.GetInstance<IEventPublisher>();
					return publisher;
				});

			//services.AddSingleton<ICache, MemoryCache>();
			For<ICache>().Use<MemoryCache>();

			//services.AddSingleton<ICache, MemoryCache>();
			//services.AddScoped<IRepository>(y => new CacheRepository(new Repository(y.GetService<IEventStore>()), 
			//	y.GetService<IEventStore>(), 
			//	y.GetService<ICache>()));

			//IEventStore eventStore, ICache cache
			For<IRepository>().HybridHttpOrThreadLocalScoped().Use<CacheRepository>()
				.Ctor<Repository>("repository").Is("repository", x =>
				{
					var service = new Repository(x.GetInstance<IEventStore>());
					return service;
				})
				.Ctor<IEventStore>("eventStore").Is(z => z.GetInstance<IEventStore>())
				.Ctor<ICache>("cache").Is(c => c.GetInstance<ICache>());


			// Routes
			//Register routes
			//services.AddHttpContextAccessor(); 
			// No longer registered by default in ASP.NET Core 2.1
			//
			//
			//registrar.RegisterInAssemblyOf(typeof(InventoryCommandHandlers));
			For<InventoryCommandHandlers>().Use<InventoryCommandHandlers>()
				.Ctor<ISession>("session").Is(y => y.GetInstance<ISession>());




			//AutoMapper
			var profiles = from t in typeof(DefaultRegistry).Assembly.GetTypes()
						   where typeof(Profile).IsAssignableFrom(t)
						   select (Profile)Activator.CreateInstance(t);


			var config = new MapperConfiguration(cfg =>
			{
				//cfg.AddProfile(new CreateEmployeeRequestProfile());
				foreach (var profile in profiles)
				{
					cfg.AddProfile(profile);

				}
			});

			var mapper = config.CreateMapper();
			For<IMapper>().Use(mapper);

			//FluentValidation 
			FluentValidation.AssemblyScanner.FindValidatorsInAssemblyContaining<CreateEmployeeRequestValidator>()
				.ForEach(result =>
				{
					For(result.InterfaceType)
						.Use(result.ValidatorType);
				});

			//Read Model
			//Repositories
			For<IEmployeeRepository>().Use<EmployeeRepository>();
			//  For<ILocationRepository>().Use<LocationRepository>();

			//StackExchange.Redis
			ConfigurationOptions option = new ConfigurationOptions
			{
				AbortOnConnectFail = false,
				SyncTimeout = 1000,
				AllowAdmin = true,
				KeepAlive = 5,

				EndPoints = { "192.168.0.9" }
			};

			var multiplexer = ConnectionMultiplexer.Connect(option);

			//ConnectionMultiplexer multiplexer = RedisConnection.Connection;
			For<IConnectionMultiplexer>().Singleton().Use(multiplexer);
		}

		#endregion
	}
}