using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL icartRL;

        public CartBL(ICartRL icartRL)
        {
            this.icartRL = icartRL;
        }

        public CartModel AddToCart(int bookId, int userId)
        {
            try
            {
                return icartRL.AddToCart(bookId, userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string UpdateCart(int cartId, int bookQty)
        {
            try
            {
                return icartRL.UpdateCart(cartId, bookQty);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool RemoveFromCart(int cartId)
        {
            try
            {
                return icartRL.RemoveFromCart(cartId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<CartModel> GetCartItem(int userId)
        {
            try
            {
                return icartRL.GetCartItem(userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
