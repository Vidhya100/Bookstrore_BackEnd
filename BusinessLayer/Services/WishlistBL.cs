using BusinessLayer.Interface;
using CommonLayer.Model;
using LanguageExt;
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
        public string AddToWishlist(int bookId, int userId)
        {
            try
            {
                return iwishlistRL.AddToWishlist(bookId,userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool DeleteFromWishlist(int wishlistId)
        {
            try
            {
                return iwishlistRL.DeleteFromWishlist(wishlistId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<WishlistModel> GetWishlistItem(int userId)
        {
            try
            {
                return iwishlistRL.GetWishlistItem(userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
