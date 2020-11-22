
using FloraBot.Dialogs;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DialogsCollectionExtension
    {
        public static IServiceCollection AddDialogs(this IServiceCollection services)
        {
            services.AddSingleton<QnADialog>();
            services.AddSingleton<SummaryDialog>();
            return services;
        }
    }
}