using CommonLayer.Model;
using LanguageExt;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class WishlistRL: IWishlistRL
    {
        private readonly IConfiguration iConfiguration;

        public WishlistRL(IConfiguration iConfiguration)
        {
            this.iConfiguration = iConfiguration;
        }
        public string AddToWishlist(int bookId, int userId)
        {
            WishlistModel model = new WishlistModel();  
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spAddToWishlist", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BookId", bookId);
                command.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                var result = command.ExecuteNonQuery();
                con.Close();

                if (result > 0)
                {
                    return "added to wishlist";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteFromWishlist(int wishlistId)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spRemoveFromWishlist", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@WishlistId", wishlistId);

                con.Open();
                var result = command.ExecuteNonQuery();
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<WishlistModel> GetWishlistItem(int userId)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                List<WishlistModel> list = new List<WishlistModel>();
                SqlCommand command = new SqlCommand("spGetAllWishlistItem", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                SqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        WishlistModel wish = new WishlistModel();
                        wish.BookId = Convert.ToInt32(rdr["BookId"] == DBNull.Value ? default : rdr["BookId"]);
                        wish.UserId = Convert.ToInt32(rdr["UserId"] == DBNull.Value ? default : rdr["UserId"]);
                        wish.WishlistId = Convert.ToInt32(rdr["WishlistId"] == DBNull.Value ? default : rdr["WishlistId"]);
                        //wish.BookName = Convert.ToString(rdr["BookName"] == DBNull.Value ? default : rdr["BookName"]);
                        //wish.AuthorName = Convert.ToString(rdr["AuthorName"] == DBNull.Value ? default : rdr["AuthorName"]);
                        //wish.Description = Convert.ToString(rdr["Description"] == DBNull.Value ? default : rdr["Description"]);
                        //wish.Price = Convert.ToString(rdr["Price"] == DBNull.Value ? default : rdr["Price"]);
                        //wish.Rating = Convert.ToString(rdr["Rating"] == DBNull.Value ? default : rdr["Rating"]);
                        list.Add(wish);
                    }
                    return list;
                }
                else
                {
                    con.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ;
            }
        }
    }
}
