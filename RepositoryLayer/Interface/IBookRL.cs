using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IBookRL
    {
        public BookModel AddBook(BookModel bookModel);
        public List<BookModel> GetAllBooks();
        public BookModel getBookById(long BookId);
        public BookModel UpdateBook(BookModel bookModel, long BookId);
        public bool deleteBook(long bookId);

    }
}
