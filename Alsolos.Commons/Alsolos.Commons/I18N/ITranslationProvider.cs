namespace Alsolos.Commons.I18N
{
    using System.Collections.Generic;
    using System.Globalization;

    public interface ITranslationProvider
    {
        object Translate(string key);

        IEnumerable<CultureInfo> Languages { get; }
    }
}
