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
        public static string Key = "vidhya@@kfxcbv@";

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

                //var encrypt = ConvertoEncrypt(userModel.Password);
                command.Parameters.AddWithValue("@FullName", userModel.FullName);
                command.Parameters.AddWithValue("@EmailId", userModel.EmailId);
                command.Parameters.AddWithValue("@Password", userModel.Password);
                command.Parameters.AddWithValue("@MobileNumber", userModel.MobileNumber);
                //var dPass = ConvertoDecrypt(encrypt);
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
        //for password encryption and decription
        public static string ConvertoEncrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
                return "";
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }

        public static string ConvertoDecrypt(string base64EncodeData)
        {
            if (string.IsNullOrEmpty(base64EncodeData))
                return "";
            var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
            var result = Encoding.UTF8.GetString(base64EncodeBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }

        //for login
        public string UserLogin(UserLoginModel userLoginModel)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
               
                SqlCommand command = new SqlCommand("spLogin", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmailId", userLoginModel.EmailId);
                command.Parameters.AddWithValue("@Password", userLoginModel.Password);

                
                con.Open();

                var result = command.ExecuteScalar();
                if (result != null)
                {
                    string query = "SELECT UserId FROM Users WHERE EmailId = '" + userLoginModel.EmailId + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    var Id = cmd.ExecuteScalar();
                    var token = GenerateSecurityToken(userLoginModel.EmailId, Id.ToString());
                    return token;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return default;
        }

        //JWT token
        public string GenerateSecurityToken(string email, string UserId)
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
                    new Claim("UserId", UserId.ToString())
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

        //for forgetpassword
        public string ForgetPassword(string Emailid)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                SqlCommand cmd = new SqlCommand("spForgetPassword", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailId", Emailid);
                con.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    string query = "SELECT UserId FROM UserTable WHERE EmailId = '" + result + "'";
                    SqlCommand que = new SqlCommand(query, con);
                    var Id = cmd.ExecuteScalar();
                    var token = GenerateSecurityToken(Emailid, Id.ToString());
                    return token;
                }
                con.Close();
                return null;
            }
            catch (Exception)
            {
                throw;
            }
         }
            //for reset password
            public bool ResetPassword(string email, string newpassword, string confirmpassword)
            {
                using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
                try
                {
                    if (newpassword == confirmpassword)
                    {
                        SqlCommand cmd = new SqlCommand("spResetPassword", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmailId", email);
                        cmd.Parameters.AddWithValue("@Password", newpassword);
                        con.Open();
                        SqlDataReader rd = cmd.ExecuteReader();
                        if (rd.HasRows)
                        {
                            while (rd.Read())
                            {
                                email = Convert.ToString(rd["EmailId"] == DBNull.Value ? default : rd["EmailId"]);
                                newpassword = Convert.ToString(rd["Password"] == DBNull.Value ? default : rd["Password"]);
                            }
                            return true;
                        }
                        return true;
                    }

                    return false;
                }
                catch (Exception)
                {

                    throw;
                }
            }        
    }
}
