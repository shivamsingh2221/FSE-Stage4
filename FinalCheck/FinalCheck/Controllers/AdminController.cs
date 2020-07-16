using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCheck.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        // GET: api/Admin
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return MenuItemOperation.GetConnection();
        }

        // PUT: api/Admin/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movie movieitem)
        {
            MenuItemOperation.Update(id, movieitem);
            return Ok();
        }
    }
}
