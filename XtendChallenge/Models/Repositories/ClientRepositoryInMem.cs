using System.Collections.Generic;
using System.Linq;
using XtendChallenge.Services.Formatters;

namespace XtendChallenge.Models.Repositories
{
    public class ClientRepositoryInMem : IClientRepository
    {
        private Dictionary<int, Client> Clients = new Dictionary<int, Client>();

        public ClientRepositoryInMem()
        {
            var client1 = new Client
            {
                Abbreviation = "gh",
                Id = 1,
                Name = "GeneralHospital",
                FormatterType = FormatterType.Pipe,
                BalanceThreshold = 0
            };

            var client2 = new Client
            {
                Abbreviation = "vh",
                Id = 2,
                Name = "VeteranHospital",
                FormatterType = FormatterType.CSV,
                BalanceThreshold = 0.0m
            };
            Clients.Add(client1.Id, client1);
            Clients.Add(client2.Id, client2);
        }

        public List<Client> GetAllClients()
        {
            return Clients.Values.ToList();
        }

        public Client GetClientById(int id)
        {
            return Clients[id];
        }
    }
}
