using Doctrina.Domain.Infrastructure;
using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{

    public class Account : IAccount
    {
        public string HomePage { get; set; }

        public string Name { get; set; }
    }
}
