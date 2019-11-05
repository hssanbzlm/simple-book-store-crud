using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bookstore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webhost=CreateWebHostBuilder(args).Build();
            RunMigrations(webhost);
            webhost.Run();
        }

        private static void RunMigrations(IWebHost webhost)
        {


            using (var scope = webhost.Services.CreateScope())
            {

                var db = scope.ServiceProvider.GetRequiredService<BookstoreDbContext>();
                db.Database.Migrate();
            }

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
