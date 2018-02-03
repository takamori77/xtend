using System;
using System.Collections.Generic;
using XtendChallenge.Models;
using XtendChallenge.Models.Repositories;
using XtendChallenge.Services.Formatters;
using XtendChallenge.Services.Interfaces;

namespace XtendChallengeTests
{
    public static class TestHelper
    {
        internal static void CleanUpData(IAccountService accountService, IFacilityService facilityService)
        {
            foreach (var account in accountService.GetAllAccounts())
            {
                accountService.DeleteAccount(account.Id);
            }

            foreach (var facility in facilityService.GetAllFacilities())
            {
                facilityService.DeleteFacility(facility.Id);
            }
        }

        internal static List<Account> SetupAccounts(IAccountService accountService, IFacilityService facilityService, IClientRepository clientRepository)
        {
            var accounts = new List<Account>();
            for (int x = 0; x < 10; x++)
            {
                facilityService.AddFacility(GenerateFacility(x));
                var account = GenerateAccount(x, facilityService, clientRepository);
                accountService.AddAccount(account);
                accounts.Add(account);
            }
            return accounts;
        }

        internal static void CleanUpData(IAccountRepository accountRepository, IFacilityService facilityService)
        {
            foreach (var account in accountRepository.GetAllAccounts())
            {
                accountRepository.DeleteAccount(account.Id);
            }

            foreach (var facility in facilityService.GetAllFacilities())
            {
                facilityService.DeleteFacility(facility.Id);
            }
        }

        internal static ExportFile GeneratePipeExportFile(IAccountService accountService, IFacilityService facilityService, PipeFormatter fileFormatter, Client client, IClientRepository clientRepository)
        {
            var exportDate = DateTime.Now;
            CleanUpData(accountService, facilityService);
            SetupAccounts(accountService, facilityService, clientRepository);
            var accounts = accountService.GetAllAccountsByClient(client);
            var exportFile = new ExportFile
            {
                FileName = fileFormatter.FormatFileName(exportDate, client),
                Content = fileFormatter.FormatContent(accounts)
            };
            return exportFile;
        }

        internal static List<ExportFile> GenerateCommaExportFiles(IFacilityService facilityService, IAccountService accountService, CommaFormatter fileFormatter, Client client)
        {
            var facilities = facilityService.GetAllFacilities();
            var exportFiles = new List<ExportFile>();
            var exportDate = DateTime.Now;
            var accountsAboveThreshold = new List<Account>();

            foreach (var facility in facilities)
            {
                var accounts = accountService.GetAllAccountsByFacilityForClient(facility, client);

                foreach (var account in accounts)
                {
                    if (account.Balance > account.Client.BalanceThreshold)
                    {
                        accountsAboveThreshold.Add(account);
                    }
                }
                var exportFile = new ExportFile
                {
                    FileName = fileFormatter.FormatFileName(exportDate, facility),
                    Content = fileFormatter.FormatContent(accountsAboveThreshold)
                };

                exportFiles.Add(exportFile);
            }

            return exportFiles;
        }

        internal static List<Account> SetupAccounts(IAccountRepository accountRepository, IFacilityService facilityService, IClientRepository clientRepository)
        {
            var accounts = new List<Account>();
            for (int x = 0; x < 10; x++)
            {
                facilityService.AddFacility(GenerateFacility(x));
                var account = GenerateAccount(x, facilityService, clientRepository);
                accountRepository.AddAccount(account);
                accounts.Add(account);
            }
            return accounts;
        }

        internal static Account GenerateAccount(int x, IFacilityService facilityService, IClientRepository clientRepository)
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
            Facility facility = new Facility
            {
                AddressLine1 = string.Format("123{0} Main Street", x),
                AddressLine2 = "",
                City = "Nashville",
                FacilityName = string.Format("Great Building {0}", x),
                Id = x,
                State = "TN",
                Zip = string.Format("3721{0}", x)
            };

            return facility;
        }

        internal static Client GenerateClient(int x)
        {
            return new Client
            {
                Id = x < 5 ? 1 : 2,
                Name = x < 5 ? "GeneralHospital" : "VeteranHospital",
                Abbreviation = x < 5 ? "gh" : "vh",
                BalanceThreshold = 100.0m,
                FormatterType = x < 5 ? FormatterType.Pipe : FormatterType.CSV
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
