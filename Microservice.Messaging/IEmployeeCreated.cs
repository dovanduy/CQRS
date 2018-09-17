using System;

namespace Microservice.Messaging
{
	public interface IEmployeeCreated
	{
		int EmployeeID { get; set; }
		string FirstName { get; set; }
		string LastName { get; set; }
		DateTime DateOfBirth { get; set; }
		string JobTitle { get; set; }
	}
}
