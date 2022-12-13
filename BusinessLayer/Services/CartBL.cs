using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL:ICartBL
    {
        private readonly ICartRL icartRL;

        public CartBL(ICartRL icartRL)
        {
            this.icartRL = icartRL;
        }

        public CartModel AddToCart(CartModel cartModel,int userId)
        {
            try
            {
                return icartRL.AddToCart(cartModel,userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
