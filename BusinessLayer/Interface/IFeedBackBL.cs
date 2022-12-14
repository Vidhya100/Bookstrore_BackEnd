using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IFeedBackBL
    {
        public string AddFeedback(FeedbackModel feedback, int userId);
        public List<FeedbackModel> GetFeedback(int bookId);
    }
}
