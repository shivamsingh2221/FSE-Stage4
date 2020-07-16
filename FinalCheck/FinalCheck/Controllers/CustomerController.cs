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
    [Authorize(Roles = "Customer")]
    public class CustomerController : ControllerBase
    {
        // GET: api/Customer
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            DateTime dt = DateTime.Now;
            return MenuItemOperation.GetConnection().Where(p => p.Active == true && p.DateOfLaunch <= dt);

        }

        // GET: api/Customer/5
        [HttpGet("{userid}", Name = "Get Favorite")]
        public object Get(int userid)
        {
            int count = 0;
            List<Movie> list = new List<Movie>(MenuItemOperation.favoriteList(userid, ref count));

            return new { list, count };
        }

        // POST: api/Customer
        [HttpPost]
        public IActionResult Post([FromBody] List<Favorites> favorite)
        {
            MenuItemOperation.InsertIntoFavorites(favorite);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{favoriteid}")]
        public string Delete(int favoriteid)
        {
            return MenuItemOperation.Delete(favoriteid);
        }
    }
}
