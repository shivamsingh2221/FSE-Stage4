using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Handson_4.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Handson_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private static List<Employee> _emp = new List<Employee>();
        // GET: api/<ValuesController1>
        [HttpGet(Name = "GetAllStudent")]
        public IActionResult Get()
        {
            return new ObjectResult(_emp);
        }

        // GET api/<ValuesController1>/5
        [HttpGet("{id}", Name = "GetStudent")]
        public IActionResult Get(int id)
        {
            return new ObjectResult(_emp.FirstOrDefault(p => p.Id == id));
        }

        // POST api/<ValuesController1>
        [HttpPost(Name = "CreateStudent")]
        public IActionResult Post([FromBody]Employee emps)
        {
            _emp.Add(emps);
            return CreatedAtRoute("GetStudent", new { id = emps.Id }, emps);
        }

        // PUT api/<ValuesController1>/5
        [HttpPut("{id}", Name = "UpdateStudent")]
        public IActionResult Put(int id, [FromBody]Employee emps)
        {
            _emp.FirstOrDefault(p => p.Id == id).Name = emps.Name;
            return CreatedAtRoute("GetStudent", new { id = emps.Id }, emps);
        }

        // DELETE api/<ValuesController1>/5
        [HttpDelete("{id}", Name = "DeleteStudent")]
        public IActionResult Delete(int id)
        {
            var _emps = _emp.FirstOrDefault(p => p.Id == id);
            _emp.Remove(_emps);
            return new NoContentResult();
        }
    }
}
