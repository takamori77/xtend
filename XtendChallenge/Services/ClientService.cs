using System.Collections.Generic;
using XtendChallenge.Models;
using XtendChallenge.Models.Repositories;
using XtendChallenge.Services.Interfaces;

namespace XtendChallenge.Services
{
    public class ClientService : IClientService
    {
        private IClientRepository ClientRepository;

        public ClientService(IClientRepository clientRepository) => ClientRepository = clientRepository;

        public Client AddClient(Client client) => ClientRepository.AddClient(client);

        public Client DeleteClient(int id) => ClientRepository.DeleteClient(id);

        public List<Client> GetAllClients() => ClientRepository.GetAllClients();

        public Client GetClient(int id) => ClientRepository.GetClientById(id);

        public Client GetClientById(int id) => ClientRepository.GetClientById(id);
    }
}
