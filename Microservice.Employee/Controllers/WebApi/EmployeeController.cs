using AutoMapper;
using CQRSlite.Commands;
using CQRSlite.Domain;
using Microservice.Employee.CQRS;
using Microservice.Employee.CQRS.WriteModel;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Microservice.Employee.Controllers.WebApi
{
	[RoutePrefix("employees")]
	public class EmployeeController : ApiController
	{
		private readonly IEmployeeRepository _employeeRepo;
		private IMapper _mapper;
		private ICommandSender _commandSender;

		public EmployeeController(IEmployeeRepository employeeRepo)
		{
			_employeeRepo = employeeRepo;
		}

		public EmployeeController(IEmployeeRepository employeeRepo,
								  IMapper mapper,
			IRepository repo,
								  ICommandSender commandSender)
		{
			_employeeRepo = employeeRepo;
			_mapper = mapper;
			_commandSender = commandSender;
		}

		[HttpGet]
		[Route("{id}")]
		public IHttpActionResult GetByID(int id)
		{
			var employee = _employeeRepo.GetByID(id);

			//It is possible for GetByID() to return null.
			//If it does, we return HTTP 400 Bad Request
			if (employee == null)
			{
				return BadRequest("No Employee with ID " + id.ToString() + " was found.");
			}

			//Otherwise, we return the employee
			return Ok(employee);
		}


		[HttpGet]
		[Route("all")]
		public IHttpActionResult GetAll()
		{
			var employees = _employeeRepo.GetAll();
			return Ok(employees);
		}


		[HttpPost]
		[Route("create")]
		public async Task<IHttpActionResult> Create(CreateEmployeeRequest request)
		{
			var command = _mapper.Map<CreateEmployeeCommand>(request);
			command.Id = Guid.NewGuid();
			await _commandSender.Send(command);

			return Ok();
		}
	}
}
