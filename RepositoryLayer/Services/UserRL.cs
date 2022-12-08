using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        public string UserLogin(UserLoginModel userLoginModel)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                //LoginTokenModel loginTokenModel = new LoginTokenModel();
                SqlCommand command = new SqlCommand("spLogin", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmailId", userLoginModel.EmailId);
                command.Parameters.AddWithValue("@Password", userLoginModel.Password);

                
                con.Open();

                SqlDataReader rd = command.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        userLoginModel.EmailId = Convert.ToString(rd["EmailId"] == DBNull.Value ? default : rd["EmailId"]);
                        userLoginModel.Password = Convert.ToString(rd["Password"] == DBNull.Value ? default : rd["Password"]);
                    }
                    var token = this.GenerateSecurityToken(userLoginModel.EmailId);
                    return token;
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

        //JWT token
        public string GenerateSecurityToken(string email)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(this.iConfiguration[("JWT:Key")]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Email, email),
                    //new Claim("UserId", UserId.ToString())
                }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
