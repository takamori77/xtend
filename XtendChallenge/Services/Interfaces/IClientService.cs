using System.Collections.Generic;
using XtendChallenge.Models;

namespace XtendChallenge.Services.Interfaces
{
    interface IClientService
    {
        List<Client> GetAllClients();
        Client GetClient(int id);
    }
}
