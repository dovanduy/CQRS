using Microservice.Kernel;
using System;

namespace Microservice.Employee.CQRS.WriteModel
{
	public class CreateEmployeeCommand : BaseCommand
	{
		public int EmployeeID;
		public string FirstName;
		public string LastName;
		public DateTime DateOfBirth;
		public string JobTitle;
		//public readonly string Location;


		public CreateEmployeeCommand()
		{
		}

		public CreateEmployeeCommand(Guid id, int employeeID, string firstName, string lastName, DateTime dateOfBirth, string jobTitle)
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