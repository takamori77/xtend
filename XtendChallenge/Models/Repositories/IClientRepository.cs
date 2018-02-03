using System.Collections.Generic;

namespace XtendChallenge.Models.Repositories
{
    public interface IClientRepository
    {
        List<Client> GetAllClients();
        Client GetClientById(int id);
        Client AddClient(Client client);
        Client DeleteClient(int id);
    }
}
