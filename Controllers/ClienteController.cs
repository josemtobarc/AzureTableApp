using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAzureTable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IConfiguration config;
        public ClienteController(IConfiguration configuration)
        {
            config = configuration;
        }

        [HttpGet]
        public IEnumerable<ClienteEntity> Get(string dui)
        {

            var condition = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, dui);
            var query = new TableQuery<ClienteEntity>().Where(condition);

            string _dbCon1 = config.GetSection("ConnectionStrings").GetSection("MyAzureTable").Value;
            // Method 2
            string _dbCon2 = config.GetValue<string>("ConnectionStrings:MyAzureTable");
            var account = CloudStorageAccount.Parse(_dbCon2);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("Client");
            var lst = table.ExecuteQuery(query);
            return lst.ToList();
        }

        [HttpPost]
        public IEnumerable<ClienteEntity> Post([FromBody] ClienteEntity cli)
        {
            string _dbCon1 = config.GetSection("ConnectionStrings").GetSection("MyAzureTable").Value;
            // Method 2
            string _dbCon2 = config.GetValue<string>("ConnectionStrings:MyAzureTable");
            var account = CloudStorageAccount.Parse(_dbCon2);
            var client = account.CreateCloudTableClient();

            var table = client.GetTableReference("Client");

            table.CreateIfNotExists();

            ClienteEntity clienteEntity = new ClienteEntity(cli.id, cli.NumeroDocumento);
            clienteEntity.id = cli.id;
            clienteEntity.NumeroDocumento = cli.NumeroDocumento;
            var query = new TableQuery<ClienteEntity>();
            TableOperation insertOperation = TableOperation.Insert(clienteEntity);


            table.Execute(insertOperation);
            var lst = table.ExecuteQuery(query);
            return lst.ToList();

        }

    }
}
