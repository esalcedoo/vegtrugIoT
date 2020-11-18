using Microsoft.Bot.Builder.AI.Luis;
using System;

namespace FloraBot.Services.LUIS
{
    public static class LuisApplicationExtension
    {
        public static void Validate(this LuisApplication luisApplication)
        {
            if (string.IsNullOrWhiteSpace(luisApplication.ApplicationId))
            {
                throw new InvalidOperationException("The LUIS ApplicationId ('ApplicationId') is required to run this sample.");
            }

            if (string.IsNullOrWhiteSpace(luisApplication.Endpoint))
            {
                throw new InvalidOperationException("The LUIS Endpoint ('Endpoint') is required to run this sample.");
            }

            if (string.IsNullOrWhiteSpace(luisApplication.EndpointKey))
            {
                throw new InvalidOperationException("The LUIS EndpointKey ('EndpointKey') is required to run this sample.");
            }
        }

        public static bool IsValid(this LuisApplication luisRecognizerOptions)
        {
            return !(string.IsNullOrWhiteSpace(luisRecognizerOptions.ApplicationId)
                || string.IsNullOrWhiteSpace(luisRecognizerOptions.Endpoint)
                || string.IsNullOrWhiteSpace(luisRecognizerOptions.EndpointKey));
        }
    }
}
