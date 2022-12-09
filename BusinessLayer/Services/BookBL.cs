using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL ibookRL;

        public BookBL(IBookRL ibookRL)
        {
            this.ibookRL = ibookRL;
        }
    }
}
