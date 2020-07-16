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
    [Authorize(Roles = "Customer")]
    public class CustomerController : ControllerBase
    {
        // GET: api/Customer
        [HttpGet]
        public IEnumerable<MenuItem> Get()
        {
            DateTime dt = DateTime.Now;
            return MenuItemOperation.GetConnection().Where(p => p.Active == true && p.DateOfLaunch <= dt);
        
        }

        // GET: api/Customer/5
        [HttpGet("{userid}", Name = "Get Customer")]
        public object Get(int userid)
        {
            int totalprice=0;
            List<MenuItem> list = new List<MenuItem>(MenuItemOperation.CartList(userid, ref totalprice));

            return new {list,totalprice };
        }

        // POST: api/Customer
        [HttpPost]
        public IActionResult Post([FromBody] List<Cart> cart)
        {
            MenuItemOperation.InsertIntoCart(cart);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{cartid}")]
        public string Delete(int cartid)
        {
            return MenuItemOperation.Delete(cartid);
        }
    }
}
