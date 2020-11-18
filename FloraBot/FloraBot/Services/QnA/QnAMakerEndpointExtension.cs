using Microsoft.Bot.Builder.AI.QnA;
using System;

namespace FloraBot.Services.QnA
{
    public static class QnAMakerEndpointExtension
    {
        public static void Validate(this QnAMakerEndpoint qnAMakerEndpoint)
        {
            if (string.IsNullOrWhiteSpace(qnAMakerEndpoint.KnowledgeBaseId))
            {
                throw new InvalidOperationException("The QnA Knowledge Base Id ('KnowledgeBaseId') is required to run this sample.");
            }

            if (string.IsNullOrWhiteSpace(qnAMakerEndpoint.Host))
            {
                throw new InvalidOperationException("The QnA Host ('Host') is required to run this sample.");
            }

            if (string.IsNullOrWhiteSpace(qnAMakerEndpoint.EndpointKey))
            {
                throw new InvalidOperationException("The QnA EndpointKey ('EndpointKey') is required to run this sample.");
            }
        }

        public static bool IsValid(this QnAMakerEndpoint qnAMakerEndpoint)
        {
            return !(string.IsNullOrWhiteSpace(qnAMakerEndpoint.KnowledgeBaseId)
                || string.IsNullOrWhiteSpace(qnAMakerEndpoint.Host)
                || string.IsNullOrWhiteSpace(qnAMakerEndpoint.EndpointKey));
        }
    }
}
