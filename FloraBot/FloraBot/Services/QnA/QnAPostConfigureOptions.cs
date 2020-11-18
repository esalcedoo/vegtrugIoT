using Microsoft.Bot.Builder.AI.QnA;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FloraBot.Services.QnA
{
    public class QnAPostConfigureOptions : IPostConfigureOptions<QnAMakerEndpoint>
    {
        private readonly IConfiguration _configuration;

        public QnAPostConfigureOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void PostConfigure(string name, QnAMakerEndpoint options)
        {
            if (!options.IsValid())
            {
                var qnAMakerEndpoint = _configuration.GetSection("QnAMakerEndpoint").Get<QnAMakerEndpoint>();
                options.KnowledgeBaseId = qnAMakerEndpoint.KnowledgeBaseId;
                options.Host = qnAMakerEndpoint.Host;
                options.EndpointKey = qnAMakerEndpoint.EndpointKey;
            }
        }
    }
}
