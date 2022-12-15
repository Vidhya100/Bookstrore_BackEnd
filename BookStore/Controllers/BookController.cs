using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL ibookBL;

        public BookController(IBookBL ibookBL)
        {
            this.ibookBL = ibookBL;
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        [Route("AddBooks")]
        public IActionResult AddBook(BookModel bookModel)
        {
            try
            {
                var result = ibookBL.AddBook(bookModel);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Book added successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book not get added" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        [HttpPost]
        [Route("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = ibookBL.GetAllBooks();
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Books fetched successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Books not getting fetched" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //for get book  by id
        [HttpGet]
        [Route("GetById")]
        public IActionResult getBookById(long BookId)
        {
            try
            {
                var reg = ibookBL.getBookById(BookId);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Book Details", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to get details" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateBooks(BookModel bookModel, long BookId)
        {
            try
            {
                var result = ibookBL.UpdateBook(bookModel, BookId);
                if (result != null)

                {
                    return this.Ok(new { Success = true, message = "Book  Updated Sucessfull", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Book details not updated" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpDelete]
        [Route("Delete")]
        public IActionResult deleteBook(long BookId)
        {
            try
            {
                var result = ibookBL.deleteBook(BookId);
                if (result != null)

                {
                    return this.Ok(new { Success = true, message = "Book Deleted Sucessfull", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to delete" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
