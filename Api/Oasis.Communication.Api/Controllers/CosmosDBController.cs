using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Oasis.Communication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CosmosDBController : ControllerBase
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;
        private readonly string _databaseId;
        private readonly string _containerId;

        public CosmosDBController(CosmosClient cosmosClient, IConfiguration configuration)
        {
            _cosmosClient = cosmosClient;
            _databaseId = configuration.GetValue<string>("DatabaseId");
            _containerId = configuration.GetValue<string>("containerId");
            _container = _cosmosClient.GetContainer(_databaseId, _containerId);
        }

        [HttpGet]
        [Route("GetAllData")]
        public async Task<List<Account>> GetAll()
        {
            //Way 1
            //FeedIterator<Account> accountFeedIterator = _container.GetItemQueryIterator<Account>(new QueryDefinition("select * from c"));
            //List<Account> accounts = new List<Account>();
            //while (accountFeedIterator.HasMoreResults)
            //{
            //    FeedResponse<Account> currentResultSet = await accountFeedIterator.ReadNextAsync();
            //    foreach (Account account in currentResultSet)
            //    {
            //        accounts.Add(account);
            //    }
            //}
            //return accounts;

            //Way 2
            FeedResponse<Account> accountFeedResponse = await _container.GetItemLinqQueryable<Account>().ToFeedIterator().ReadNextAsync();
            return accountFeedResponse.Resource.ToList();
        }

        [HttpGet]
        public async Task<Account> Get()
        {
            ItemResponse<Account> response = await _container.ReadItemAsync<Account>("1", new Microsoft.Azure.Cosmos.PartitionKey("fda78c9f-215b-4f57-aac1-b9c9eb9bcf10"));
            return response.Resource;
        }

        [HttpPost]
        public async Task<string> Post([FromBody] Account account)
        {
            ItemResponse<Account> response = await _container.UpsertItemAsync<Account>(account, new Microsoft.Azure.Cosmos.PartitionKey(account.AccountId.ToString()));
            if (response == null) return "Adding new account " + account.Name + " failed...";
            return "Account " + response.Resource.Name + " added successfully...";
        }

        [HttpPut]
        public async Task<string> Put([FromBody] Account account)
        {
            ItemResponse<Account> response = await _container.UpsertItemAsync<Account>(account, new Microsoft.Azure.Cosmos.PartitionKey(account.AccountId.ToString()));
            if (response == null) return "Updating account " + account.Name + " failed...";
            return "Account " + response.Resource.Name + " updated successfully...";
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(string id)
        {
            ItemResponse<Account> response = await _container.DeleteItemAsync<Account>("string", new Microsoft.Azure.Cosmos.PartitionKey(id.ToString()));
            if (response == null) return "Deleting account " + id + " failed...";
            return "Account " + id + " deleted successfully...";
        }
    }

    public class Account
    {
        public string id { get; set; }
        public Guid AccountId { get; set; }
        public string Name { get; set; }
    }
}
