using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeCheck.Models;

namespace PracticeCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
       
        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id,[FromBody]string password)
        {
            List<User> list = MenuItemOperation.UserList();
            bool user = list.Any(p => p.Id == id && p.Password == password);
              if (user == true)
                  return "true";
              return "falseSubmission";   
        }

        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            MenuItemOperation.Insert(user);
            return Ok();
        }
    }
}
