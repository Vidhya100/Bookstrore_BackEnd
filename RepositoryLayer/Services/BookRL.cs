using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL : IBookRL
    {
        private readonly IConfiguration iConfiguration;

        public BookRL(IConfiguration iConfiguration)
        {
            this.iConfiguration = iConfiguration;
        }
    }
}
