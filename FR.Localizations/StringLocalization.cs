using FR.Localizations.Interfaces;
using FR.Localizations.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FR.Localizations
{
    public class StringLocalization : IStringLocalization
    {
        private IList<JsonLocalization> _localizationValuesList = new List<JsonLocalization>();
        private readonly IHostEnvironment _environment;
        private static IHttpContextAccessor _accessor;

        public StringLocalization(IHostEnvironment environment,
                                   IHttpContextAccessor accessor)
        {
            _environment = environment;
            _accessor = accessor;
            _localizationValuesList = JsonConvert.DeserializeObject<List<JsonLocalization>>(JsonFile());
        }

        private string JsonFile()
        {
            var httpContext = _accessor.HttpContext;

            if (httpContext.Request.Headers["Accept-Language"].Count <= 0)
                throw new ArgumentException("Accept-Language not loaded");

            var language = httpContext.Request.Headers["Accept-Language"];

            return File.ReadAllText(Path.Combine(_environment.ContentRootPath, "App_Data", $"resources.{language}.json"));
        }

        public LocalizedString this[string key]
        {
            get
            {
                var translation = GetValueByKey(key);
                return new LocalizedString(key, translation ?? key, translation == null);
            }
        }

        public LocalizedString this[string key, params object[] arguments]
        {
            get
            {
                var translation = GetValueByKey(key);
                var value = string.Format(translation ?? key, arguments);

                return new LocalizedString(key, value, translation == null);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings()
        {
            return _localizationValuesList
                .Select(localization => new LocalizedString(localization.Key, localization.Value))
                .AsEnumerable();
        }

        private string GetValueByKey(string key)
        {
            return _localizationValuesList
                    .FirstOrDefault(localization => localization.Key == key)?
                    .Value;
        }
    }
}
