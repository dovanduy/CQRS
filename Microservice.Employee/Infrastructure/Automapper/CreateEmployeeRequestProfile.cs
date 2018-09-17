using AutoMapper;
using Microservice.Employee.CQRS;
using Microservice.Employee.CQRS.WriteModel;

namespace Microservice.Employee.Infrastructure.Automapper
{
	public class CreateEmployeeRequestProfile : Profile
	{
		public CreateEmployeeRequestProfile()
		{
			this.CreateMap<CreateEmployeeRequest, CreateEmployeeCommand>()
				.ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth))
				.ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
				.ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
				.ForMember(dest => dest.EmployeeID, opts => opts.MapFrom(src => src.EmployeeID))
				.ForMember(dest => dest.JobTitle, opts => opts.MapFrom(src => src.JobTitle))
				.ReverseMap()
				.ForMember(dest => dest.LocationID, opts => opts.Ignore());

		}
	}
}