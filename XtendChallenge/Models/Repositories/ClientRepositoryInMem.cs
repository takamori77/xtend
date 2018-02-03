using System.Collections.Generic;
using System.Linq;
using XtendChallenge.Services.Formatters;

namespace XtendChallenge.Models.Repositories
{
    public class ClientRepositoryInMem : IClientRepository
    {
        private Dictionary<int, Client> Clients = new Dictionary<int, Client>();

        public Client AddClient(Client client)
        {
            Clients.Add(client.Id, client);
            return Clients[client.Id];
        }

        public Client DeleteClient(int id)
        {
            var clientToDelete = Clients[id];
            Clients.Remove(id);
            return clientToDelete;
        }

        public List<Client> GetAllClients() => Clients.Values.ToList();

        public Client GetClientById(int id) => Clients[id];
    }
}
