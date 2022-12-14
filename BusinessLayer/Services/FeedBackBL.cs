using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class FeedBackBL:IFeedBackBL
    {
        private readonly IFeedBackRL iFeedBackRL;

        public FeedBackBL(IFeedBackRL iFeedBackRL)
        {
            this.iFeedBackRL = iFeedBackRL;
        }

        public string AddFeedback(FeedbackModel feedback, int userId)
        {
            try
            {
                return iFeedBackRL.AddFeedback(feedback, userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<FeedbackModel> GetFeedback(int bookId)
        {
            try
            {
                return iFeedBackRL.GetFeedback(bookId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
