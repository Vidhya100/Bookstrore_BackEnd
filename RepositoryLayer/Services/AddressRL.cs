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
    public class AddressRL:IAddressRL
    {
        private readonly IConfiguration iConfiguration;
        public AddressRL(IConfiguration iconfiguration)
        {
            this.iConfiguration = iconfiguration;
        }
        public string AddAddress(AddressModel address, int userId)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spAddAddress", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Address", address.Address);
                command.Parameters.AddWithValue("@City", address.City);
                command.Parameters.AddWithValue("@State", address.State);
                command.Parameters.AddWithValue("@TypeId", address.Type);
                command.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                var result = command.ExecuteNonQuery();
                con.Close();

                if (result != 0)
                {
                    return "Address added";
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
        
        public AddressModel UpdateAddress(AddressModel address, int userId)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spUpdateAddress", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AddressId", address.AddressId);
                command.Parameters.AddWithValue("@Address", address.Address);
                command.Parameters.AddWithValue("@City", address.City);
                command.Parameters.AddWithValue("@State", address.State);
                command.Parameters.AddWithValue("@TypeId", address.Type);
                command.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                var result = command.ExecuteNonQuery();
                con.Close();

                if (result != 0)
                {
                    return address;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool removeAddress(int addressId)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            
                try
                {
                    SqlCommand cmd = new SqlCommand("spDeleteAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AddressId ", addressId);
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result > 0)
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
        
        public List<AddressModel> GetAllAddress(int UserId)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                List<AddressModel> addressList = new List<AddressModel>();

                con.Open();
                String query = "SELECT AddressId, Address, City, State, TypeId FROM Address WHERE UserId = '" + UserId + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        AddressModel address = new AddressModel();
                        address.AddressId = Convert.ToInt32(rdr["Addressid"] == DBNull.Value ? default : rdr["Addressid"]);
                        address.Address = Convert.ToString(rdr["Address"] == DBNull.Value ? default : rdr["Address"]);
                        address.City = Convert.ToString(rdr["City"] == DBNull.Value ? default : rdr["City"]);
                        address.State = Convert.ToString(rdr["State"] == DBNull.Value ? default : rdr["State"]);
                        address.Type = Convert.ToInt32(rdr["TypeId"] == DBNull.Value ? default : rdr["TypeId"]);
                        addressList.Add(address);
                    }
                    return addressList;
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
