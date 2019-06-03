using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using TransacaoApi.Helpers;
using TransacaoApi.Serialization;

namespace TransacaoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\MerchantDiscountRates.json");
            Utils.mdr = JsonConvert.DeserializeObject<List<MerchantDiscount>>(json);

            CreateWebHostBuilder(args).Build().Run();           
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
