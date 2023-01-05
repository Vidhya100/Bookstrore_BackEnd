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
    public class CartRL: ICartRL
    {
        private readonly IConfiguration iConfiguration;

        public CartRL(IConfiguration iConfiguration)
        {
            this.iConfiguration = iConfiguration;
        }
        public CartModel AddToCart(int bookId, int userId)
        {
            CartModel cartModel = new CartModel();
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spAddToCart", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BookId", bookId);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@BookQuantity", cartModel.BookQuantity);

                con.Open();
                var result = command.ExecuteNonQuery();
                con.Close();

                if (result > 0)
                {
                    return cartModel;
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
        public string UpdateCart(int cartId, int bookQty)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spUpdateCart", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BookQuantity", bookQty);
                command.Parameters.AddWithValue("@CartId", cartId);

                con.Open();
                var result = command.ExecuteNonQuery();
                con.Close();

                if (result != 0)
                {
                    return "Quantity updated";
                }
                else
                {
                    return "Failed to update";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveFromCart(int cartId)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spRemoveFromCart", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CartId", cartId);

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

        public List<CartModel> GetCartItem(int userId)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                List<CartModel> cartList = new List<CartModel>();
                SqlCommand command = new SqlCommand("spGetAllCartItem", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                SqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        CartModel cart = new CartModel();
                        cart.BookId = Convert.ToInt32(rdr["BookId"] == DBNull.Value ? default : rdr["BookId"]);
                        cart.UserId = Convert.ToInt32(rdr["UserId"] == DBNull.Value ? default : rdr["UserId"]);
                        cart.CartId = Convert.ToInt32(rdr["CartId"] == DBNull.Value ? default : rdr["CartId"]);
                        cart.BookName = Convert.ToString(rdr["BookName"] == DBNull.Value ? default : rdr["BookName"]);
                        cart.AuthorName = Convert.ToString(rdr["AuthorName"] == DBNull.Value ? default : rdr["AuthorName"]);
                        cart.BookImage = Convert.ToString(rdr["BookImage"] == DBNull.Value ? default : rdr["BookImage"]);
                        //this string added for taking image from assets folder
                        cart.BookImage = "../../../assets/" + cart.BookImage;
                        cart.DiscountPrice = Convert.ToInt32(rdr["DiscountPrice"] == DBNull.Value ? default : rdr["DiscountPrice"]);
                        cart.OriginalPrice = Convert.ToInt32(rdr["OriginalPrice"] == DBNull.Value ? default : rdr["OriginalPrice"]);
                        cart.BookQuantity = Convert.ToInt32(rdr["BookQuantity"] == DBNull.Value ? default : rdr["BookQuantity"]);
                        cartList.Add(cart);
                    }
                    return cartList;
                }
                else
                {
                    con.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
