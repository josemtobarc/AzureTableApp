using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAzureTable
{
    public class ClienteEntity : TableEntity
    {
        public ClienteEntity(string numerodocumento, string id)
        {
            this.PartitionKey = id; this.RowKey = numerodocumento;
        }

        public ClienteEntity() { }

        public string id { get; set; }
        public string NumeroDocumento { get; set; }

    }
}
