using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Bizpack.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Bizpack
{
    public class Program
    {
        public static void Main(string[] args)
        {
              IWebHost webHost = CreateWebHostBuilder (args).Build ();

            // Create a new scope
            using (var scope = webHost.Services.CreateScope ()) {
                // Get the DbContext instance
                var seeder = scope.ServiceProvider.GetRequiredService<ISeederService> ();

                // //Do the migration asynchronously
                Task.Run(() => seeder.SeedRoles()).Wait();
                Task.Run(() => seeder.SeedUsers()).Wait();
            }

            webHost.Run();


            // Run the WebHost, and start accepting requests
            // There's an async overload, so we may as well use it
            
        }

        public static IWebHostBuilder CreateWebHostBuilder (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .UseStartup<Startup> ();
    }
}

