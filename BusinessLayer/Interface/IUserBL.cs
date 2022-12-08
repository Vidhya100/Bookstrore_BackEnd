using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserRegiModel Register(UserRegiModel userModel);
        public UserLoginModel UserLogin(UserLoginModel userLoginModel);

    }
}
