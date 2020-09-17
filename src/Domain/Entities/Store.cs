using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.Domain.Entities
{
    public class Store
    {
        public Guid StoreId { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Number of statements stored in this store
        /// </summary>
        public int StatementsCount { get; set; }

        public virtual ICollection<StatementEntity> Statements { get; set; }


        public virtual ICollection<Client> Clients { get; set; }
    }
}
