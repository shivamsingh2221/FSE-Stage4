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
    [AllowAnonymous]
    public class AnonymousUserController : ControllerBase
    {
        // GET: api/AnonymousUser
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return MenuItemOperation.GetConnection();
        }
    }
}
