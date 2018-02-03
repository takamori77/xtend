using System.Collections.Generic;
using XtendChallenge.Models;
using XtendChallenge.Services.Interfaces;

namespace XtendChallenge.Services.Stubs
{
    public class AccountServiceStub : IAccountService
    {
        private Account account = new Account();
        private List<Account> accounts = new List<Account>();

        public Account AddAccount(Account account)
        {
            return account;
        }

        public Account DeleteAccount(int id)
        {
            return account;
        }

        public Account GetAccountById(int id)
        {
            return account;
        }

        public List<Account> GetAllAccounts()
        {
            return accounts;
        }

        public List<Account> GetAllAccountsByClient(Client client)
        {
            return accounts;
        }

        public List<Account> GetAllAccountsByFacility(Facility facility)
        {
            return accounts;
        }

        public List<Account> GetAllAccountsByFacilityForClient(Facility facility, Client client)
        {
            return accounts;
        }
    }
}
