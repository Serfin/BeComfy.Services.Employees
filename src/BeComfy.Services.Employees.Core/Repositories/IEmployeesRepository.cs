using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BeComfy.Services.Employees.Core.Domain;

namespace BeComfy.Services.Employees.Core.Repositories
{
    public interface IEmployeesRepository
    {
        Task AddAsync(Employee employee);
        Task<Employee> GetAsync(Guid id);
        Task<IEnumerable<Employee>> BrowseAsync(int pageSize, int page);
        Task<IEnumerable<Employee>> BrowseAsync(int pageSize, int page, 
            Expression<Func<Employee, bool>> predicate);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Guid id);
    }
}