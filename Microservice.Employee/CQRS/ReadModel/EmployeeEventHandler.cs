using AutoMapper;
using CQRSlite.Events;
using Microservice.Employee.CQRS.WriteModel;
using Microservice.Employee.Models;
using System.Threading.Tasks;

namespace Microservice.Employee.CQRS.ReadModel
{
	public class EmployeeEventHandler : IEventHandler<EmployeeCreatedEvent>
	{
		private readonly IMapper _mapper;
		private readonly IEmployeeRepository _employeeRepo;

		public EmployeeEventHandler(IMapper mapper, IEmployeeRepository employeeRepo)
		{
			_mapper = mapper;
			_employeeRepo = employeeRepo;
		}

		public Task Handle(EmployeeCreatedEvent message)
		{
			return Task.Run(() =>
			{
				EmployeeRM employee = _mapper.Map<EmployeeRM>(message);
				_employeeRepo.Save(employee);
			});

		}
	}
}