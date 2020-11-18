using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

public partial class LuisResponse : IRecognizerConvert
{
    [JsonProperty("text")]
    public string Text;

    [JsonProperty("alteredText")]
    public string AlteredText;

    public enum Intent
    {
        None,
        WhatToWatch
    };
    [JsonProperty("intents")]
    public Dictionary<Intent, IntentScore> Intents;

    public class _Entities
    {
        // Built-in entities
        public DateTimeSpec[] datetime;

        // Lists
        public string[][] TimeAdverbPhrase;


        // Instance
        public class _Instance
        {
            public InstanceData[] TimeAdverbPhrase;
        }
        [JsonProperty("$instance")]
        public _Instance _instance;
    }
    [JsonProperty("entities")]
    public _Entities Entities;

    [JsonExtensionData(ReadData = true, WriteData = true)]
    public IDictionary<string, object> Properties { get; set; }

    public void Convert(dynamic result)
    {
        var app = JsonConvert.DeserializeObject<LuisResponse>(
                JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }),
                new JsonSerializerSettings
                {
                    Error = HandleDeserializationError
                });
        Text = app.Text;
        AlteredText = app.AlteredText;
        Intents = app.Intents;
        Entities = app.Entities;
        Properties = app.Properties;
    }

    public (Intent intent, double score) TopIntent()
    {
        Intent maxIntent = Intent.None;
        var max = 0.0;
        foreach (var entry in Intents)
        {
            if (entry.Value.Score > max)
            {
                maxIntent = entry.Key;
                max = entry.Value.Score.Value;
            }
        }
        return (maxIntent, max);
    }
    private void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
    {
        errorArgs.ErrorContext.Handled = true;
    }
}