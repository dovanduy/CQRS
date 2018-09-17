using Microservice.Kernel;
using System;

namespace Microservice.Employee.CQRS.WriteModel
{
	public interface IEmployeeCreatedEvent
	{
		int EmployeeID { get; set; }
		string FirstName { get; set; }
		string LastName { get; set; }
		DateTime DateOfBirth { get; set; }
		string JobTitle { get; set; }
	}

	public class EmployeeCreatedEvent : BaseEvent
	{
		public readonly int EmployeeID;
		public readonly string FirstName;
		public readonly string LastName;
		public readonly DateTime DateOfBirth;
		public readonly string JobTitle;

		public EmployeeCreatedEvent(Guid id, int employeeID, string firstName, string lastName, DateTime dateOfBirth, string jobTitle)
		{
			Id = id;
			EmployeeID = employeeID;
			FirstName = firstName;
			LastName = lastName;
			DateOfBirth = dateOfBirth;
			JobTitle = jobTitle;
		}
	}
}