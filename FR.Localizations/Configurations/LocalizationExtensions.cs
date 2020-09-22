using FR.Localizations.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FR.Localizations.Configurations
{
    public static class LocalizationExtensions
    {
        public static IServiceCollection AddFRLocalizations(this IServiceCollection service)
        {
            service.AddScoped<IStringLocalization, StringLocalization>();
            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return service;
        }
    }
}
