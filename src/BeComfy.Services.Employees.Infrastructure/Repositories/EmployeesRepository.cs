using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BeComfy.Common.Mongo;
using BeComfy.Services.Employees.Core;
using BeComfy.Services.Employees.Core.Domain;
using BeComfy.Services.Employees.Core.Repositories;

namespace BeComfy.Services.Employees.Infrastructure.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly IMongoRepository<Employee> _collection;

        public EmployeesRepository(IMongoRepository<Employee> collection)
        {
            _collection = collection;
        }

        public async Task AddAsync(Employee employee)
            => await _collection.AddAsync(employee);

        public async Task<IEnumerable<Employee>> BrowseAsync(int pageSize, int page)
            => await _collection.BrowseAsync(pageSize, page);

        public async Task<IEnumerable<Employee>> BrowseAsync(int page, int pageSize, Expression<Func<Employee, bool>> predicate)
            => await _collection.BrowseAsync(pageSize, page, predicate);

        public async Task DeleteAsync(Guid id)
            => await _collection.DeleteAsync(id);

        public async Task<Employee> GetAsync(Guid id)
            => await _collection.GetAsync(x => x.Id == id);

        public async Task UpdateAsync(Employee employee)
            => await _collection.UpdateAsync(employee);
    }
}