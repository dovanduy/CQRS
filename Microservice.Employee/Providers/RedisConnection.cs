using StackExchange.Redis;
using System;

namespace Microservice.Employee.Providers
{
	public static class RedisConnection
	{
		//private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
		//{
		//	//string conn = ConfigurationManager.AppSettings["cacheConnections"].ToString();

		//	ConfigurationOptions option = new ConfigurationOptions
		//	{
		//		AbortOnConnectFail = false,
		//		EndPoints = { "192.168.0.6" }
		//	};

		//	return ConnectionMultiplexer.Connect(option);
		//});

		//public static ConnectionMultiplexer Connection { get { return lazyConnection.Value; } }

		private static readonly Lazy<ConnectionMultiplexer> LazyConnection;

		static RedisConnection()
		{
			var configurationOptions = new ConfigurationOptions
			{
				AbortOnConnectFail = false,
				SyncTimeout = 1000,
				AllowAdmin = true,
				KeepAlive = 5,
				EndPoints = { "192.168.0.6:6379" }
			};

			LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
		}

		public static ConnectionMultiplexer Connection => LazyConnection.Value;

		public static IDatabase RedisCache => Connection.GetDatabase();

	}
}