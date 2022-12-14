using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL:IOrderBL
    {
        private readonly IOrderRL iorderRL;

        public OrderBL(IOrderRL iorderRL)
        {
            this.iorderRL = iorderRL;
        }
        public string AddOrder(OrderModel orderModel, int userId)
        {
            try
            {
                return iorderRL.AddOrder(orderModel, userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /*
        public string AddAddress(AddressModel address, int userId)
        {
            try
            {
                return iaddressRL.AddAddress(address, userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }*/
    }
}
