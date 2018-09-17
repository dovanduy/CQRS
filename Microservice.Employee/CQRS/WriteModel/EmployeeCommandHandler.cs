namespace Microservice.Employee.CQRS.WriteModel
{

	// The ISession object is provided by CQRSLite and acts as a gateway
	//into the data loaded into our Event Store.
	//It is similar to Entity Framework's DataContext class, and so we use it in a similar manner.

	//public class EmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand>
	//{
	//	private readonly ISession _session;

	//	public EmployeeCommandHandler(ISession session)
	//	{
	//		_session = session;
	//	}

	//	public Task Handle(CreateEmployeeCommand command)
	//	{
	//		return Task.Run(() =>
	//		{
	//			Models.Employee employee = new Models.Employee(command.Id, command.EmployeeID, command.FirstName, command.LastName,
	//				command.DateOfBirth, command.JobTitle);
	//			_session.Add(employee);
	//			_session.Commit();
	//		});

	//	}


	//}
}