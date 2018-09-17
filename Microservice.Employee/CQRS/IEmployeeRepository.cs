using Microservice.Employee.Models;
using Microservice.Kernel.Data;
using System.Collections.Generic;

namespace Microservice.Employee.CQRS
{
	public interface IEmployeeRepository : IBaseRepository<EmployeeRM>
	{
		IEnumerable<EmployeeRM> GetAll();
	}

}
