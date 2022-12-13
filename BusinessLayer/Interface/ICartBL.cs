using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
        public CartModel AddToCart(int bookId, int userId);
        public string UpdateCart(int cartId, int bookQty);
        public bool RemoveFromCart(int cartId);
        public List<CartModel> GetCartItem(int userId);
    }
}
