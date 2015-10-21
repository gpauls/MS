using Microsoft.AspNet.Mvc;
using MS.Employees.Commands;
using MS.Employees.Services;
using MS.Infrastructure.Messaging;
using MS.Web.Models;

namespace MS.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IEmployeeQuery _employeeQuery;

        public EmployeeController(ICommandBus commandBus, IEmployeeQuery employeeQuery)
        {
            _commandBus = commandBus;
            _employeeQuery = employeeQuery;
        }

        public IActionResult List()
        {
            return Json(_employeeQuery.List());
        }

        public IActionResult Get(long id)
        {
            return Json(_employeeQuery.Get(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody]EmployeeCreateModel model)
        {
            var response = _commandBus.Execute<EmployeeUpdateCommand, EmployeeUpdateResponse>(new EmployeeUpdateCommand
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            });

            return response.Succeeded ? Json("ok") : Json("error");
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody] EmployeeUpdateModel model)
        {
            var response = _commandBus.Execute<EmployeeCreateCommand, EmployeeCreateResponse>(new EmployeeCreateCommand
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            });

            return response.Succeeded ? Json("ok") : Json("error");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var response = _commandBus.Execute<EmployeeDeleteCommand, EmployeeDeleteResponse>(new EmployeeDeleteCommand
            {
                Id = id
            });

            return response.Succeeded ? Json("ok") : Json("error");
        }
    }
}
