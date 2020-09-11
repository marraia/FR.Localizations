using FR.Localizations.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FR.Localizations.Configurations
{
    public static class LocalizationExtensions
    {
        public static IServiceCollection AddLocalizations(this IServiceCollection service)
        {
            service.AddScoped<IStringLocalization, StringLocalization>();

            return service;
        }
    }
}
