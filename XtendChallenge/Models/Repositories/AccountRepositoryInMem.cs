using System.Collections.Generic;
using System.Linq;
using XtendChallenge.Services.Interfaces;

namespace XtendChallenge.Models.Repositories
{
    public class AccountRepositoryInMem : IAccountRepository
    {
        Dictionary<int, Account> accounts = new Dictionary<int, Account>();

        public AccountRepositoryInMem()
        {
        }

        public AccountRepositoryInMem(IClientRepository clientRepository, IFacilityService facilityService)
        {
            DataSeed.SetupAccounts(this, clientRepository, facilityService);
        }

        public Account AddAccount(Account account)
        {
            accounts[account.Id] = account;
            return accounts[account.Id];
        }

        public Account DeleteAccount(int id)
        {
            var deletedAccount = accounts[id];
            accounts.Remove(id);
            return deletedAccount;
        }

        public Account GetAccountById(int id) => accounts[id];

        public List<Account> GetAllAccounts() => accounts.Values.ToList();

        public List<Account> GetAllAccountsByClient(Client client)
        {
            var accountsByClient = new List<Account>();

            foreach (KeyValuePair<int, Account> account in accounts)
            {
                if (account.Value.Client.Id == client.Id)
                {
                    accountsByClient.Add(account.Value);
                }
            }

            return accountsByClient;
        }

        public List<Account> GetAllAccountsByFacility(Facility facility)
        {
            var accountsByFacility = new List<Account>();

            foreach (KeyValuePair<int, Account> account in accounts)
            {
                if (account.Value.Facility == facility)
                {
                    accountsByFacility.Add(account.Value);
                }
            }

            return accountsByFacility;
        }

        public List<Account> GetAllAccountsByFacilityForClient(Facility facility, Client client)
        {
            var facilityClientAccounts = new List<Account>();
            foreach (KeyValuePair<int, Account> account in accounts)
            {
                if ((account.Value.Facility.Id == facility.Id) && (account.Value.Client.Id == client.Id))
                {
                    facilityClientAccounts.Add(account.Value);
                }
            }
            return facilityClientAccounts;
        }
    }
}
