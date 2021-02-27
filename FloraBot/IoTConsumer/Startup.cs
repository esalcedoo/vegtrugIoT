using IoTConsumer.Data;
using IoTConsumer.IoTCentral;
using IoTConsumer.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

[assembly: FunctionsStartup(typeof(IoTConsumer.Startup))]
namespace IoTConsumer
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;

            var botClientServiceUri = configuration["BotClientServiceUri"];

            builder.Services.AddHttpClient<BotClientService>(client =>
            {
                client.BaseAddress = new Uri(botClientServiceUri);
            });

            builder.Services.AddScoped<PlantService>();

            var ioTCentralHost = configuration["IoTCentral:Host"];
            builder.Services.AddHttpClient<IoTCentralCommandsService>(client =>
            {
                client.BaseAddress = new Uri(ioTCentralHost);
                client.DefaultRequestHeaders.Add("Authorization", configuration["IoTCentral:Authorization"]);

            });

            string connectionString = configuration.GetConnectionStringOrSetting("DBConnectionString");

            builder.Services.AddDbContext<FloraDBContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
