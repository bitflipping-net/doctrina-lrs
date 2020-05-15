using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Doctrina.Domain.Entities.OwnedTypes
{
    public class LanguageMapCollection : IDictionary<string, string>, IEqualityComparer<LanguageMapCollection>
    {
        private readonly Dictionary<string, string> Values = new Dictionary<string, string>();

        public string this[string key] { get => Values[key]; set => Values[key] = value; }

        public ICollection<string> Keys => ((IDictionary<string, string>)Values).Keys;

        public int Count => Values.Count;

        public bool IsReadOnly => ((IDictionary<string, string>)Values).IsReadOnly;

        ICollection<string> IDictionary<string, string>.Values => ((IDictionary<string, string>)Values).Values;

        public void Add(string key, string value)
        {
            Values.Add(key, value);
        }

        public void Add(KeyValuePair<string, string> item)
        {
            ((IDictionary<string, string>)Values).Add(item);
        }

        public void Add(IDictionary<string, string> values)
        {
            foreach (var value in values)
            {
                Add(value);
            }
        }

        public void Clear()
        {
            Values.Clear();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return ((IDictionary<string, string>)Values).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return Values.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            ((IDictionary<string, string>)Values).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return ((IDictionary<string, string>)Values).GetEnumerator();
        }

        public bool Remove(string key)
        {
            return Values.Remove(key);
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            return ((IDictionary<string, string>)Values).Remove(item);
        }

        public bool TryGetValue(string key, out string value)
        {
            return Values.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<string, string>)Values).GetEnumerator();
        }

        public override bool Equals(object obj) =>
            (obj is LanguageMapCollection)
                ? Equals(obj)
                : false;

        public bool Equals([AllowNull] LanguageMapCollection x, [AllowNull] LanguageMapCollection y)
        {
            return x.Values.Equals(y.Values);
        }

        public override int GetHashCode()
        {
            return Values.GetHashCode();
        }

        public int GetHashCode([DisallowNull] LanguageMapCollection obj)
        {
            return obj.Values.GetHashCode();
        }
    }
}
