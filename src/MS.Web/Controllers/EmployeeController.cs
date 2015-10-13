using System.Web.Http;
using MS.Employees.Commands;
using MS.Infrastructure.Messaging;
using MS.Web.Models;

namespace MS.Web.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly ICommandBus _commandBus;

        public EmployeeController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        // GET api/values
        public IHttpActionResult Get()
        {
            return Ok(new[] { "value1", "value2" });
        }

        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            return Ok("value");
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]EmployeeCreateModel model)
        {
            var response = _commandBus.Execute<CreateEmployeeCommand, CreateEmployeeResponse>(new CreateEmployeeCommand
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            });

            return response.Succeeded ? Ok() : BadRequest(response.Errors) as IHttpActionResult;
        }

        // PUT api/values/5
        public IHttpActionResult Put(int id, [FromBody]string value)
        {
            return Ok();
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
