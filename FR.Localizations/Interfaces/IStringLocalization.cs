using FR.Localizations.Models;
using System.Collections.Generic;

namespace FR.Localizations.Interfaces
{
    public interface IStringLocalization
    {
        LocalizedString this[string name] { get; }
        LocalizedString this[string name, params object[] arguments] { get; }
        IEnumerable<LocalizedString> GetAllStrings();
    }
}
