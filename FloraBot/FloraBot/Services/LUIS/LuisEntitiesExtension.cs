using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;
using System;
using System.Linq;

namespace FloraBot.Services.LUIS
{
    public static class LuisEntitiesExtension
    {
        public static DateTime GetDateTime(this RecognizerResult recognizerResult)
        {
            var luisResponse = new LuisResponse();
            luisResponse.Convert(recognizerResult);

            DateTime dateTime;
            if (TryFindTimeAdverPhrase(luisResponse, out dateTime) || TryFindDateTime(luisResponse, out dateTime))
            {
                return dateTime;
            }
            return default;
        }

        public static bool TryFindDateTime(LuisResponse luisResponse, out DateTime dateTime)
        {
            DateTimeSpec dateTimeSpec = luisResponse.Entities.datetime?.FirstOrDefault();

            dateTime = default;
            if (dateTimeSpec!=null)
            foreach (string expresion in dateTimeSpec?.Expressions)
            {
                TimexProperty parsed = new TimexProperty(expresion);
                if (parsed.TimexValue.Equals("PRESENT_REF", StringComparison.CurrentCultureIgnoreCase))
                {
                    dateTime = DateTime.UtcNow.AddHours(2);
                    return true;
                }
                else if (8 <= parsed.Hour && parsed.Hour < 18)
                {
                    dateTime = new DateTime(
                        year: parsed.Year ?? DateTime.UtcNow.Year,
                        month: parsed.Month ?? DateTime.UtcNow.Month,
                        day: parsed.DayOfMonth ?? DateTime.UtcNow.Day,
                        hour: parsed.Hour ?? DateTime.UtcNow.AddHours(2).Hour,
                        minute: parsed.Minute ?? 0,
                        second: parsed.Second ?? 0
                        );
                    return true;
                }
            }
            return false;
        }

        public static bool TryFindTimeAdverPhrase(LuisResponse luisResponse, out DateTime dateTime)
        {
            dateTime = default;
            if (int.TryParse(luisResponse.Entities.TimeAdverbPhrase?[0][0], out int hour))
            {
                dateTime = new DateTime(
                        year: DateTime.UtcNow.Year,
                        month: DateTime.UtcNow.Month,
                        day: DateTime.UtcNow.Day,
                        hour: hour,
                        minute: 0,
                        second: 0
                        );

                return true;
            }
            return false;
        }
    }
}
