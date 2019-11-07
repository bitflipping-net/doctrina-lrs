using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities.OwnedTypes
{
    public class ExtensionsCollection : IDictionary<Uri, JToken>
    {
        private readonly IDictionary<Uri, JToken> _values = new Dictionary<Uri, JToken>();

        public JToken this[Uri key] { get => _values[key]; set => _values[key] = value; }

        public ICollection<Uri> Keys => _values.Keys;

        public ICollection<JToken> Values => _values.Values;

        public int Count => _values.Count;

        public bool IsReadOnly => _values.IsReadOnly;

        public void Add(Uri key, JToken value)
        {
            _values.Add(key, value);
        }

        public void Add(KeyValuePair<Uri, JToken> item)
        {
            _values.Add(item);
        }

        public void Add(IDictionary<Uri, JToken> values)
        {
            foreach(var item in values)
            {
                Add(item);
            }
        }

        public void Clear()
        {
            _values.Clear();
        }

        public bool Contains(KeyValuePair<Uri, JToken> item)
        {
            return _values.Contains(item);
        }

        public bool ContainsKey(Uri key)
        {
            return _values.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<Uri, JToken>[] array, int arrayIndex)
        {
            _values.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<Uri, JToken>> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        public bool Remove(Uri key)
        {
            return _values.Remove(key);
        }

        public bool Remove(KeyValuePair<Uri, JToken> item)
        {
            return _values.Remove(item);
        }

        public bool TryGetValue(Uri key, out JToken value)
        {
            return _values.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        public string JsonStringDbValue
        {
            get
            {
                return JsonConvert.SerializeObject(this);
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }

                var collection = JsonConvert.DeserializeObject<ExtensionsCollection>(value);
                Clear();
                Add(collection);
            }
        }
    }
}
