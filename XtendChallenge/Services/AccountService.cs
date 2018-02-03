using System.Collections.Generic;
using XtendChallenge.Models;
using XtendChallenge.Models.Repositories;
using XtendChallenge.Services.Interfaces;

namespace XtendChallenge.Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository) => this.accountRepository = accountRepository;

        public Account AddAccount(Account account) => accountRepository.AddAccount(account);

        public Account DeleteAccount(int id) => accountRepository.DeleteAccount(id);

        public Account GetAccountById(int id) => accountRepository.GetAccountById(id);

        public List<Account> GetAllAccounts() => accountRepository.GetAllAccounts();

        public List<Account> GetAllAccountsByClient(Client client) => accountRepository.GetAllAccountsByClient(client);

        public List<Account> GetAllAccountsByFacility(Facility facility) => accountRepository.GetAllAccountsByFacility(facility: facility);

        public List<Account> GetAllAccountsByFacilityForClient(Facility facility, Client client) => accountRepository.GetAllAccountsByFacilityForClient(facility, client);
    }
}
