using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentService.Models;
//using PaymentService.Models;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(PaymentDbServiceContext _context) : ControllerBase
    {

        [HttpGet("")]
        public async Task<ActionResult> GetTModels()
        {

            return Ok(await _context.Payments.ToListAsync());
        }

     
    }
}