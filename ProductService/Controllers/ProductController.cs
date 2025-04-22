using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Models;
//using ProductService.Models;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ProductDbServiceContext _context) : ControllerBase
    {

        [HttpGet("GetAllProduct")]
        public async Task<ActionResult> GetAllProduct()
        {
         
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult> GetProductById([FromRoute] int id)
        {
            return Ok(await _context.Products.FindAsync(id));
        }
    }
}


