using System;
using System.Collections.Generic;
using XtendChallenge.Models;
using XtendChallenge.Services.Formatters;
using XtendChallenge.Services.Interfaces;

namespace XtendChallenge.Services
{
    public class FileService : IFileService
    {
        private IAccountService accountService;
        private IFacilityService facilityService;

        public FileService(IAccountService accountService, IFacilityService facilityService)
        {
            this.accountService = accountService;
            this.facilityService = facilityService;
        }

        public ExportFile GetExportFile(PipeFormatter fileFormatter, List<Account> accounts, Client client)
        {
            return new ExportFile
            {
                FileName = fileFormatter.FormatFileName(DateTime.Now, client),
                Content = fileFormatter.FormatContent(accounts)
            };
        }

        public List<ExportFile> GetExportFile(Client client)
        {
            List<ExportFile> exportFiles = new List<ExportFile>();
            if (client.FormatterType == FormatterType.Pipe)
            {
                exportFiles.Add(GetExportFile(new PipeFormatter(), accountService.GetAllAccountsByClient(client), client));
                return exportFiles;
            }
            if (client.FormatterType == FormatterType.CSV)
            {
                exportFiles = GetExportFiles(new CommaFormatter(), facilityService.GetAllFacilities(), client);
                return exportFiles;
            }
            throw new FormatException("No such format");
        }

        public List<ExportFile> GetExportFiles(CommaFormatter fileFormatter, List<Facility> facilities, Client client)
        {
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
    }
}