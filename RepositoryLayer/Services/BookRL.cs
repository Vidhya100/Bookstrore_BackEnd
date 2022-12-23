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
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            BookModel bookModel = new BookModel();

                            bookModel.BookId = Convert.ToInt32(rd["BookId"] == DBNull.Value ? default : rd["BookId"]);
                            bookModel.BookName = Convert.ToString(rd["BookName"] == DBNull.Value ? default : rd["BookName"]);
                            bookModel.AuthorName = Convert.ToString(rd["AuthorName"] == DBNull.Value ? default : rd["AuthorName"]);
                            bookModel.Rating = Convert.ToDouble(rd["Rating"] == DBNull.Value ? default : rd["Rating"]);
                            bookModel.ReviewerCount = Convert.ToInt32(rd["ReviewerCount"] == DBNull.Value ? default : rd["ReviewerCount"]);
                            bookModel.DiscountPrice = Convert.ToInt32(rd["DiscountPrice"] == DBNull.Value ? default : rd["DiscountPrice"]);
                            bookModel.OriginalPrice = Convert.ToInt32(rd["OriginalPrice"] == DBNull.Value ? default : rd["OriginalPrice"]);
                            bookModel.BookDetail = Convert.ToString(rd["BookDetail"] == DBNull.Value ? default : rd["BookDetail"]);
                            bookModel.BookImage = Convert.ToString(rd["BookImage"] == DBNull.Value ? default : rd["BookImage"]);
                            //if(bookModel.BookId==11)
                            //this string added for taking image from assets folder
                                bookModel.BookImage = "../../../assets/"+ bookModel.BookImage;
                            bookModel.BookQuantity = Convert.ToInt32(rd["BookQuantity"] == DBNull.Value ? default : rd["BookQuantity"]);


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
                            bookModel.BookId = Convert.ToInt32(rd["BookId"] == DBNull.Value ? default : rd["BookId"]);
                            bookModel.BookName = Convert.ToString(rd["BookName"] == DBNull.Value ? default : rd["BookName"]);
                            bookModel.AuthorName = Convert.ToString(rd["AuthorName"] == DBNull.Value ? default : rd["AuthorName"]);
                            bookModel.Rating = Convert.ToDouble(rd["Rating"] == DBNull.Value ? default : rd["Rating"]);
                            bookModel.ReviewerCount = Convert.ToInt32(rd["ReviewerCount"] == DBNull.Value ? default : rd["ReviewerCount"]);
                            bookModel.DiscountPrice = Convert.ToInt32(rd["DiscountPrice"] == DBNull.Value ? default : rd["DiscountPrice"]);
                            bookModel.OriginalPrice = Convert.ToInt32(rd["OriginalPrice"] == DBNull.Value ? default : rd["OriginalPrice"]);
                            bookModel.BookDetail = Convert.ToString(rd["BookDetail"] == DBNull.Value ? default : rd["BookDetail"]);
                            bookModel.BookImage = Convert.ToString(rd["BookImage"] == DBNull.Value ? default : rd["BookImage"]);
                            bookModel.BookQuantity = Convert.ToInt32(rd["BookQuantity"] == DBNull.Value ? default : rd["BookQuantity"]);
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
