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
        public List<OrderModel> GetAllOrders(int userId)
        {
            try
            {
                return iorderRL.GetAllOrders(userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool RemoveOrder(int orderId)
        {
            try
            {
                return iorderRL.RemoveOrder(orderId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
