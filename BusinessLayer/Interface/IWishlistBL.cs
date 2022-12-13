using CommonLayer.Model;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishlistBL
    {
        public string AddToWishlist(int bookId, int userId);
        public bool DeleteFromWishlist(int wishlistId);
        public List<WishlistModel> GetWishlistItem(int userId);
    }
}
