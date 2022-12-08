using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL: IUserRL
    {
        private readonly IConfiguration iconfiguration;

        public UserRL(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
    }
}
