using System;
using System.Collections.Generic;
using System.Windows;

namespace Alsolos.Commons.Resources {
	// https://github.com/indexzero/bruce-wayne/blob/master/wpf-samples/WpfSamples/Controls/SharedResourceDictionary.cs
    public class SharedResourceDictionary : ResourceDictionary {
        private static readonly Dictionary<Uri, ResourceDictionary> _sharedDictionaries = new Dictionary<Uri, ResourceDictionary>();

        private Uri _sourceUri;

        public new Uri Source {
            get {
                return _sourceUri;
            }
            set {
                _sourceUri = new Uri(value.OriginalString, UriKind.RelativeOrAbsolute);
                if (!_sharedDictionaries.ContainsKey(value)) {
                    base.Source = value;
                    _sharedDictionaries.Add(value, this);
                } else {
                    MergedDictionaries.Add(_sharedDictionaries[value]);
                }
            }
        }
    }
}
