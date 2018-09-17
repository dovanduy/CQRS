using CQRSlite.Commands;
using CQRSlite.Domain;
using System.Threading.Tasks;

namespace Microservice.Employee.CQRS.WriteModel.Handlers
{
	public class InventoryCommandHandlers : ICommandHandler<CreateEmployeeCommand>

	{
		private readonly ISession _session;

		public InventoryCommandHandlers(ISession session)
		{
			_session = session;
		}

		public async Task Handle(CreateEmployeeCommand message)
		{
			var item = new Models.Employee(message.Id,
										   message.EmployeeID,
										   message.FirstName,
										   message.LastName,
										   message.DateOfBirth,
										   message.JobTitle);
			await _session.Add(item);
			await _session.Commit();
		}

	}
}