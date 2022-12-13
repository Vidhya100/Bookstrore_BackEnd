using BusinessLayer.Interface;
using CommonLayer.Model;
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
        [HttpPost]
        [Route("Addtocart")]
        public IActionResult AddToCart(CartModel cartModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = icartBL.AddToCart(cartModel,userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Added to Cart", Data = result });
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
    }
}
