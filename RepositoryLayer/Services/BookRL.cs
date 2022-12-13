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
                    cmd.Parameters.AddWithValue("@Rating", bookModel.Rating);
                    cmd.Parameters.AddWithValue("@ReviewerCount", bookModel.ReviewerCount);
                    cmd.Parameters.AddWithValue("@DiscountPrice", bookModel.DiscountPrice);
                    cmd.Parameters.AddWithValue("@OriginalPrice", bookModel.OriginalPrice);
                    cmd.Parameters.AddWithValue("@BookDetail", bookModel.BookDetail);
                    cmd.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                    cmd.Parameters.AddWithValue("@BookQuantity", bookModel.BookQuantity);

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

                            bookModel.BookId = Convert.ToInt32(rdr["BookId"] == DBNull.Value ? default : rdr["BookId"]);
                            bookModel.BookName = Convert.ToString(rdr["BookName"] == DBNull.Value ? default : rdr["BookName"]);
                            bookModel.AuthorName = Convert.ToString(rdr["AuthorName"] == DBNull.Value ? default : rdr["AuthorName"]);
                            bookModel.Rating = Convert.ToDouble(rdr["Rating"] == DBNull.Value ? default : rdr["Rating"]);
                            bookModel.ReviewerCount = Convert.ToInt32(rdr["ReviewerCount"] == DBNull.Value ? default : rdr["ReviewerCount"]);
                            bookModel.DiscountPrice = Convert.ToInt32(rdr["DiscountPrice"] == DBNull.Value ? default : rdr["DiscountPrice"]);
                            bookModel.OriginalPrice = Convert.ToInt32(rdr["OriginalPrice"] == DBNull.Value ? default : rdr["OriginalPrice"]);
                            bookModel.BookDetail = Convert.ToString(rdr["BookDetail"] == DBNull.Value ? default : rdr["BookDetail"]);
                            bookModel.BookImage = Convert.ToString(rdr["BookImage"] == DBNull.Value ? default : rdr["BookImage"]);
                            bookModel.BookQuantity = Convert.ToInt32(rdr["BookQuantity"] == DBNull.Value ? default : rdr["BookQuantity"]);

                            bookList.Add(bookModel);
                        }
                    }
                    con.Close();
                    return bookList;
                }                
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
                    SqlCommand cmd = new SqlCommand("spGetBookById", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    con.Open();
                    BookModel bookModel = new BookModel();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            bookModel = new BookModel();

                           bookModel.BookId = Convert.ToInt32(rdr["BookId"] == DBNull.Value ? default : rdr["BookId"]);
                            bookModel.BookName = Convert.ToString(rdr["BookName"] == DBNull.Value ? default : rdr["BookName"]);
                            bookModel.AuthorName = Convert.ToString(rdr["AuthorName"] == DBNull.Value ? default : rdr["AuthorName"]);
                            bookModel.Rating = Convert.ToDouble(rdr["Rating"] == DBNull.Value ? default : rdr["Rating"]);
                            bookModel.ReviewerCount = Convert.ToInt32(rdr["ReviewerCount"] == DBNull.Value ? default : rdr["ReviewerCount"]);
                            bookModel.DiscountPrice = Convert.ToInt32(rdr["DiscountPrice"] == DBNull.Value ? default : rdr["DiscountPrice"]);
                            bookModel.OriginalPrice = Convert.ToInt32(rdr["OriginalPrice"] == DBNull.Value ? default : rdr["OriginalPrice"]);
                            bookModel.BookDetail = Convert.ToString(rdr["BookDetail"] == DBNull.Value ? default : rdr["BookDetail"]);
                            bookModel.BookImage = Convert.ToString(rdr["BookImage"] == DBNull.Value ? default : rdr["BookImage"]);
                            bookModel.BookQuantity = Convert.ToInt32(rdr["BookQuantity"] == DBNull.Value ? default : rdr["BookQuantity"]);
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
        public BookModel UpdateBook(BookModel bookModel)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
               try
                {
                    SqlCommand cmd = new SqlCommand("spUpdateBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId", bookModel.BookId);
                    cmd.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    cmd.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    cmd.Parameters.AddWithValue("@Rating", bookModel.Rating);
                    cmd.Parameters.AddWithValue("@ReviewerCount", bookModel.ReviewerCount);
                    cmd.Parameters.AddWithValue("@DiscountPrice", bookModel.DiscountPrice);
                    cmd.Parameters.AddWithValue("@OriginalPrice", bookModel.OriginalPrice);
                    cmd.Parameters.AddWithValue("@BookDetail", bookModel.BookDetail);
                    cmd.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                    cmd.Parameters.AddWithValue("@BookQuantity", bookModel.BookQuantity);

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
