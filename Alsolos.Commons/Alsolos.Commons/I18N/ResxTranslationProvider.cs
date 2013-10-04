
namespace Alsolos.Commons.I18N {
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;

    public class ResxTranslationProvider : ITranslationProvider {
        private readonly ResourceManager _resourceManager;

        public ResxTranslationProvider(string baseName, Assembly assembly) {
            _resourceManager = new ResourceManager(baseName, assembly);
        }

        public object Translate(string key) {
            try {
                return _resourceManager.GetString(key);
            } catch {
                return null;
            }
        }

        public IEnumerable<CultureInfo> Languages {
            get {
                // TODO: Resolve the available languages
                yield return new CultureInfo("de");
                yield return new CultureInfo("en");
            }
        }
    }
}
