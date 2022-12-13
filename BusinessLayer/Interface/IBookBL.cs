using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBookBL
    {
        public BookModel AddBook(BookModel bookModel);
        public List<BookModel> GetAllBooks();
        public BookModel getBookById(long BookId);
        public BookModel UpdateBook(BookModel bookModel);
        public bool deleteBook(long bookId);

    }
}
