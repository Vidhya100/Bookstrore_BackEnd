using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAddressBL
    {
        public string AddAddress(AddressModel address, int userId);
        public AddressModel UpdateAddress(AddressModel address, int userId);
        public bool removeAddress(int addressId);
        public List<AddressModel> GetAllAddress(int UserId);
    }
}
