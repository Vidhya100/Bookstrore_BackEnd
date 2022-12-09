using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBL: IAdminBL
    {
        private readonly IAdminRL iadminRL;

        public AdminBL(IAdminRL iadminRL)
        {
            this.iadminRL = iadminRL;
        }

        public string AdminLogin(AdminLoginModel admin)
        {
            try
            {
                return iadminRL.AdminLogin(admin);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
