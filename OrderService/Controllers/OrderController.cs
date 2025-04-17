using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Models;
using OrderService.ViewModel;
//using ProductService.Models;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(OrderDbServiceContext _context) : ControllerBase
    {

        [HttpGet("GetAllOrder")]
        public async Task<ActionResult> GetAllOrder()
        {
         
            return Ok(await _context.Orders.ToListAsync());
        }



        [HttpPost("addOrder")]
        public async Task<ActionResult> AddOrder(OrderRequest order)
        {
            //Thêm cơ sở dữ liệu cho order
            try {
                _context.Database.BeginTransaction();
                Order model = new Order();
                model.UserId = order.UserId;
                model.Total = order.Price * order.Quantity;
                model.CreatedAt = DateTime.Now;
                _context.Orders.Add(model);
                _context.SaveChanges();
                
                OrderItem detail = new OrderItem();
                detail.OrderId = model.Id;
                detail.ProductId = order.ProductId;
                detail.Quantity = order.Quantity;
                detail.UnitPrice = order.Price;
                _context.OrderItems.Add(detail);
                _context.SaveChanges();
                //Cấu hình kafka đưa dữ liệu lên topic
                

                
                _context.Database.CommitTransaction();


            }catch(Exception ex) {
                _context.Database.RollbackTransaction();
                Console.WriteLine(ex.Message);
            }

            //Đồng thời bắn message lên kafka reducer
        
            return Ok("Đặt hàng thành công");
        }
        
    }
}