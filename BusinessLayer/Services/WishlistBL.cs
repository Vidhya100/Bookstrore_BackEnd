using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishlistBL:IWishlistBL
    {
        private readonly IWishlistRL iwishlistRL;

        public WishlistBL(IWishlistRL iwishlistRL)
        {
            this.iwishlistRL = iwishlistRL;
        }
    }
}
