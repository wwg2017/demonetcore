using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DemoNetCore
{
    public class Program
    {        
        public static void Main(string[] args)
        {            
            //下面是指定ip
          BuildWebHost(args).Run(); //会显示为生产环境
            // var host = new WebHostBuilder()
            //     .UseKestrel()
            //     .UseUrls("http://192.168.22.8:5000")
            //     .UseContentRoot(Directory.GetCurrentDirectory())
            //     .UseIISIntegration()
            //     .UseStartup<Startup>()
            //     .Build();
            //host.Run();
            ////下面通过改变端口 可以跑起来两个服务//会显示为开发环境
            var config = new ConfigurationBuilder().AddJsonFile("host.json", optional: true)
                .Build();
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseConfiguration(config)
                .Build();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)           
                .UseStartup<Startup>()
                .Build();
    }
}
