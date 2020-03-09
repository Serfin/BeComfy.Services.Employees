using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Common.RabbitMq;
using BeComfy.Services.Employees.Core.Domain;
using BeComfy.Services.Employees.Core.Repositories;

namespace BeComfy.Services.Employees.Application.Commands.CommandHandlers
{
    public class CreateEmployeeHandler : ICommandHandler<CreateEmployee>
    {
        private readonly IEmployeesRepository _employeeRepository;

        public CreateEmployeeHandler(IEmployeesRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task HandleAsync(CreateEmployee command, ICorrelationContext context)
        {
            var employee = new Employee(command.Id, command.FirstName, command.SecondName, command.Surname,
                command.Birthday, command.EmployeeStatus, command.EmployeePosition);
            await _employeeRepository.AddAsync(employee);
        }
    }
}