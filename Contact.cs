using Azure.Data.Tables;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khmarni_lab3
{
    public class ContactEntity : ITableEntity
    {
        public string PartitionKey { get; set; } // Grouping Key
        public string RowKey { get; set; } // Unique Identifier for the Contact
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; } // Used for concurrency checks
    }
}
