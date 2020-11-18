using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FloraBot.Services.LUIS
{
    internal class LuisPostConfigureOptions : IPostConfigureOptions<LuisApplication>
    {
        private readonly IConfiguration _configuration;

        public LuisPostConfigureOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void PostConfigure(string name, LuisApplication options)
        {
            if (!options.IsValid())
            {
                var luisApplication = _configuration.GetSection("LuisApplication").Get<LuisApplication>();
                options.ApplicationId = luisApplication.ApplicationId;
                options.Endpoint = luisApplication.Endpoint;
                options.EndpointKey = luisApplication.EndpointKey;
            }
        }
    }
}
