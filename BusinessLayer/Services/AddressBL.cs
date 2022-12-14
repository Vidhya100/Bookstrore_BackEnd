
using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL:IAddressBL
    {
        private readonly IAddressRL iaddressRL;

        public AddressBL(IAddressRL iaddressRL)
        {
            this.iaddressRL = iaddressRL;
        }
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
        }
        public AddressModel UpdateAddress(AddressModel address, int userId)
        {
            try
            {
                return iaddressRL.UpdateAddress(address, userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool removeAddress(int addressId)
        {
            try
            {
                return iaddressRL.removeAddress(addressId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<AddressModel> GetAllAddress(int UserId)
        {
            try
            {
                return iaddressRL.GetAllAddress(UserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
    }
}
