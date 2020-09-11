using System;

namespace FR.Localizations.Models
{
    public class LocalizedString
    {
        public LocalizedString(string name, string value)
            : this(name, value, false)
        {
        }

        public LocalizedString(string name, string value, bool resourceNotFound)
            : this(name, value, resourceNotFound, (string)null)
        {
        }

        public LocalizedString(string name, string value, bool resourceNotFound, string searchedLocation)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Name = name;
            Value = value;
            ResourceNotFound = resourceNotFound;
            SearchedLocation = searchedLocation;
        }

        public string Name { get; }
        public string Value { get; }
        public bool ResourceNotFound { get; }
        public string SearchedLocation { get; }
    }
}
