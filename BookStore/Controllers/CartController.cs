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
    public class CartController : ControllerBase
    {
        private readonly ICartBL icartBL;

        public CartController(ICartBL icartBL)
        {
            this.icartBL = icartBL;
        }
        [Authorize]
        [HttpPost]
        [Route("AddToCart")]
        public IActionResult AddToCart(int bookId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = icartBL.AddToCart(bookId, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, message = "Added to Cart" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "Failed to add", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("Updatecart")]
        public IActionResult UpdateCart(int cartId, int bookQty)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = icartBL.UpdateCart(cartId, bookQty);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Quantity updated" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to update" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Removefromcart")]
        public IActionResult RemoveFromCart(int cartId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = icartBL.RemoveFromCart(cartId);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "Removed from Cart" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to remove" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("Getcartitem")]
        public IActionResult GetCartItem()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = icartBL.GetCartItem(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Cart data", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to fetch" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
