using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using MIQuizAPI.Database.Context;
using MIQuizAPI.Database;

namespace MIQuizAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var wh = BuildWebHost( args );
            using( var scope = wh.Services.CreateScope() ) {
                var services = scope.ServiceProvider;
                try {
                    var context = services.GetRequiredService<MIQuizContext>();
                    DbInitializer.Initialize( context );
                } catch( Exception ex ) {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError( ex, "An error occurred while seeding the database." );
                }
            }
            wh.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
