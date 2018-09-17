using FluentValidation;
using System;
using System.Web.Http;

namespace Microservice.Employee.CQRS
{
	public class StructureMapValidatorFactory : ValidatorFactoryBase
	{
		private readonly HttpConfiguration _configuration;

		public StructureMapValidatorFactory(HttpConfiguration configuration)
		{
			_configuration = configuration;
		}

		public override IValidator CreateInstance(Type validatorType)
		{
			return _configuration.DependencyResolver.GetService(validatorType) as IValidator;
		}
	}
}