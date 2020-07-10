using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Common.RabbitMq;
using BeComfy.Common.Types.Enums;
using Core = BeComfy.Services.Employees.Core.Enumerators;
using BeComfy.Services.Employees.Core.Domain;
using BeComfy.Services.Employees.Core.Repositories;
using BeComfy.Common.Types.Exceptions;

namespace BeComfy.Services.Employees.Application.Events.EventHandlers
{
    public class AirplaneReservedHandler : IEventHandler<AirplaneReserved>
    {
        private readonly IEmployeesRepository _employeeRepository;

        public AirplaneReservedHandler(IEmployeesRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task HandleAsync(AirplaneReserved @event, ICorrelationContext context)
        {
            var pilots = await _employeeRepository.GetManyAsync(x => (int)x.EmployeePosition == (int)EmployeePosition.Pilot && 
                (int)x.EmployeeStatus == (int)EmployeeStatus.Available, 
                @event.RequiredCrew[EmployeePosition.Pilot]);

            if (pilots.Count < @event.RequiredCrew[EmployeePosition.Pilot])
            {
                throw new BeComfyException("not_enough_pilots_in_crew", "There is not enough available pilots to handle flight!");
            }

            var staff = await _employeeRepository.GetManyAsync(x => (int)x.EmployeePosition == (int)EmployeePosition.Staff && 
                (int)x.EmployeeStatus == (int)EmployeeStatus.Available, 
                @event.RequiredCrew[EmployeePosition.Staff]);

            if (staff.Count < @event.RequiredCrew[EmployeePosition.Staff])
            {
                throw new BeComfyException("not_enough_staff_in_crew", "There is not enough available staff members to handle flight!");
            }

            var crew = new List<Employee>();
            crew.AddRange(pilots);
            crew.AddRange(staff);

            foreach (var employee in crew)
            {
                employee.SetCurrentFlight(@event.FlightId);
                employee.SetEmployeeStatus(Core.Enumerators.EmployeeStatus.NotAvailalbe);

                await _employeeRepository.UpdateAsync(employee);
            }
        }
    }
}