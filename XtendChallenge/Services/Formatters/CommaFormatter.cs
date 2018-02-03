using System;
using System.Collections.Generic;
using XtendChallenge.Models;

namespace XtendChallenge.Services.Formatters
{
    public class CommaFormatter
    {
        public string FormatFileName(DateTime date, Facility facility)
        {
            return string.Format("export-{0}.{1}.csv", date.ToString("yyyy-MM-dd"), facility.FacilityName);
        }

        public string FormatContent(List<Account> accounts)
        {
            var fileContent = "";
            foreach (var account in accounts)
            {
                fileContent += string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}",
                   account.Id,
                    account.AccountNumber,
                    account.AdminDate.ToString("MM/dd/yyyy"),
                    account.Balance.ToString("c"),
                    account.Client.Id,
                    account.DischargeDate.ToString("MM/dd/yyyy"),
                    account.Facility.AddressLine1,
                    account.Facility.AddressLine2,
                    account.Facility.City,
                    account.Facility.State,
                    account.Facility.FacilityName,
                    account.Facility.Id,
                    account.Facility.Zip,
                    account.Patient.SocialSecurityNumber
                    );
                foreach (var insurance in account.Insurance)
                {
                    fileContent += string.Format(",{0},{1},{2},{3}",
                        insurance.Id,
                        insurance.GroupName,
                        insurance.PlanName,
                        insurance.Policy
                        );
                }
                fileContent += "\r\n";
            }
            return fileContent;
        }
    }
}
