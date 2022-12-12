using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL : IBookRL
    {
        private readonly IConfiguration iConfiguration;

        public BookRL(IConfiguration iConfiguration)
        {
            this.iConfiguration = iConfiguration;
        }

        //for addBook
        public BookModel AddBook(BookModel bookModel)
        {
            try
            {
                using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
                {
                    SqlCommand cmd = new SqlCommand("spAddBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    cmd.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    cmd.Parameters.AddWithValue("@Price", bookModel.Price);
                    cmd.Parameters.AddWithValue("@Description", bookModel.Description);
                    cmd.Parameters.AddWithValue("@Rating", bookModel.Rating);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return bookModel;
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //To View all employees details      
        public List<BookModel> GetAllBooks()
        {
            try
            {

                List<BookModel> bookList = new List<BookModel>();
                using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
                {


                    SqlCommand cmd = new SqlCommand("spGetAllBooks", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            BookModel bookModel = new BookModel();

                            bookModel.BookId = Convert.ToInt32(rdr["BookId"]);
                            bookModel.BookName = rdr["BookName"].ToString();
                            bookModel.AuthorName = rdr["AuthorName"].ToString();
                            bookModel.Price = Convert.ToInt32(rdr["Price"]);
                            bookModel.Description = rdr["Description"].ToString();
                            bookModel.Rating = rdr["Rating"].ToString();


                            bookList.Add(bookModel);
                        }
                    }
                }
                con.Close();
                return bookList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //for get book by id
        public BookModel getBookById(long BookId)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
               try
                {
                    SqlCommand cmd = new SqlCommand("spGetBookbyId", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    con.Open();
                    BookModel bookModel = new BookModel();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            bookModel.BookId = Convert.ToInt32(rd["BookId"]);
                            bookModel.BookName = rd["BookName"].ToString();
                            bookModel.AuthorName = rd["AuthorName"].ToString();
                            bookModel.Price = Convert.ToInt32(rd["Price"]);
                            bookModel.Description = rd["Description"].ToString();
                            bookModel.Rating = rd["Rating"].ToString();
                        }
                        return bookModel;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
        }
        public BookModel UpdateBook(BookModel bookModel, long BookId)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
                {
                    SqlCommand cmd = new SqlCommand("spUpdateBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    cmd.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    cmd.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    cmd.Parameters.AddWithValue("@Price", bookModel.Price);
                    cmd.Parameters.AddWithValue("@Description", bookModel.Description);
                    cmd.Parameters.AddWithValue("@Rating", bookModel.Rating);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    con.Close();
                    if (result != 0)
                    {
                        return bookModel;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
        }
        public bool deleteBook(long BookId)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
               try
                {
                    SqlCommand cmd = new SqlCommand("spDeleteBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
        }
    }
}
