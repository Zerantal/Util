﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace Util
{
    // ReSharper disable once UnusedMember.Global
    public class MakeIEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _comparer;

        public MakeIEqualityComparer(Func<T, T, bool> comparer)
        {
            // // Contract.Requires(comparer != null);

            _comparer = comparer;
        }

        public bool Equals(T x, T y)
        {
            return _comparer(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.ToString().ToLower(CultureInfo.CurrentCulture).GetHashCode();
        }

    }
}
