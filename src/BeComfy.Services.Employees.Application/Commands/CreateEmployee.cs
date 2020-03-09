using System;
using BeComfy.Common.CqrsFlow;
using BeComfy.Services.Employees.Core.Enumerators;
using Newtonsoft.Json;

namespace BeComfy.Services.Employees.Application.Commands
{
    public class CreateEmployee : ICommand
    {
        public Guid Id { get; }
        public string FirstName { get; }
        public string SecondName { get; }
        public string Surname { get; }
        public int Age { get; }
        public DateTime Birthday { get; }
        public EmployeeStatus EmployeeStatus { get; }
        public EmployeePosition EmployeePosition { get; }

        [JsonConstructor]
        public CreateEmployee(Guid id, string firstName, string secondName, string surname, 
            int age, DateTime birthday, EmployeeStatus employeeStatus, EmployeePosition employeePosition)
        {
            Id = id;
            FirstName = firstName;
            SecondName = secondName;
            Surname = surname;
            Age = age;
            Birthday = birthday;
            EmployeeStatus = employeeStatus;
            EmployeePosition = employeePosition;
        }
    }
}