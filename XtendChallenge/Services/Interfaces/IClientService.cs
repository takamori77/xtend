using System.Collections.Generic;
using XtendChallenge.Models;

namespace XtendChallenge.Services.Interfaces
{
    public interface IClientService
    {
        List<Client> GetAllClients();
        Client GetClient(int id);
        Client DeleteClient(int id);
        Client AddClient(Client client);
        Client GetClientById(int v);
    }
}
