using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LogisticsChain.Tool
{
    public class ConfigurationManager
    {
        private static IConfigurationRoot config = null;
        static ConfigurationManager()
        {
            // Microsoft.Extensions.Configuration扩展包提供的
            var builder = new ConfigurationBuilder()
               // .AddJsonFile(@"D:\Guo\03_CoreTest\LogisticsChain\LogisticsChain\LogisticsChain.Test\XmlConfig\system.config");
                .AddJsonFile(Directory.GetCurrentDirectory()+ "/JsonConfig/config.json");
            config = builder.Build();
        }

        public static IConfigurationRoot AppSettings
        {
            get
            {
                return config;
            }
        }

        public static string Get(string key)
        {
            return config[key];
        }

    }
}
