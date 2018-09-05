using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using visopt.DataAccess;
using visopt.Models;

namespace visopt.Controllers.api
{
    public class ClientController : ApiController
    {
        private readonly ClientRepo _client;

        public ClientController()
        {
            _client = new ClientRepo();
        }

        [HttpGet]
        public async Task<ICollection<Client>> All()
        {
            return await _client.All();
        }

        [HttpGet]
        public async Task<Client> Find(int id)
        {
            return await _client.Find(id);
        }

        [HttpGet]
        public async Task<Client> ByMobile(string mobile)
        {
            return await _client.Find(mobile);
        }

        [HttpDelete]
        public async Task<int> Remove(int id)
        {
            return await _client.Remove(id);
        }

        [HttpPost]
        public async void Add(Client client)
        {
            await _client.AddOrUpdate(client);
        }
    }
}
