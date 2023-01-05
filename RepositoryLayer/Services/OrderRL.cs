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
    public class OrderRL : IOrderRL
    {
        private readonly IConfiguration iConfiguration;
        public OrderRL(IConfiguration iconfiguration)
        {
            this.iConfiguration = iconfiguration;
        }

        public string AddOrder(OrderModel orderModel, int userId)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spAddOrder", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AddressId", orderModel.AddressId);
                command.Parameters.AddWithValue("@BookId", orderModel.BookId);
                command.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                var result = command.ExecuteNonQuery();
                con.Close();

                if (result != 0)
                {
                    return "Order Placed";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ;
            }
        }
        
        public List<OrderModel> GetAllOrders(int userId)
        {
            using SqlConnection connection = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                List<OrderModel> orderList = new List<OrderModel>();
                SqlCommand command = new SqlCommand("spGetAllOrders", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderModel order = new OrderModel();
                        order.OrderId = Convert.ToInt32(reader["OrderId"] == DBNull.Value ? default : reader["OrderId"]);
                        order.BookId = Convert.ToInt32(reader["BookId"] == DBNull.Value ? default : reader["BookId"]);
                        order.UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
                        order.AddressId = Convert.ToInt32(reader["AddressId"] == DBNull.Value ? default : reader["AddressId"]);
                        order.BookName = Convert.ToString(reader["BookName"] == DBNull.Value ? default : reader["BookName"]);
                        order.AuthorName = Convert.ToString(reader["AuthorName"] == DBNull.Value ? default : reader["AuthorName"]);
                        order.BookImage = Convert.ToString(reader["BookImage"] == DBNull.Value ? default : reader["BookImage"]);
                        //this string added for taking image from assets folder
                        order.BookImage = "../../../assets/" + order.BookImage;
                        order.TotalPrice = Convert.ToDouble(reader["TotalPrice"] == DBNull.Value ? default : reader["TotalPrice"]);
                        order.OrderQty = Convert.ToInt32(reader["OrderQty"] == DBNull.Value ? default : reader["OrderQty"]);
                        order.OrderDate = Convert.ToDateTime(reader["OrderDate"] == DBNull.Value ? default : reader["OrderDate"]);
                        
                        orderList.Add(order);
                    }
                    return orderList;
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ;
            }
        }
        
        public bool RemoveOrder(int orderId)
        {
            using SqlConnection connection = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spRemoveOrder", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@OrderId", orderId);

                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();

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
                throw ;
            }
        }
    }
}
