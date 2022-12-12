using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CartRL: ICartRL
    {
        private readonly IConfiguration iConfiguration;

        public CartRL(IConfiguration iConfiguration)
        {
            this.iConfiguration = iConfiguration;
        }
    }
}
