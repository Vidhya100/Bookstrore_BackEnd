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
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL iaddressBL;

        public AddressController(IAddressBL iaddressBL)
        {
            this.iaddressBL = iaddressBL;
        }
        
        [HttpPost]
        [Route("AddAddress")]
        public IActionResult AddAddress(AddressModel address)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = iaddressBL.AddAddress(address, userId);
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
        
        [HttpPut]
        [Route("UpdateAddress")]
        public IActionResult UpdateAddress(AddressModel address)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = iaddressBL.UpdateAddress(address, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Updated", Data = result });
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
        [Route("Delete")]
        public IActionResult removeAddress(int addressId)
        {
            try
            {
                var response = iaddressBL.removeAddress(addressId);
                if (response != null)
                {
                    return Ok(new { success = true, message = "Address Deleted" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Address not Deleted" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("Getalladdress")]
        public IActionResult GetAllAddress()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = iaddressBL.GetAllAddress(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Getting Address", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to get Address" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
