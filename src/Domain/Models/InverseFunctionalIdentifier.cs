using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Doctrina.Domain.Models
{
    /// <summary>
    /// Inverse Functional Indentifier
    /// </summary>
    public class InverseFunctionalIdentifier
    {
        private static readonly ICollection<InverseFunctionalIdentifier> _identifiers = new HashSet<InverseFunctionalIdentifier>();

        public static readonly InverseFunctionalIdentifier Mbox = new InverseFunctionalIdentifier("mbox");
        public static readonly InverseFunctionalIdentifier Mbox_SHA1SUM = new InverseFunctionalIdentifier("mbox_sha1sum");
        public static readonly InverseFunctionalIdentifier OpenId = new InverseFunctionalIdentifier("openid");
        public static readonly InverseFunctionalIdentifier Account = new InverseFunctionalIdentifier("account");

        public readonly string Key;

        public InverseFunctionalIdentifier(string key)
        {
            Key = key;
            _identifiers.Add(this);
        }

        public override string ToString()
        {
            return Key;
        }

        public override bool Equals(object obj)
        {
            return obj is InverseFunctionalIdentifier identifier &&
                   Key == identifier.Key;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Key);
        }

        public static implicit operator string(InverseFunctionalIdentifier ifi)
        {
            return ifi.ToString();
        }

        public static implicit operator InverseFunctionalIdentifier(string key)
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

        public static bool operator ==(InverseFunctionalIdentifier left, InverseFunctionalIdentifier right)
        {
            return left?.ToString() == right?.ToString();
        }

        public static bool operator !=(InverseFunctionalIdentifier left, InverseFunctionalIdentifier right)
        {
            return left?.ToString() != right?.ToString();
        }
    }
}
