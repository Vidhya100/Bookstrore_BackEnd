using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserRegiModel Register(UserRegiModel userModel);
        public string UserLogin(UserLoginModel userLoginModel);

    }
}
