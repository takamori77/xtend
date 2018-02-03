using System.Collections.Generic;
using XtendChallenge.Models;

namespace XtendChallenge.Services.Interfaces
{
    public interface IAccountService
    {
        List<Account> GetAllAccounts();
        Account AddAccount(Account account);
        Account DeleteAccount(int id);
        Account GetAccountById(int id);
        List<Account> GetAllAccountsByFacility(Facility facility);
        List<Account> GetAllAccountsByClient(Client client);
        List<Account> GetAllAccountsByFacilityForClient(Facility facility, Client client);
    }
}
