using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
