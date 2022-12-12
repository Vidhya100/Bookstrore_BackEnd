using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class WishlistRL: IWishlistRL
    {
        private readonly IConfiguration iConfiguration;

        public WishlistRL(IConfiguration iConfiguration)
        {
            this.iConfiguration = iConfiguration;
        }
    }
}
