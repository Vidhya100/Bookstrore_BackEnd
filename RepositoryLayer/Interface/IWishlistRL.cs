using CommonLayer.Model;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishlistRL
    {
        public string AddToWishlist(int bookId, int userId);
        public bool DeleteFromWishlist(int wishlistId);
        public List<WishlistModel> GetWishlistItem(int userId);
    }
}
