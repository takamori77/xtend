using System.Collections.Generic;

namespace XtendChallenge.Models.Repositories
{
    public interface IAccountRepository
    {
        List<Account> GetAllAccounts();
        Account GetAccountById(int id);
        Account AddAccount(Account account);
        Account DeleteAccount(int id);
        List<Account> GetAllAccountsByFacility(Facility facility);
        List<Account> GetAllAccountsByClient(Client client);
        List<Account> GetAllAccountsByFacilityForClient(Facility facility, Client client);
    }
}