using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IFeedBackRL
    {
        public string AddFeedback(FeedbackModel feedback, int userId);
        public List<FeedbackModel> GetFeedback(int bookId);
    }
}
