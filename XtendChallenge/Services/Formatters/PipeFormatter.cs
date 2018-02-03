using System;
using System.Collections.Generic;
using XtendChallenge.Models;

namespace XtendChallenge.Services.Formatters
{
    public class PipeFormatter
    {
        public string FormatFileName(DateTime date, Client client)
        {
            return string.Format("{0}-{1}.data.dat", date.ToString("yyyy-MM-dd"), client.Abbreviation);
        }

        public string FormatContent(List<Account> accounts)
        {
            var fileContent = "";
            foreach (var account in accounts)
            {
                fileContent += string.Format("{0},{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}\r\n",
                    account.Patient.LastName,
                    account.Patient.FirstName.Substring(0, 1),
                    account.Id,
                    account.AccountNumber,
                    account.AdminDate.ToString("MM/dd/yyyy"),
                    account.Balance,
                    account.Client.Id,
                    account.DischargeDate.ToString("MM/dd/yyyy"),
                    account.Facility.Id);
            }

            return fileContent;
        }
    }
}
