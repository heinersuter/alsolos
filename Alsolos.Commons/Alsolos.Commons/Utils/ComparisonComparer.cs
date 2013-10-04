using System;
using System.Collections;
using System.Collections.Generic;

namespace Alsolos.Commons.Utils {
    public class ComparisonComparer<T> : IComparer, IComparer<T> {
        private readonly Comparison<T> _comparison;

        public ComparisonComparer(Func<T, T, int> comparison) {
            if (comparison == null) {
                throw new ArgumentNullException(@"comparison");
            }
            _comparison = new Comparison<T>(comparison);
        }

        public int Compare(object x, object y) {
            return Compare((T)x, (T)y);
        }

        public int Compare(T x, T y) {
            return _comparison(x, y);
        }
    }
}