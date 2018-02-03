using System;
using System.Collections.Generic;
using XtendChallenge.Services.Interfaces;

namespace XtendChallenge.Models.Repositories
{
    public static class DataSeed
    {
        internal static List<Account> SetupAccounts(IAccountRepository accountRepository, IClientRepository clientRepository, IFacilityService facilityService)
        {
            var accounts = new List<Account>();
            for (int x = 0; x < 12; x++)
            {
                facilityService.AddFacility(GenerateFacility(x));
                var account = GenerateAccount(x, clientRepository, facilityService);
                accountRepository.AddAccount(account);
                accounts.Add(account);
            }
            return accounts;
        }

        internal static Account GenerateAccount(int x, IClientRepository clientRepository, IFacilityService facilityService)
        {
            var account = new Account
            {
                Id = x,
                Client = x < 5 ? clientRepository.GetClientById(1) : clientRepository.GetClientById(2),
                AccountNumber = x,
                Balance = x,
                Facility = facilityService.GetFacilityById(x),
                AdminDate = GetValidDate(),
                DischargeDate = GetValidDate(),
                Patient = GeneratePatient(x),
                Insurance = new List<Insurance>()
            };
            account.Insurance.Add(GenerateInsurance(x));

            return account;
        }

        internal static Patient GeneratePatient(int x)
        {
            return new Patient
            {
                FirstName = "John",
                Id = x,
                LastName = string.Format("Doe the {0}", x),
                MiddleName = "Smith",
                SocialSecurityNumber = string.Format("123-45-678{0}", x)
            };
        }

        internal static Facility GenerateFacility(int x)
        {
            return new Facility
            {
                AddressLine1 = string.Format("123{0} Main Street", x),
                AddressLine2 = "",
                City = "Nashville",
                FacilityName = string.Format("Great Building {0}", x),
                Id = x,
                State = "TN",
                Zip = string.Format("3721{0}", x)
            };
        }

        internal static DateTime GetValidDate()
        {
            return DateTime.Today;
        }

        internal static Insurance GenerateInsurance(int x)
        {
            return new Insurance
            {
                Id = x,
                GroupName = string.Format("G{0}", x),
                PlanName = string.Format("HD HMO {0}", x),
                Policy = string.Format("P12134ABC{0}", x)
            };
        }
    }
}
