using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Authorize(Roles = Role.Users)]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackBL iFeedBackBL;

        public FeedBackController(IFeedBackBL iFeedBackBL)
        {
            this.iFeedBackBL = iFeedBackBL;
        }
        
        [HttpPost]
        [Route("Addfeedback")]
        public IActionResult AddFeedback(FeedbackModel feedback)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = iFeedBackBL.AddFeedback(feedback, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Data = result });
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
        [Route("Getfeedback")]
        public IActionResult GetFeedback(int bookId)
        {
            try
            {
                var result = iFeedBackBL.GetFeedback(bookId);
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
                throw ;
            }
        }
    }
}
