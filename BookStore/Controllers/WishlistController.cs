using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistBL iwishlistBL;

        public WishlistController(IWishlistBL iwishlistBL)
        {
            this.iwishlistBL = iwishlistBL;
        }

        [Authorize]
        [HttpPost]
        [Route("AddToWishlist")]
        public IActionResult AddToWishlist(int bookId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iwishlistBL.AddToWishlist(bookId, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, message="Added to wishlist", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message="Failed to add", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw ;
            }
        }
        [HttpPost]
        [Route("DeleteFromWishlist")]
        public IActionResult RemoveFromWishlist(int wishlistId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = iwishlistBL.DeleteFromWishlist(wishlistId);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "Deleted from wishlist" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to Delete" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        [Route("GetFromWishlist")]
        public IActionResult GetWishlistItem()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = iwishlistBL.GetWishlistItem(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Data = result });
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
    }
}
