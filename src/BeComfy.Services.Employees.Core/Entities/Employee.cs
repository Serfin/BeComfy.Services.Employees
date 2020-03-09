using System;
using BeComfy.Common.Mongo;
using BeComfy.Common.Types.Exceptions;
using BeComfy.Services.Employees.Core.Enumerators;

namespace BeComfy.Services.Employees.Core.Domain
{
    public class Employee : IEntity
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }
        public string Surname { get; private set; }
        public int Age { get; private set; }
        public DateTime Birthday { get; private set; }
        public EmployeeStatus EmployeeStatus { get; private set; }
        public EmployeePosition EmployeePosition { get; private set; }
        public Guid? CurrentFlight { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime? UpdatedAt { get; private set; }

        public Employee(Guid id, string firstName, string secondName, string surname, 
            DateTime birthday, EmployeeStatus employeeStatus, EmployeePosition employeePosition)
        {
            SetId(id);
            SetFirstName(firstName);
            SetSecondName(secondName);
            SetSurname(surname);
            SetBirthday(birthday);
            SetEmployeeStatus(employeeStatus);
            SetEmployeePosition(employeePosition);
            CurrentFlight = null;
            CreatedAt = DateTime.Now;
            SetUpdateDate();
        }

        private void SetId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new BeComfyException("employee_invalid_id", "Employee id cannot be empty");
            }

            Id = id;
        }

        private void SetFirstName(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new BeComfyException("employee_invalid_firstname", "Employee first name cannot be empty or null");
            }

            FirstName = firstName.Trim();
            SetUpdateDate();
        }

        private void SetSecondName(string secondName)
        {
            SecondName = secondName.Trim();
            SetUpdateDate();
        }

        private void SetSurname(string surname)
        {
            if (string.IsNullOrEmpty(surname))
            {
                throw new BeComfyException("employee_invalid_surname", "Employee surname cannot be empty or null");
            }

            Surname = surname.Trim();
            SetUpdateDate();
        }

        private void SetBirthday(DateTime birthday)
        {
            const int minimumRequiredAge = 18;
            var currentAge = (int)DateTime.Now.Subtract(birthday).TotalDays / 365;

            if (currentAge < minimumRequiredAge)
            {
                throw new BeComfyException("employee_invalid_age", "Terms of service does not accept employees under 18 yrs");
            }

            Birthday = birthday;
            Age = currentAge;
            SetUpdateDate();
        }

        private void SetEmployeeStatus(EmployeeStatus employeeStatus)
        {
            EmployeeStatus = employeeStatus;
            SetUpdateDate();
        }

        private void SetEmployeePosition(EmployeePosition employeePosition)
        {
            EmployeePosition = employeePosition;
            SetUpdateDate();
        }

        public void SetCurrentFlight(Guid? currentFlight)
        {
            if (currentFlight is null || currentFlight == Guid.Empty)
            {
                throw new BeComfyException("employee_invalid_currentflight", "Employee current flight cannot be null or empty");
            }

            CurrentFlight = currentFlight;
            SetUpdateDate();
        }

        private void SetUpdateDate()
            => UpdatedAt = DateTime.Now;
    }       
}