﻿using System;
using System.Collections.Generic;

namespace Util
{
    // ReSharper disable once UnusedMember.Global
    public sealed class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _sourceDictionary;

        public ReadOnlyDictionary(IDictionary<TKey, TValue> source)
        {
            // // Contract.Requires(source != null);

            _sourceDictionary = source;
        }

        #region IDictionary<TKey,TValue> Members

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            throw new NotSupportedException();            
        }

        public bool ContainsKey(TKey key)
        {
            return _sourceDictionary.ContainsKey(key);            
        }

        public ICollection<TKey> Keys => _sourceDictionary.Keys;

        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            throw new NotSupportedException();            
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            bool success = ContainsKey(key);

            value = success ? this[key] : default;
                            
            return success;            
        }

        public ICollection<TValue> Values => _sourceDictionary.Values;

        public TValue this[TKey key] => _sourceDictionary[key];

        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get => _sourceDictionary[key];
            set => throw new NotSupportedException();
        }

        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>> Members

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();            
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            throw new NotSupportedException();            
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _sourceDictionary.Contains(item);            
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _sourceDictionary.CopyTo(array, arrayIndex);            
        }

        public int Count =>
            // //Contract.Ensures(// Contract.Result<int>() == _sourceDictionary.Count);
            _sourceDictionary.Count;

        public bool IsReadOnly => true;

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();            
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _sourceDictionary.GetEnumerator();            
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _sourceDictionary.GetEnumerator();            
        }

        #endregion

    }
}
