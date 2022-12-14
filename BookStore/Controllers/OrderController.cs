using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL iOrderBL;

        public OrderController(IOrderBL iOrderBL)
        {
            this.iOrderBL = iOrderBL;
        }
        [Authorize]
        [HttpPost]
        [Route("Placeorder")]
        public IActionResult AddOrder(OrderModel orderModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = iOrderBL.AddOrder(orderModel, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Placed" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to add" });
                }
            }
            catch (Exception ex)
            {
                throw ;
            }
        }
        [HttpGet]
        [Route("Getorders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = iOrderBL.GetAllOrders(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Details", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to get" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        [HttpDelete]
        [Route("DeleteOrder")]
        public IActionResult RemoveOrder(int orderId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = iOrderBL.RemoveOrder(orderId);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "Deleted" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to delete" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
