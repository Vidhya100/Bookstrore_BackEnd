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
    public class UserRL: IUserRL
    {
        private readonly IConfiguration iConfiguration;

        public UserRL(IConfiguration iconfiguration)
        {
            this.iConfiguration = iconfiguration;
        }

        public UserRegiModel Register(UserRegiModel userModel)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spRegister", con);
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.AddWithValue("@FullName", userModel.FullName);
                command.Parameters.AddWithValue("@EmailId", userModel.EmailId);
                command.Parameters.AddWithValue("@Password", userModel.Password);
                command.Parameters.AddWithValue("@MobileNumber", userModel.MobileNumber);

                con.Open();
                var result = command.ExecuteNonQuery();
                con.Close();

                if (result == 1)
                {
                    return userModel;
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

        //for login
        public UserLoginModel UserLogin(UserLoginModel userLoginModel)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spLogin", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmailId", userLoginModel.EmailId);
                command.Parameters.AddWithValue("@Password", userLoginModel.Password);

                con.Open();
                var resultcnt = (Int32)command.ExecuteScalar();//for taking single value
               

                if (resultcnt == 1)
                    return userLoginModel;
                else
                    con.Close();
                    return null;


            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
