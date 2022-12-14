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
    public class FeedBackRL: IFeedBackRL
    {
        private readonly IConfiguration iConfiguration;
        public FeedBackRL(IConfiguration iconfiguration)
        {
            this.iConfiguration = iconfiguration;
        }
        public string AddFeedback(FeedbackModel feedback, int userId)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spAddFeedback", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Rating", feedback.Rating);
                command.Parameters.AddWithValue("@Comment", feedback.Comment);
                command.Parameters.AddWithValue("@BookId", feedback.BookId);
                command.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                var result = command.ExecuteNonQuery();
                con.Close();

                if (result > 0)
                {
                    return "Feedback added";
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

        public List<FeedbackModel> GetFeedback(int bookId)
        {
            using SqlConnection con = new SqlConnection(iConfiguration["ConnectionStrings:BookStoreDB"]);
            try
            {
                List<FeedbackModel> feedbackList = new List<FeedbackModel>();
                SqlCommand command = new SqlCommand("spGetFeedback", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BookId", bookId);

                con.Open();
                SqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        FeedbackModel feedback = new FeedbackModel();
                        feedback.FeedbackId = Convert.ToInt32(rdr["FeedbackId"] == DBNull.Value ? default : rdr["FeedbackId"]);
                        feedback.BookId = Convert.ToInt32(rdr["BookId"] == DBNull.Value ? default : rdr["BookId"]);
                        feedback.UserId = Convert.ToInt32(rdr["UserId"] == DBNull.Value ? default : rdr["UserId"]);
                        feedback.Comment = Convert.ToString(rdr["Comment"] == DBNull.Value ? default : rdr["Comment"]);
                        feedback.Rating = Convert.ToDouble(rdr["Rating"] == DBNull.Value ? default : rdr["Rating"]);
                        feedback.FullName = Convert.ToString(rdr["FullName"] == DBNull.Value ? default : rdr["FullName"]);
                        feedbackList.Add(feedback);
                    }
                    return feedbackList;
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
