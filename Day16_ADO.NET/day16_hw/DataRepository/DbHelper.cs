using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace DataRepository
{
    class DbHelper
    {
        public static string GetConnectionString()
        {
            IConfigurationBuilder conf = new ConfigurationBuilder();
            IConfigurationRoot root = conf.AddJsonFile("appsettings.json").Build();
            string connectionString = root.GetConnectionString("MinionsDB");
            return connectionString;
        }
    }
}
