using BeComfy.Common.CqrsFlow.Dispatcher;
using Microsoft.AspNetCore.Mvc;

namespace BeComfy.Services.Employees.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : BaseController
    {
        public EmployeesController(IQueryDispatcher queryDispatcher) 
            : base(queryDispatcher)
        {

        }
    }
}