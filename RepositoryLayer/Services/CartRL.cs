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
        public CartModel AddToCart(CartModel  cartModel, int userId)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spAddToCart", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BookInCart", cartModel.BookInCart);
                command.Parameters.AddWithValue("@BookId", cartModel.BookId);
                command.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                var result = command.ExecuteNonQuery();
                con.Close();

                if (result == 1)
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
    }
}
