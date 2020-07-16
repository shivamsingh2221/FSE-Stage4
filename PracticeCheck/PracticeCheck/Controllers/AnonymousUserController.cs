using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeCheck.Models;

namespace PracticeCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AnonymousUserController : ControllerBase
    {
        // GET: api/AnonynousUser
        [HttpGet]
        public IEnumerable<MenuItem> Get()
        {
           return MenuItemOperation.GetConnection();
        }
    }
}
