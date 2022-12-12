using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL ibookRL;

        public BookBL(IBookRL ibookRL)
        {
            this.ibookRL = ibookRL;
        }

        public BookModel AddBook(BookModel bookModel)
        {
            try
            {
                return ibookRL.AddBook(bookModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public List<BookModel> GetAllBooks()
        {
            try
            {
                return ibookRL.GetAllBooks();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public BookModel getBookById(long BookId)
        {
            try
            {
                return ibookRL.getBookById(BookId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public BookModel UpdateBook(BookModel bookModel, long BookId)
        {
            try
            {
                return ibookRL.UpdateBook(bookModel, BookId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool deleteBook(long BookId)
        {
            try
            {
                return ibookRL.deleteBook(BookId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
