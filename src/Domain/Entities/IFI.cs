using System;
using System.Collections.Generic;
using System.Linq;

namespace Doctrina.Domain.Entities
{
     /// <summary>
    /// Inverse Functional Indentifier
    /// </summary>
    public class Ifi
    {
        private static readonly ICollection<Ifi> _identifiers = new HashSet<Ifi>();

        public static readonly Ifi Mbox = new Ifi("mbox");
        public static readonly Ifi Mbox_SHA1SUM = new Ifi("mbox_sha1sum");
        public static readonly Ifi OpenId = new Ifi("openid");
        public static readonly Ifi Account = new Ifi("account");

        public readonly string Key;

        public Ifi(string key)
        {
            Key = key;
            _identifiers.Add(this);
        }

        public override string ToString()
        {
            return Key;
        }

        public static Ifi Parse(string s)
        {
            var ifi = _identifiers.SingleOrDefault(x=> x.Key == s);

            if(ifi == null)
                throw new FormatException("Invalid IFI format.");

            return ifi;
        }

        public override bool Equals(object obj)
        {
            return obj is Ifi identifier &&
                   Key == identifier.Key;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Key);
        }

        public static implicit operator string(Ifi ifi)
        {
            return ifi.ToString();
        }

        public static implicit operator Ifi(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var ifi = _identifiers.FirstOrDefault(x => x.Key == key);
            if (ifi != null)
            {
                return ifi;
            }

            throw new ArgumentException($"\"{key}\" is not a valid Inverse Functional Identifier.");
        }

        public static bool operator ==(Ifi left, Ifi right)
        {
            return left?.ToString() == right?.ToString();
        }

        public static bool operator !=(Ifi left, Ifi right)
        {
            return left?.ToString() != right?.ToString();
        }
    }
}