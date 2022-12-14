using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IOrderRL
    {
        public string AddOrder(OrderModel orderModel, int userId);
        public List<OrderModel> GetAllOrders(int userId);
        public bool RemoveOrder(int orderId);
    }
}
