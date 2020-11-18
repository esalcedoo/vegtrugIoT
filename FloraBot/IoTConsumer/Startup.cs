using IoTConsumer.Data;
using IoTConsumer.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(IoTConsumer.Startup))]
namespace IoTConsumer
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient<BotClientService>(client =>
            {
                client.BaseAddress = new Uri("https://a769ba9d691c.ngrok.io/api/");
            });

            builder.Services.AddScoped<PlantService>();

            string connectionString = builder.GetContext().Configuration.GetConnectionStringOrSetting("DBConnectionString");
            builder.Services.AddDbContext<FloraDBContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
