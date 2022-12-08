using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL iuserRL;

        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }

        public UserRegiModel Register(UserRegiModel userModel)
        {
            try
            {
                return iuserRL.Register(userModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public UserLoginModel UserLogin(UserLoginModel userLoginModel)
        {
            try
            {
                return iuserRL.UserLogin(userLoginModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
