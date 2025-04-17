using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IdentityService.Models;
//using ProductService.Models;

namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IdentityDbServiceContext _context) : ControllerBase
    {

        [HttpGet("GetAllUser")]
        public async Task<ActionResult> GetAllUser()
        {
         
            return Ok(await _context.Users.ToListAsync());
        }

    }
}